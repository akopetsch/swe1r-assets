// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization.Nodes;
using SWE1R.Assets.Blocks.ModelBlock.Nodes;
using System.ComponentModel.DataAnnotations.Schema;

namespace SWE1R.Assets.Blocks.Original.SQLite.Entities.ModelBlock.Nodes
{
    [Table("Model_Node3064")]
    public class DbNode3064 : DbNode<MeshGroup3064>
    {
        public float Bounds_Min_X { get; set; }
        public float Bounds_Min_Y { get; set; }
        public float Bounds_Min_Z { get; set; }

        public float Bounds_Max_X { get; set; }
        public float Bounds_Max_Y { get; set; }
        public float Bounds_Max_Z { get; set; }

        public override void CopyFrom(Node node)
        {
            base.CopyFrom(node);

            var n = (MeshGroup3064)node.Value;

            Bounds_Min_X = n.Bounds.Min.X;
            Bounds_Min_Y = n.Bounds.Min.Y;
            Bounds_Min_Z = n.Bounds.Min.Z;

            Bounds_Max_X = n.Bounds.Max.X;
            Bounds_Max_Y = n.Bounds.Max.Y;
            Bounds_Max_Z = n.Bounds.Max.Z;
        }

        public override bool Equals(DbModelStructure<MeshGroup3064> other)
        {
            var _other = (DbNode3064)other;

            if (!base.Equals(_other))
                return false;

            if (Bounds_Min_X != _other.Bounds_Min_X) return false;
            if (Bounds_Min_Y != _other.Bounds_Min_Y) return false;
            if (Bounds_Min_Z != _other.Bounds_Min_Z) return false;

            if (Bounds_Max_X != _other.Bounds_Max_X) return false;
            if (Bounds_Max_Y != _other.Bounds_Max_Y) return false;
            if (Bounds_Max_Z != _other.Bounds_Max_Z) return false;

            return true;
        }

        public override bool Equals(object obj)
        {
            if (obj is DbNode3064)
                return this.Equals((DbNode3064)obj);
            else
                return base.Equals(obj);
        }

        public override int GetHashCode() =>
            HashCode.Combine(base.GetHashCode(), 
                Bounds_Min_X, Bounds_Min_Y, Bounds_Min_Z, 
                Bounds_Max_X, Bounds_Max_Y, Bounds_Max_Z);
    }
}
