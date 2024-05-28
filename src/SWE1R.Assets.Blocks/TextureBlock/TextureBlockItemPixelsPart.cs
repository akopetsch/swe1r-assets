// Copyright 2024 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

namespace SWE1R.Assets.Blocks.TextureBlock
{
    public class TextureBlockItemPixelsPart : BlockItemPart
    {
        public TextureBlockItemPixelsPart() : base() { }
        private TextureBlockItemPixelsPart(TextureBlockItemPixelsPart source) : base(source) { }

        public override BlockItemPart Clone() => new TextureBlockItemPixelsPart(this);
    }
}
