// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization.IO;
using SWE1R.Assets.Blocks.Colors;
using System.IO;

namespace SWE1R.Assets.Blocks.TextureBlock
{
    public class TextureBlockItemPalettePart : BlockItemPart
    {
        public TextureBlockItemPalettePart() : base() { }
        private TextureBlockItemPalettePart(TextureBlockItemPalettePart source) : base(source) { }
        
        public ColorRgba5551[] GetColors()
        {
            var colors = new ColorRgba5551[Length / sizeof(short)];
            using (var s = new MemoryStream(Bytes))
            using (var r = new EndianBinaryReader(s, Endianness.BigEndian))
            {
                for (int i = 0; i < colors.Length; i++)
                {
                    colors[i] = new ColorRgba5551(r.ReadInt16());
                }
            }
            return colors;
        }

        public override BlockItemPart Clone() => new TextureBlockItemPalettePart(this);
    }
}
