// SPDX-License-Identifier: MIT

using ByteSerialization.Nodes;
using SWE1R.Assets.Blocks.ModelBlock;
using SWE1R.Assets.Blocks.ModelBlock.Meshes;
using SWE1R.Assets.Blocks.ModelBlock.Meshes.Behaviours;
using System.ComponentModel.DataAnnotations.Schema;

namespace SWE1R.Assets.Blocks.Original.SQLite.Entities.ModelBlock.Meshes.Behaviours
{
    [Table($"{nameof(Model)}_{nameof(Mapping)}")]
    public class DbMapping : DbBlockItemStructure<Mapping>
    {
        #region Properties

        public short Word_00 { get; set; }

        public byte FogFlags { get; set; }

        public byte FogColor_X { get; set; }
        public byte FogColor_Y { get; set; }
        public byte FogColor_Z { get; set; }

        public int FogStart { get; set; } // ushort
        public int FogEnd { get; set; } // ushort
        public int LightFlags { get; set; } // ushort

        public byte AmbientColor_X { get; set; }
        public byte AmbientColor_Y { get; set; }
        public byte AmbientColor_Z { get; set; }

        public byte LightColor_X { get; set; }
        public byte LightColor_Y { get; set; }
        public byte LightColor_Z { get; set; }

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

        #endregion

        public override void CopyFrom(Node node)
        {
            base.CopyFrom(node);

            var x = (Mapping)node.Value;

            Word_00 = x.Word_00;
            FogFlags = x.FogFlags;
            FogColor_X = x.FogColor.X;
            FogColor_Y = x.FogColor.Y;
            FogColor_Z = x.FogColor.Z;
            FogStart = x.FogStart;
            FogEnd = x.FogEnd;
            LightFlags = x.LightFlags;
            AmbientColor_X = x.AmbientColor.X;
            AmbientColor_Y = x.AmbientColor.Y;
            AmbientColor_Z = x.AmbientColor.Z;
            LightColor_X = x.LightColor.X;
            LightColor_Y = x.LightColor.Y;
            LightColor_Z = x.LightColor.Z;
            Byte_12 = x.Byte_12;
            Byte_13 = x.Byte_13;
            LightVector_X = x.LightVector.X;
            LightVector_Y = x.LightVector.Y;
            LightVector_Z = x.LightVector.Z;
            Float_20 = x.Float_20;
            Float_24 = x.Float_24;
            VehicleReaction = x.VehicleReaction;
            Word_30 = x.Word_30;
            Word_32 = x.Word_32;
        }

        public override bool Equals(DbBlockItemStructure<Mapping> other)
        {
            var x = (DbMapping)other;

            if (!base.Equals(x))
                return false;

            if (Word_00 != x.Word_00) return false;
            if (FogFlags != x.FogFlags) return false;
            if (FogColor_X != x.FogColor_X) return false;
            if (FogColor_Y != x.FogColor_Y) return false;
            if (FogColor_Z != x.FogColor_Z) return false;
            if (FogStart != x.FogStart) return false;
            if (FogEnd != x.FogEnd) return false;
            if (LightFlags != x.LightFlags) return false;
            if (AmbientColor_X != x.AmbientColor_X) return false;
            if (AmbientColor_Y != x.AmbientColor_Y) return false;
            if (AmbientColor_Z != x.AmbientColor_Z) return false;
            if (LightColor_X != x.LightColor_X) return false;
            if (LightColor_Y != x.LightColor_Y) return false;
            if (LightColor_Z != x.LightColor_Z) return false;
            if (Byte_12 != x.Byte_12) return false;
            if (Byte_13 != x.Byte_13) return false;
            if (LightVector_X != x.LightVector_X) return false;
            if (LightVector_Y != x.LightVector_Y) return false;
            if (LightVector_Z != x.LightVector_Z) return false;
            if (Float_20 != x.Float_20) return false;
            if (Float_24 != x.Float_24) return false;
            if (VehicleReaction != x.VehicleReaction) return false;
            if (Word_30 != x.Word_30) return false;
            if (Word_32 != x.Word_32) return false;

            return true;
        }

        public override bool Equals(object obj)
        {
            if (obj is DbMapping x)
                return Equals(x);
            else
                return base.Equals(obj);
        }

        public override int GetHashCode() =>
            CombineHashCodes(base.GetHashCode(),
                Word_00, FogFlags, FogColor_X, FogColor_Y, FogColor_Z, FogStart, FogEnd, LightFlags,
                AmbientColor_X, AmbientColor_Y, AmbientColor_Z, LightColor_X, LightColor_Y, LightColor_Z, Byte_12, Byte_13,
                LightVector_X, LightVector_Y, LightVector_Z, Float_20, Float_24, VehicleReaction, Word_30, Word_32);
    }
}
