// SPDX-License-Identifier: MIT

using ByteSerialization.Nodes;
using SWE1R.Assets.Blocks.ModelBlock;
using SWE1R.Assets.Blocks.ModelBlock.Nodes;
using System.ComponentModel.DataAnnotations.Schema;

namespace SWE1R.Assets.Blocks.Original.SQLite.Entities.ModelBlock.Nodes
{
    [Table($"{nameof(Model)}_{nameof(FlaggedNode)}_{nameof(MeshGroupNode)}")]
    public class DbMeshGroupNode : DbNode<MeshGroupNode>
    {
        #region Properties

        public float Aabb_Min_X { get; set; }
        public float Aabb_Min_Y { get; set; }
        public float Aabb_Min_Z { get; set; }

        public float Aabb_Max_X { get; set; }
        public float Aabb_Max_Y { get; set; }
        public float Aabb_Max_Z { get; set; }

        #endregion

        public override void CopyFrom(Node node)
        {
            base.CopyFrom(node);

            var x = (MeshGroupNode)node.Value;

            Aabb_Min_X = x.Aabb.Min.X;
            Aabb_Min_Y = x.Aabb.Min.Y;
            Aabb_Min_Z = x.Aabb.Min.Z;

            Aabb_Max_X = x.Aabb.Max.X;
            Aabb_Max_Y = x.Aabb.Max.Y;
            Aabb_Max_Z = x.Aabb.Max.Z;
        }

        public override bool Equals(DbBlockItemStructure<MeshGroupNode> other)
        {
            var x = (DbMeshGroupNode)other;

            if (!base.Equals(x))
                return false;

            if (Aabb_Min_X != x.Aabb_Min_X) return false;
            if (Aabb_Min_Y != x.Aabb_Min_Y) return false;
            if (Aabb_Min_Z != x.Aabb_Min_Z) return false;

            if (Aabb_Max_X != x.Aabb_Max_X) return false;
            if (Aabb_Max_Y != x.Aabb_Max_Y) return false;
            if (Aabb_Max_Z != x.Aabb_Max_Z) return false;

            return true;
        }

        public override bool Equals(object obj)
        {
            if (obj is DbMeshGroupNode x)
                return Equals(x);
            else
                return base.Equals(obj);
        }

        public override int GetHashCode() =>
            CombineHashCodes(base.GetHashCode(), 
                Aabb_Min_X, Aabb_Min_Y, Aabb_Min_Z, 
                Aabb_Max_X, Aabb_Max_Y, Aabb_Max_Z);
    }
}
