// SPDX-License-Identifier: MIT

namespace SWE1R.Assets.Blocks.SplineBlock
{
    public class SplineBlockItemPart : BlockItemPart
    {
        #region Constructor

        public SplineBlockItemPart() :
            base()
        { }

        private SplineBlockItemPart(SplineBlockItemPart source) :
            base(source)
        { }

        #endregion

        #region Methods

        public override BlockItemPart Clone() => 
            new SplineBlockItemPart(this);

        #endregion
    }
}
