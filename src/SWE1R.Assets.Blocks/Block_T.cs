// SPDX-License-Identifier: MIT

using ByteSerialization.IO;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace SWE1R.Assets.Blocks
{
    [DebuggerDisplay("{" + nameof(DebuggerDisplay) + ",nq}")]
    public class Block<T> : IBlock, IList<T> where T : BlockItem, new()
    {
        #region Properties

        private string DebuggerDisplay =>
            $"{nameof(Count)}={Count}";

        public Endianness Endianness { get; set; }
        public byte[] Bytes { get; private set; }
        private List<T> Items { get; } = new List<T>();
        private Dictionary<BlockItemPart, int> Offsets { get; set; }
        private int Size { get; set; }

        public int Count => Items.Count;
        public byte[] Hash { get; private set; }
        public string HashString { get; private set; }

        public int IndexOf(BlockItem item) => Items.IndexOf((T)item);
        public int PartsCount => new T().Parts.Length;
        private int HeaderSize =>
            /* count   */ sizeof(int) +
            /* offsets */ sizeof(int) * Count * PartsCount +
            /* size    */ sizeof(int);

        public BlockItemType BlockItemType =>
            BlockItemTypeAttributeHelper.GetBlockItemType(typeof(T));

        #endregion

        #region Constructor

        public Block(Endianness endianness) =>
            Endianness = endianness;

        #endregion

        #region Methods (load)

        public void Load(string filename) =>
            Load(File.OpenRead(filename));

        public void Load(Stream stream)
        {
            LoadBytes(stream);
            LoadItems();
        }

        private void LoadBytes(Stream stream)
        {
            using (var r = new EndianBinaryReader(stream, Endianness))
            {
                long start = stream.Position;

                int n = r.ReadInt32();
                stream.Position += n * sizeof(int) * PartsCount;

                int length = r.ReadInt32();
                stream.Position = start;

                Bytes = r.ReadBytes(length);
                Hash = Bytes.GetSha1();
                HashString = Hash.ToHexString();
            }
        }

        private void LoadItems()
        {
            Items.Clear();

            using (var ms = new MemoryStream(Bytes))
            using (var reader = new EndianBinaryReader(ms, Endianness))
            {
                // count
                Items.AddRange(Enumerable.Range(0, reader.ReadInt32()).Select(i => new T() { Block = this }));

                // offsets
                Offsets = Items.SelectMany(i => i.Parts).ToDictionary(p => p, p => reader.ReadInt32());

                // size
                Size = reader.ReadInt32();

                // parts
                foreach (T item in Items)
                {
                    int end = GetEnd(item);
                    for (int i = 0; i < item.Parts.Length; i++)
                        item.Parts[i].Load(reader.ReadBytes(GetPartSize(item, i, end)));
                }
            }
        }

        private int GetEnd(T item)
        {
            T nextItem = Items.ElementAtOrDefault(IndexOf(item) + 1);
            if (nextItem != null)
                return Offsets[nextItem.Parts.First()];
            else
                return Size;
        }

        private int GetPartSize(T item, int i, int itemEnd)
        {
            int start = Offsets[item.Parts[i]];
            if (start == 0)
                return 0;

            int next;
            BlockItemPart nextPart = item.Parts.ElementAtOrDefault(i + 1);
            if (nextPart == null || Offsets[nextPart] == 0)
                next = itemEnd;
            else
                next = Offsets[nextPart];

            return next - start;
        }

        #endregion

        #region Methods (save)

        public void Save(string filename)
        {
            Save();
            File.WriteAllBytes(filename, Bytes);
        }

        public void Save()
        {
            Bytes = SaveBytes();
            Hash = Bytes.GetSha1();
            HashString = Hash.ToHexString();
        }

        private byte[] SaveBytes()
        {
            using (var ms = new MemoryStream())
            using (var writer = new EndianBinaryWriter(ms, Endianness))
            {
                // count
                writer.Write(Count);

                // offsets
                int offset = HeaderSize;
                foreach (T item in Items)
                {
                    foreach (BlockItemPart part in item.Parts)
                    {
                        if (part.Length == 0)
                            writer.Write(0);
                        else
                            writer.Write(offset);
                        offset += part.Length;
                    }
                }

                // end
                writer.Write(offset);

                // items
                Items.ForEach(i => writer.Write(i.Bytes));

                return ms.ToArray();
            }
        }

        #endregion

        #region Methods (: IList<T>)

        public int IndexOf(T item) => Items.IndexOf(item);
        public void Insert(int index, T item) => Items.Insert(index, item);
        public void RemoveAt(int index) => Items.RemoveAt(index);

        public T this[int index] { get => Items[index]; set => Items[index] = value; }

        public void Add(T item) => Items.Add(item);
        public void Clear() => Items.Clear();
        public bool Contains(T item) => Items.Contains(item);
        public void CopyTo(T[] array, int arrayIndex) => Items.CopyTo(array, arrayIndex);
        public bool IsReadOnly => false;
        public bool Remove(T item) => Items.Remove(item);

        public IEnumerator<T> GetEnumerator() => Items.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => Items.GetEnumerator();

        #endregion
    }
}
