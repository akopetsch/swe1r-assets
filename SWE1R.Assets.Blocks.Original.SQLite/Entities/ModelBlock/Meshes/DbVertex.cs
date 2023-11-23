// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization.Nodes;
using SWE1R.Assets.Blocks.ModelBlock.Meshes;
using System.ComponentModel.DataAnnotations.Schema;

namespace SWE1R.Assets.Blocks.Original.SQLite.Entities.ModelBlock.Meshes
{
    [Table("Model_Vertex")]
    public class DbVertex : DbModelStructure<Vertex>
    {
        public short P_X { get; set; }
        public short P_Y { get; set; }
        public short P_Z { get; set; }

        public short U { get; set; }
        public short V { get; set; }

        public short C_R { get; set; }
        public short C_G { get; set; }
        public short C_B { get; set; }

        public byte C_A { get; set; }

        public override void CopyFrom(Node node)
        {
            base.CopyFrom(node);

            var v = (Vertex)node.Value;

            P_X = v.Position.X;
            P_Y = v.Position.Y;
            P_Z = v.Position.Z;

            U = v.U;
            V = v.V;

            C_R = v.Color.R;
            C_G = v.Color.G;
            C_B = v.Color.B;
            C_A = v.Color.A;
        }

        public override bool Equals(DbModelStructure<Vertex> other)
        {
            var _other = (DbVertex)other;

            if (!base.Equals(_other))
                return false;

            if (P_X != _other.P_X) return false;
            if (P_Y != _other.P_Y) return false;
            if (P_Z != _other.P_Z) return false;

            if (U != _other.U) return false;
            if (V != _other.V) return false;

            if (C_R != _other.C_R) return false;
            if (C_G != _other.C_G) return false;
            if (C_B != _other.C_B) return false;
            if (C_A != _other.C_A) return false;

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
                HashCode.Combine(C_R, C_G, C_B, C_A));
    }
}
