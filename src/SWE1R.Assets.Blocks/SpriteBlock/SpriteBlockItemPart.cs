// SPDX-License-Identifier: GPL-2.0-only

namespace SWE1R.Assets.Blocks.SpriteBlock
{
    public class SpriteBlockItemPart : BlockItemPart
    {
        public SpriteBlockItemPart() : base() { }
        private SpriteBlockItemPart(SpriteBlockItemPart source) : base(source) { }

        public override BlockItemPart Clone() => new SpriteBlockItemPart(this);
    }
}
