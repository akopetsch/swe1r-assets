// SPDX-License-Identifier: GPL-2.0-only

using ByteSerialization.Nodes;
using SWE1R.Assets.Blocks.ModelBlock.Nodes;
using System.ComponentModel.DataAnnotations.Schema;

namespace SWE1R.Assets.Blocks.Original.SQLite.Entities.ModelBlock.Nodes
{
    [Table("Model_Node5065")]
    public class DbNode5065 : DbNode<Group5065>
    {
        public int Int { get; set; }

        public override void CopyFrom(Node node)
        {
            base.CopyFrom(node);

            var n = (Group5065)node.Value;

            Int = n.Int;
        }

        public override bool Equals(DbBlockItemStructure<Group5065> other)
        {
            var _other = (DbNode5065)other;

            if (!base.Equals(_other))
                return false;

            if (Int != _other.Int) return false;

            return true;
        }

        public override bool Equals(object obj)
        {
            if (obj is DbNode5065)
                return this.Equals((DbNode5065)obj);
            else
                return base.Equals(obj);
        }

        public override int GetHashCode() =>
            HashCode.Combine(base.GetHashCode(), Int);
    }
}
