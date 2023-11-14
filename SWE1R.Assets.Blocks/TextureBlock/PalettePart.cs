// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization.IO;
using SWE1R.Assets.Blocks.Common.Colors;
using System.IO;

namespace SWE1R.Assets.Blocks.TextureBlock
{
    public class PalettePart : BlockItemPart
    {
        public PalettePart() : base() { }
        private PalettePart(PalettePart source) : base(source) { }
        
        public ColorArgbF[] GetColors()
        {
            var colors = new ColorArgbF[Length / sizeof(short)];
            using (var s = new MemoryStream(Bytes))
            using (var r = new EndianBinaryReader(s, Endianness.BigEndian))
            {
                for (int i = 0; i < colors.Length; i++)
                {
                    colors[i] = ColorArgbF.FromRgba5551(r.ReadInt16());
                }
            }
            return colors;
        }

        public override BlockItemPart Clone() => new PalettePart(this);
    }
}
