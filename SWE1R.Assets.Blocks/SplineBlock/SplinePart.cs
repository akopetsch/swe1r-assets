// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

namespace SWE1R.Assets.Blocks.SplineBlock
{
    public class SplinePart : BlockItemPart
    {
        public SplinePart() : base() { }
        private SplinePart(SplinePart source) : base(source) { }

        public override BlockItemPart Clone() => new SplinePart(this);
    }
}
