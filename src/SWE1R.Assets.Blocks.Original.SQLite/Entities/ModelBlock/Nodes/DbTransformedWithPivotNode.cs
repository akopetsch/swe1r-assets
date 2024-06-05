// SPDX-License-Identifier: MIT

using ByteSerialization.Nodes;
using SWE1R.Assets.Blocks.ModelBlock;
using SWE1R.Assets.Blocks.ModelBlock.Nodes;
using System.ComponentModel.DataAnnotations.Schema;

namespace SWE1R.Assets.Blocks.Original.SQLite.Entities.ModelBlock.Nodes
{
    [Table($"{nameof(Model)}_{nameof(FlaggedNode)}_{nameof(TransformedWithPivotNode)}")]
    public class DbTransformedWithPivotNode : DbNode<TransformedWithPivotNode>
    {
        public float Transform_11 { get; set; }
        public float Transform_12 { get; set; }
        public float Transform_13 { get; set; }
        public float Transform_14 { get; set; }
        public float Transform_21 { get; set; }
        public float Transform_22 { get; set; }
        public float Transform_23 { get; set; }
        public float Transform_24 { get; set; }
        public float Transform_31 { get; set; }
        public float Transform_32 { get; set; }
        public float Transform_33 { get; set; }
        public float Transform_34 { get; set; }
        public float Pivot_X { get; set; }
        public float Pivot_Y { get; set; }
        public float Pivot_Z { get; set; }
        
        public override void CopyFrom(Node node)
        {
            base.CopyFrom(node);

            var n = (TransformedWithPivotNode)node.Value;

            Transform_11 = n.Transform[0, 0];
            Transform_12 = n.Transform[0, 1];
            Transform_13 = n.Transform[0, 2];
            Transform_14 = n.Transform[1, 3];
            Transform_21 = n.Transform[1, 0];
            Transform_22 = n.Transform[1, 1];
            Transform_23 = n.Transform[1, 2];
            Transform_24 = n.Transform[1, 3];
            Transform_31 = n.Transform[2, 0];
            Transform_32 = n.Transform[2, 1];
            Transform_33 = n.Transform[2, 2];
            Transform_34 = n.Transform[2, 3];
            Pivot_X = n.Pivot.X;
            Pivot_Y = n.Pivot.Y;
            Pivot_Z = n.Pivot.Z;
        }

        public override bool Equals(DbBlockItemStructure<TransformedWithPivotNode> other)
        {
            var _other = (DbTransformedWithPivotNode)other;

            if (!base.Equals(_other))
                return false;

            if (Transform_11 != _other.Transform_11) return false;
            if (Transform_12 != _other.Transform_12) return false;
            if (Transform_13 != _other.Transform_13) return false;
            if (Transform_14 != _other.Transform_14) return false;
            if (Transform_21 != _other.Transform_21) return false;
            if (Transform_22 != _other.Transform_22) return false;
            if (Transform_23 != _other.Transform_23) return false;
            if (Transform_24 != _other.Transform_24) return false;
            if (Transform_31 != _other.Transform_31) return false;
            if (Transform_32 != _other.Transform_32) return false;
            if (Transform_33 != _other.Transform_33) return false;
            if (Transform_34 != _other.Transform_34) return false;
            if (Pivot_X != _other.Pivot_X) return false;
            if (Pivot_Y != _other.Pivot_Y) return false;
            if (Pivot_Z != _other.Pivot_Z) return false;

            return true;
        }

        public override bool Equals(object obj)
        {
            if (obj is DbTransformedWithPivotNode)
                return this.Equals((DbTransformedWithPivotNode)obj);
            else
                return base.Equals(obj);
        }

        public override int GetHashCode() =>
            HashCode.Combine(base.GetHashCode(),
                HashCode.Combine(Transform_11, Transform_12, Transform_13, Transform_14),
                HashCode.Combine(Transform_21, Transform_22, Transform_23, Transform_24),
                HashCode.Combine(Transform_31, Transform_32, Transform_33, Transform_34),
                HashCode.Combine(Pivot_X, Pivot_Y, Pivot_Z));
    }
}
