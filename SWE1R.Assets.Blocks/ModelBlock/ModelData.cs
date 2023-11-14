// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization.IO;
using SWE1R.Assets.Blocks.Common.Compression;
using System;
using System.IO;

namespace SWE1R.Assets.Blocks.ModelBlock
{
    public class ModelData : BlockItemPart
    {
        private static string CompressionSignature { get; } = "CompWolf";
        private static Compressor Compressor => Compressor.Instance;

        public bool WasCompressed { get; private set; }

        public ModelData() => Loaded += (s, a) => Decompress();
        private ModelData(ModelData source) : base(source) { }
        
        public void Compress()
        {
            using (var s = new MemoryStream())
            using (var w = new EndianBinaryWriter(s, Endianness.BigEndian))
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
                using (var r = new EndianBinaryReader(s, Endianness.BigEndian))
                {
                    r.ReadBytes(CompressionSignature.Length);

                    int size = r.ReadInt32();

                    byte[] compressed = r.ReadBytes(Length - (int)s.Position);
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
            using (var r = new EndianBinaryReader(s, Endianness.BigEndian))
                return new string(r.ReadChars(comp.Length)).Equals(comp);
        }

        public override BlockItemPart Clone() => new ModelData(this);
    }
}
