// Copyright 2024 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

namespace SWE1R.Assets.Blocks.SpriteBlock
{
    public class SpriteBlockItemPart : BlockItemPart
    {
        public SpriteBlockItemPart() : base() { }
        private SpriteBlockItemPart(SpriteBlockItemPart source) : base(source) { }

        public override BlockItemPart Clone() => new SpriteBlockItemPart(this);
    }
}
