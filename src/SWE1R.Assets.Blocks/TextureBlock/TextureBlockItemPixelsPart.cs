// SPDX-License-Identifier: MIT

namespace SWE1R.Assets.Blocks.TextureBlock
{
    public class TextureBlockItemPixelsPart : BlockItemPart
    {
        public TextureBlockItemPixelsPart() : base() { }
        private TextureBlockItemPixelsPart(TextureBlockItemPixelsPart source) : base(source) { }

        public override BlockItemPart Clone() => new TextureBlockItemPixelsPart(this);
    }
}
