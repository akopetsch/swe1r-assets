// SPDX-License-Identifier: MIT

using SWE1R.Assets.Blocks.ModelBlock.Nodes;
using System.ComponentModel.DataAnnotations.Schema;

namespace SWE1R.Assets.Blocks.Original.SQLite.Entities.ModelBlock.Nodes
{
    [Table("Model_Node5064")]
    public class DbNode5064 : DbNode<BasicNode>
    {
        public override bool Equals(object obj)
        {
            if (obj is DbNode5064)
                return this.Equals((DbNode5064)obj);
            else
                return base.Equals(obj);
        }

        public override int GetHashCode() => 
            base.GetHashCode();
    }
}
