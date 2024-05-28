// Copyright 2024 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization.Nodes;
using SWE1R.Assets.Blocks.ModelBlock.Meshes;
using System.ComponentModel.DataAnnotations.Schema;

namespace SWE1R.Assets.Blocks.Original.SQLite.Entities.ModelBlock.Meshes
{
    [Table("Model_Mapping")]
    public class DbMapping : DbBlockItemStructure<Mapping>
    {
        public short Word_00 { get; set; }

        public byte FogFlags { get; set; }

        public byte FogColor_R { get; set; }
        public byte FogColor_G { get; set; }
        public byte FogColor_B { get; set; }

        public int FogStart { get; set; } // ushort
        public int FogEnd { get; set; } // ushort
        public int LightFlags { get; set; } // ushort

        public byte AmbientColor_R { get; set; }
        public byte AmbientColor_G { get; set; }
        public byte AmbientColor_B { get; set; }
        
        public byte LightColor_R { get; set; }
        public byte LightColor_G { get; set; }
        public byte LightColor_B { get; set; }

        public byte Byte_12 { get; set; }
        public byte Byte_13 { get; set; }

        public float LightVector_X { get; set; }
        public float LightVector_Y { get; set; }
        public float LightVector_Z { get; set; }

        public float Float_20 { get; set; }
        public float Float_24 { get; set; }

        public VehicleReaction VehicleReaction { get; set; }
        
        public int Word_30 { get; set; } // ushort
        public int Word_32 { get; set; } // ushort
        
        public override void CopyFrom(Node node)
        {
            base.CopyFrom(node);

            var m = (Mapping)node.Value;

            Word_00 = m.Word_00;
            FogFlags = m.FogFlags;
            FogColor_R = m.FogColor.X;
            FogColor_G = m.FogColor.Y;
            FogColor_B = m.FogColor.Z;
            FogStart = m.FogStart;
            FogEnd = m.FogEnd;
            LightFlags = m.LightFlags;
            AmbientColor_R = m.AmbientColor.X;
            AmbientColor_G = m.AmbientColor.Y;
            AmbientColor_B = m.AmbientColor.Z;
            LightColor_R = m.LightColor.X;
            LightColor_G = m.LightColor.Y;
            LightColor_B = m.LightColor.Z;
            Byte_12 = m.Byte_12;
            Byte_13 = m.Byte_13;
            LightVector_X = m.LightVector.X;
            LightVector_Y = m.LightVector.Y;
            LightVector_Z = m.LightVector.Z;
            Float_20 = m.Float_20;
            Float_24 = m.Float_24;
            VehicleReaction = m.VehicleReaction;
            Word_30 = m.Word_30;
            Word_32 = m.Word_32;
        }

        public override bool Equals(DbBlockItemStructure<Mapping> other)
        {
            var _other = (DbMapping)other;

            if (!base.Equals(_other))
                return false;

            if (Word_00 != _other.Word_00) return false;
            if (FogFlags != _other.FogFlags) return false;
            if (FogColor_R != _other.FogColor_R) return false;
            if (FogColor_G != _other.FogColor_G) return false;
            if (FogColor_B != _other.FogColor_B) return false;
            if (FogStart != _other.FogStart) return false;
            if (FogEnd != _other.FogEnd) return false;
            if (LightFlags != _other.LightFlags) return false;
            if (AmbientColor_R != _other.AmbientColor_R) return false;
            if (AmbientColor_G != _other.AmbientColor_G) return false;
            if (AmbientColor_B != _other.AmbientColor_B) return false;
            if (LightColor_R != _other.LightColor_R) return false;
            if (LightColor_G != _other.LightColor_G) return false;
            if (LightColor_B != _other.LightColor_B) return false;
            if (Byte_12 != _other.Byte_12) return false;
            if (Byte_13 != _other.Byte_13) return false;
            if (LightVector_X != _other.LightVector_X) return false;
            if (LightVector_Y != _other.LightVector_Y) return false;
            if (LightVector_Z != _other.LightVector_Z) return false;
            if (Float_20 != _other.Float_20) return false;
            if (Float_24 != _other.Float_24) return false;
            if (VehicleReaction != _other.VehicleReaction) return false;
            if (Word_30 != _other.Word_30) return false;
            if (Word_32 != _other.Word_32) return false;

            return true;
        }

        public override bool Equals(object obj)
        {
            if (obj is DbMapping)
                return this.Equals((DbMapping)obj);
            else
                return base.Equals(obj);
        }

        public override int GetHashCode() =>
            HashCode.Combine(base.GetHashCode(),
                HashCode.Combine(Word_00, FogFlags, FogColor_R, FogColor_G, FogColor_B, FogStart, FogEnd, LightFlags),
                HashCode.Combine(AmbientColor_R, AmbientColor_G, AmbientColor_B, LightColor_R, LightColor_G, LightColor_B, Byte_12, Byte_13),
                HashCode.Combine(LightVector_X, LightVector_Y, LightVector_Z, Float_20, Float_24, VehicleReaction, Word_30, Word_32));
    }
}
