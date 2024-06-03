// SPDX-License-Identifier: MIT

using SWE1R.Assets.Blocks.ModelBlock;
using SWE1R.Assets.Blocks.ModelBlock.Nodes;
using System.ComponentModel.DataAnnotations.Schema;

namespace SWE1R.Assets.Blocks.Original.SQLite.Entities.ModelBlock.Nodes
{
    [Table($"{nameof(Model)}_{nameof(BasicNode)}")]
    public class DbBasicNode : DbNode<BasicNode>
    {
        public override bool Equals(object obj)
        {
            if (obj is DbBasicNode)
                return this.Equals((DbBasicNode)obj);
            else
                return base.Equals(obj);
        }

        public override int GetHashCode() => 
            base.GetHashCode();
    }
}
