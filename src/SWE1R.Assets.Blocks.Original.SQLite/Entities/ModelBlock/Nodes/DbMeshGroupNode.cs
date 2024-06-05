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
        public float Aabb_Min_X { get; set; }
        public float Aabb_Min_Y { get; set; }
        public float Aabb_Min_Z { get; set; }

        public float Aabb_Max_X { get; set; }
        public float Aabb_Max_Y { get; set; }
        public float Aabb_Max_Z { get; set; }

        public override void CopyFrom(Node node)
        {
            base.CopyFrom(node);

            var n = (MeshGroupNode)node.Value;

            Aabb_Min_X = n.Aabb.Min.X;
            Aabb_Min_Y = n.Aabb.Min.Y;
            Aabb_Min_Z = n.Aabb.Min.Z;

            Aabb_Max_X = n.Aabb.Max.X;
            Aabb_Max_Y = n.Aabb.Max.Y;
            Aabb_Max_Z = n.Aabb.Max.Z;
        }

        public override bool Equals(DbBlockItemStructure<MeshGroupNode> other)
        {
            var _other = (DbMeshGroupNode)other;

            if (!base.Equals(_other))
                return false;

            if (Aabb_Min_X != _other.Aabb_Min_X) return false;
            if (Aabb_Min_Y != _other.Aabb_Min_Y) return false;
            if (Aabb_Min_Z != _other.Aabb_Min_Z) return false;

            if (Aabb_Max_X != _other.Aabb_Max_X) return false;
            if (Aabb_Max_Y != _other.Aabb_Max_Y) return false;
            if (Aabb_Max_Z != _other.Aabb_Max_Z) return false;

            return true;
        }

        public override bool Equals(object obj)
        {
            if (obj is DbMeshGroupNode)
                return this.Equals((DbMeshGroupNode)obj);
            else
                return base.Equals(obj);
        }

        public override int GetHashCode() =>
            HashCode.Combine(base.GetHashCode(), 
                Aabb_Min_X, Aabb_Min_Y, Aabb_Min_Z, 
                Aabb_Max_X, Aabb_Max_Y, Aabb_Max_Z);
    }
}
