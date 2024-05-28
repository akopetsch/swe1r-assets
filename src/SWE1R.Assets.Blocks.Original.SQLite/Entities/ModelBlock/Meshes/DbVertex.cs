// Copyright 2024 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization.Nodes;
using SWE1R.Assets.Blocks.ModelBlock.Meshes;
using System.ComponentModel.DataAnnotations.Schema;

namespace SWE1R.Assets.Blocks.Original.SQLite.Entities.ModelBlock.Meshes
{
    [Table("Model_Vertex")]
    public class DbVertex : DbBlockItemStructure<Vertex>
    {
        public short P_X { get; set; }
        public short P_Y { get; set; }
        public short P_Z { get; set; }

        public short U { get; set; }
        public short V { get; set; }

        public short B_C { get; set; }
        public short B_D { get; set; }
        public short B_E { get; set; }

        public byte B_F { get; set; }

        public override void CopyFrom(Node node)
        {
            base.CopyFrom(node);

            var v = (Vertex)node.Value;

            P_X = v.Position.X;
            P_Y = v.Position.Y;
            P_Z = v.Position.Z;

            U = v.U;
            V = v.V;

            B_C = v.Byte_C;
            B_D = v.Byte_D;
            B_E = v.Byte_E;
            B_F = v.Byte_F;
        }

        public override bool Equals(DbBlockItemStructure<Vertex> other)
        {
            var _other = (DbVertex)other;

            if (!base.Equals(_other))
                return false;

            if (P_X != _other.P_X) return false;
            if (P_Y != _other.P_Y) return false;
            if (P_Z != _other.P_Z) return false;

            if (U != _other.U) return false;
            if (V != _other.V) return false;

            if (B_C != _other.B_C) return false;
            if (B_D != _other.B_D) return false;
            if (B_E != _other.B_E) return false;
            if (B_F != _other.B_F) return false;

            return true;
        }

        public override bool Equals(object obj)
        {
            if (obj is DbVertex)
                return this.Equals((DbVertex)obj);
            else
                return base.Equals(obj);
        }

        public override int GetHashCode() =>
            HashCode.Combine(base.GetHashCode(), 
                HashCode.Combine(P_X, P_Y, P_Z), 
                HashCode.Combine(U, V), 
                HashCode.Combine(B_C, B_D, B_E, B_F));
    }
}
