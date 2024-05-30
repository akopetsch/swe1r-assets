﻿// SPDX-License-Identifier: MIT

namespace SWE1R.Assets.Blocks.SplineBlock
{
    public class SplineBlockItemPart : BlockItemPart
    {
        public SplineBlockItemPart() : base() { }
        private SplineBlockItemPart(SplineBlockItemPart source) : base(source) { }

        public override BlockItemPart Clone() => new SplineBlockItemPart(this);
    }
}