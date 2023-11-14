// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

namespace SWE1R.Assets.Blocks.SpriteBlock
{
    public class SpritePart : BlockItemPart
    {
        public SpritePart() : base() { }
        private SpritePart(SpritePart source) : base(source) { }

        public override BlockItemPart Clone() => new SpritePart(this);
    }
}
