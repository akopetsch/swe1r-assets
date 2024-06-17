// SPDX-License-Identifier: MIT

using ByteSerialization.IO;
using SWE1R.Assets.Blocks.Common.Compression;
using System;
using System.IO;

namespace SWE1R.Assets.Blocks.ModelBlock
{
    public class ModelBlockItemDataPart : BlockItemPart
    {
        private static string CompressionSignature { get; } = "CompWolf";
        private static Compressor Compressor => Compressor.Instance;

        public bool WasCompressed { get; private set; }

        public ModelBlockItemDataPart() => Loaded += (s, a) => Decompress();
        private ModelBlockItemDataPart(ModelBlockItemDataPart source) : base(source) { }
        
        public void Compress()
        {
            using (var s = new MemoryStream())
            using (var w = new EndianBinaryWriter(s, Item.Block.Endianness))
            {
                w.Write(CompressionSignature.ToCharArray());
                w.Write(Length);
                w.Write(Compressor.Compress(Bytes));

                Bytes = s.ToArray();
            }
        }

        public void Decompress()
        {
            if (IsCompressed())
            {
                using (var s = new MemoryStream(Bytes))
                using (var r = new EndianBinaryReader(s, Item.Block.Endianness))
                {
                    r.Read<byte>(CompressionSignature.Length);

                    int size = r.ReadInt32();

                    byte[] compressed = r.Read<byte>(Length - (int)s.Position);
                    byte[] decompressed = Compressor.Decompress(compressed);

                    if (decompressed.Length != size)
                        throw new InvalidOperationException();

                    Bytes = decompressed;
                }
                WasCompressed = true;
            }
        }

        public bool IsCompressed()
        {
            const string comp = "Comp";
            using (var s = new MemoryStream(Bytes))
            using (var r = new EndianBinaryReader(s, Item.Block.Endianness))
                return new string(r.Read<char>(comp.Length)).Equals(comp);
        }

        public override BlockItemPart Clone() => new ModelBlockItemDataPart(this);
    }
}
