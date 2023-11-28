// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization.IO;
using ByteSerialization.IO.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace SWE1R.Assets.Blocks
{
    public interface IBlock : IEnumerable
    {
        byte[] Bytes { get; }
        int Count { get; }
        byte[] Hash { get; }
        string HashString { get; }
        
        void Load(string filename);
        void Load(Stream s);

        void Save(string filename);
        void Save();
        
        int IndexOf(BlockItem item);
        int PartsCount { get; }
    }

    public abstract class Block : IBlock
    {
        #region Properties (: IBlock)

        public abstract byte[] Bytes { get; }

        public abstract int Count { get; }

        public abstract byte[] Hash { get; }

        public abstract string HashString { get; }

        public abstract int PartsCount { get; }

        #endregion

        #region Methods (: IBlock)

        public abstract IEnumerator GetEnumerator();
        public abstract int IndexOf(BlockItem item);
        public abstract void Load(string filename);
        public abstract void Load(Stream s);
        public abstract void Save(string filename);
        public abstract void Save();

        #endregion

        #region Methods (helper)

        public static Block<TItem> Load<TItem>(string filename) where TItem : BlockItem, new()
        {
            var block = new Block<TItem>();
            block.Load(filename);
            return block;
        }

        public static Block<TItem> Load<TItem>(Stream stream) where TItem : BlockItem, new()
        {
            var block = new Block<TItem>();
            block.Load(stream);
            return block;
        }

        #endregion
    }

    [DebuggerDisplay("{" + nameof(DebuggerDisplay) + ",nq}")]
    public class Block<T> : IBlock, IList<T> where T : BlockItem, new()
    {
        #region Properties

        private string DebuggerDisplay =>
            $"{nameof(Count)}={Count}";

        public byte[] Bytes { get; private set; }
        private List<T> Items { get; } = new List<T>();
        private Dictionary<BlockItemPart, int> Offsets { get; set; }
        private int Size { get; set; }

        public int Count => Items.Count;
        public byte[] Hash => Bytes.GetSha1();
        public string HashString => Hash.ToHexString();
        
        public int IndexOf(BlockItem item) => Items.IndexOf((T)item);
        public int PartsCount => new T().Parts.Length;
        private int HeaderSize =>
            /* count   */ sizeof(int) + 
            /* offsets */ sizeof(int) * Count * PartsCount + 
            /* size    */ sizeof(int);
        
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
            using (var r = new EndianBinaryReader(stream, Endianness.BigEndian))
            {
                long start = stream.Position;

                int n = r.ReadInt32();
                stream.Position += n * sizeof(int) * PartsCount;

                int length = r.ReadInt32();
                stream.Position = start;

                Bytes = r.ReadBytes(length);
            }
        }

        private void LoadItems()
        {
            Items.Clear();

            using (var s = new MemoryStream(Bytes))
            using (var r = new EndianBinaryReader(s, Endianness.BigEndian))
            {
                // count
                Items.AddRange(Enumerable.Range(0, r.ReadInt32()).Select(i => new T() { Block = this }));

                // offsets
                Offsets = Items.SelectMany(i => i.Parts).ToDictionary(p => p, p => r.ReadInt32());

                // size
                Size = r.ReadInt32();
                
                // parts
                foreach (T item in Items)
                {
                    int end = GetEnd(item);
                    for (int i = 0; i < item.Parts.Length; i++)
                        item.Parts[i].Load(r.ReadBytes(GetPartSize(item, i, end)));
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

        public void Save() => 
            Bytes = SaveBytes();

        private byte[] SaveBytes()
        {
            using (var s = new MemoryStream())
            using (var w = new EndianBinaryWriter(s, Endianness.BigEndian))
            {
                // count
                w.Write(Count);

                // offsets
                int offset = HeaderSize;
                foreach (T item in Items)
                {
                    foreach (BlockItemPart part in item.Parts)
                    {
                        if (part.Length == 0)
                            w.Write(0);
                        else
                            w.Write(offset);
                        offset += part.Length;
                    }
                }

                // end
                w.Write(offset);

                // items
                Items.ForEach(i => w.Write(i.Bytes));

                return s.ToArray();
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
