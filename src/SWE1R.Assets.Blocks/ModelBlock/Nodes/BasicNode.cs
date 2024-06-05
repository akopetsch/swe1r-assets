// SPDX-License-Identifier: MIT

namespace SWE1R.Assets.Blocks.ModelBlock.Nodes
{
    public class BasicNode : FlaggedNode
    {
        #region Constructor

        public BasicNode() : base() =>
            Flags = NodeFlags.BasicNode;

        #endregion
    }
}
