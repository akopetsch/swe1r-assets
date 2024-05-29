// SPDX-License-Identifier: MIT

using ByteSerialization.Nodes;
using SWE1R.Assets.Blocks.ModelBlock.Nodes;
using System.ComponentModel.DataAnnotations.Schema;

namespace SWE1R.Assets.Blocks.Original.SQLite.Entities.ModelBlock.Nodes
{
    [Table("Model_NodeD065")]
    public class DbNodeD065 : DbNode<TransformableD065>
    {
        public float Matrix_11 { get; set; }
        public float Matrix_12 { get; set; }
        public float Matrix_13 { get; set; }
        public float Matrix_14 { get; set; }
        public float Matrix_21 { get; set; }
        public float Matrix_22 { get; set; }
        public float Matrix_23 { get; set; }
        public float Matrix_24 { get; set; }
        public float Matrix_31 { get; set; }
        public float Matrix_32 { get; set; }
        public float Matrix_33 { get; set; }
        public float Matrix_34 { get; set; }
        public float Vector_X { get; set; }
        public float Vector_Y { get; set; }
        public float Vector_Z { get; set; }
        
        public override void CopyFrom(Node node)
        {
            base.CopyFrom(node);

            var n = (TransformableD065)node.Value;

            Matrix_11 = n.Matrix[0, 0];
            Matrix_12 = n.Matrix[0, 1];
            Matrix_13 = n.Matrix[0, 2];
            Matrix_14 = n.Matrix[1, 3];
            Matrix_21 = n.Matrix[1, 0];
            Matrix_22 = n.Matrix[1, 1];
            Matrix_23 = n.Matrix[1, 2];
            Matrix_24 = n.Matrix[1, 3];
            Matrix_31 = n.Matrix[2, 0];
            Matrix_32 = n.Matrix[2, 1];
            Matrix_33 = n.Matrix[2, 2];
            Matrix_34 = n.Matrix[2, 3];
            Vector_X = n.Vector.X;
            Vector_Y = n.Vector.Y;
            Vector_Z = n.Vector.Z;
        }

        public override bool Equals(DbBlockItemStructure<TransformableD065> other)
        {
            var _other = (DbNodeD065)other;

            if (!base.Equals(_other))
                return false;

            if (Matrix_11 != _other.Matrix_11) return false;
            if (Matrix_12 != _other.Matrix_12) return false;
            if (Matrix_13 != _other.Matrix_13) return false;
            if (Matrix_14 != _other.Matrix_14) return false;
            if (Matrix_21 != _other.Matrix_21) return false;
            if (Matrix_22 != _other.Matrix_22) return false;
            if (Matrix_23 != _other.Matrix_23) return false;
            if (Matrix_24 != _other.Matrix_24) return false;
            if (Matrix_31 != _other.Matrix_31) return false;
            if (Matrix_32 != _other.Matrix_32) return false;
            if (Matrix_33 != _other.Matrix_33) return false;
            if (Matrix_34 != _other.Matrix_34) return false;
            if (Vector_X != _other.Vector_X) return false;
            if (Vector_Y != _other.Vector_Y) return false;
            if (Vector_Z != _other.Vector_Z) return false;

            return true;
        }

        public override bool Equals(object obj)
        {
            if (obj is DbNodeD065)
                return this.Equals((DbNodeD065)obj);
            else
                return base.Equals(obj);
        }

        public override int GetHashCode() =>
            HashCode.Combine(base.GetHashCode(),
                HashCode.Combine(Matrix_11, Matrix_12, Matrix_13, Matrix_14),
                HashCode.Combine(Matrix_21, Matrix_22, Matrix_23, Matrix_24),
                HashCode.Combine(Matrix_31, Matrix_32, Matrix_33, Matrix_34),
                HashCode.Combine(Vector_X, Vector_Y, Vector_Z));
    }
}
