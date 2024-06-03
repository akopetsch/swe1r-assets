// SPDX-License-Identifier: MIT

using ByteSerialization.Nodes;
using SWE1R.Assets.Blocks.ModelBlock.Nodes;
using System.ComponentModel.DataAnnotations.Schema;

namespace SWE1R.Assets.Blocks.Original.SQLite.Entities.ModelBlock.Nodes
{
    [Table("Model_NodeD066")]
    public class DbNodeD066 : DbNode<TransformedComputedNode>
    {
        public short Word1 { get; set; }
        public short Word2 { get; set; }
        public float Vector_X { get; set; }
        public float Vector_Y { get; set; }
        public float Vector_Z { get; set; }

        public override void CopyFrom(Node node)
        {
            base.CopyFrom(node);

            var n = (TransformedComputedNode)node.Value;

            Word1 = n.Word1;
            Word2 = n.Word2;
            Vector_X = n.Vector.X;
            Vector_Y = n.Vector.Y;
            Vector_Z = n.Vector.Z;
        }

        public override bool Equals(DbBlockItemStructure<TransformedComputedNode> other)
        {
            var _other = (DbNodeD066)other;

            if (!base.Equals(_other))
                return false;

            if (Word1 != _other.Word1) return false;
            if (Word2 != _other.Word2) return false;
            if (Vector_X != _other.Vector_X) return false;
            if (Vector_Y != _other.Vector_Y) return false;
            if (Vector_Z != _other.Vector_Z) return false;

            return true;
        }

        public override bool Equals(object obj)
        {
            if (obj is DbNodeD066)
                return this.Equals((DbNodeD066)obj);
            else
                return base.Equals(obj);
        }

        public override int GetHashCode() =>
            HashCode.Combine(base.GetHashCode(), Word1, Word1,
                HashCode.Combine(Vector_X, Vector_Y, Vector_Z));
    }
}
