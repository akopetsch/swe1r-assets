// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

namespace SWE1R.Assets.Blocks.SplineBlock
{
    public class SplineBlockItemPart : BlockItemPart
    {
        public SplineBlockItemPart() : base() { }
        private SplineBlockItemPart(SplineBlockItemPart source) : base(source) { }

        public override BlockItemPart Clone() => new SplineBlockItemPart(this);
    }
}
