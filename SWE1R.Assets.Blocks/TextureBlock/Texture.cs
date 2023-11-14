// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization;
using SWE1R.Assets.Blocks.Common.Colors;
using System;

namespace SWE1R.Assets.Blocks.TextureBlock
{
    public class Texture : BlockItem<PixelsPart, PalettePart>
    {
        public PixelsPart PixelsPart => Part1;
        public PalettePart PalettePart => Part2;

        public ColorArgbF[] PaletteColors { get; set; }

        public Texture() : base() { }
        public Texture(Texture source) : base(source) { }
        
        public override void Load(out ByteSerializerContext context)
        {
            context = null;
            PaletteColors = PalettePart.GetColors();
        }

        public override void Unload() => PaletteColors = null;

        public override void Save(out ByteSerializerContext context) => 
            throw new NotImplementedException();

        public override BlockItem Clone() => new Texture(this);
    }
}
