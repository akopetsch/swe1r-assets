// SPDX-License-Identifier: MIT

using SWE1R.Assets.Blocks.ModelBlock;
using SWE1R.Assets.Blocks.ModelBlock.Nodes;
using System.ComponentModel.DataAnnotations.Schema;

namespace SWE1R.Assets.Blocks.Original.SQLite.Entities.ModelBlock.Nodes
{
    [Table($"{nameof(Model)}_{nameof(FlaggedNode)}_{nameof(BasicNode)}")]
    public class DbBasicNode : DbNode<BasicNode>
    {
        public override bool Equals(object obj)
        {
            if (obj is DbBasicNode x)
                return Equals(x);
            else
                return base.Equals(obj);
        }

        public override int GetHashCode() => 
            CombineHashCodes(base.GetHashCode());
    }
}
