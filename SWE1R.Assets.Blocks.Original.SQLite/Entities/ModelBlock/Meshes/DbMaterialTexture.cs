// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization.Nodes;
using SWE1R.Assets.Blocks.ModelBlock.Meshes;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;

namespace SWE1R.Assets.Blocks.Original.SQLite.Entities.ModelBlock.Meshes
{
    [DebuggerDisplay("{IdField,nq}")]
    [Table("Model_MaterialTexture")]
    public class DbMaterialTexture : DbModelStructure<MaterialTexture>, IEquatable<DbMaterialTexture>
    {
        public int Mask_Unk { get; set; }
        public short Width4 { get; set; }
        public short Height4 { get; set; }
        public short Always0_08 { get; set; }
        public short Always0_0a { get; set; }
        public byte Byte_0c { get; set; }
        public byte Byte_0d { get; set; }
        public short Word_0e { get; set; }
        public short Width { get; set; }
        public short Height { get; set; }
        public int Width_Unk { get; set; } // ushort
        public int Height_Unk { get; set; } // ushort
        public short Flags { get; set; }
        public short Mask { get; set; }
        public int P_Child0 { get; set; }
        public int P_Child1 { get; set; }
        public int P_Child2 { get; set; }
        public int P_Child3 { get; set; }
        public int P_Child4 { get; set; }
        public int IdField { get; set; }
        
        public override void CopyFrom(Node node)
        {
            base.CopyFrom(node);

            var mt = (MaterialTexture)node.Value;

            Mask_Unk = mt.Mask_Unk;
            Width4 = mt.Width4;
            Height4 = mt.Height4;
            Always0_08 = mt.Always0_08;
            Always0_0a = mt.Always0_0a;
            Byte_0c = mt.Byte_0c;
            Byte_0d = mt.Byte_0d;
            Word_0e = mt.Word_0e;
            Width = mt.Width;
            Height = mt.Height;
            Width_Unk = mt.Width_Unk;
            Height_Unk = mt.Height_Unk;
            Flags = mt.Flags;
            Mask = mt.Mask;
            P_Child0 = (int)(node.Context.Graph.GetValueComponent(mt.Children[0])?.Position ?? 0);
            P_Child1 = (int)(node.Context.Graph.GetValueComponent(mt.Children[1])?.Position ?? 0);
            P_Child2 = (int)(node.Context.Graph.GetValueComponent(mt.Children[2])?.Position ?? 0);
            P_Child3 = (int)(node.Context.Graph.GetValueComponent(mt.Children[3])?.Position ?? 0);
            P_Child4 = (int)(node.Context.Graph.GetValueComponent(mt.Children[4])?.Position ?? 0);
            IdField = mt.TextureIndex;
        }

        public bool Equals(DbMaterialTexture other)
        {
            if (!base.Equals(other))
                return false;

            if (Mask_Unk != other.Mask_Unk) return false;
            if (Width4 != other.Width4)
            if (Height4 != other.Height4) return false;
            if (Always0_08 != other.Always0_08) return false;
            if (Always0_0a != other.Always0_0a) return false;
            if (Byte_0c != other.Byte_0c) return false;
            if (Byte_0d != other.Byte_0d) return false;
            if (Word_0e != other.Word_0e) return false;
            if (Width != other.Width) return false;
            if (Height != other.Height) return false;
            if (Width_Unk != other.Width_Unk) return false;
            if (Height_Unk != other.Height_Unk) return false;
            if (Flags != other.Flags) return false;
            if (Mask != other.Mask) return false;
            if (P_Child0 != other.P_Child0) return false;
            if (P_Child1 != other.P_Child1) return false;
            if (P_Child2 != other.P_Child2) return false;
            if (P_Child3 != other.P_Child3) return false;
            if (P_Child4 != other.P_Child4) return false;
            if (IdField != other.IdField) return false;

            return true;
        }

        public override bool Equals(object obj)
        {
            if (obj is DbMaterialTexture)
                return this.Equals((DbMaterialTexture)obj);
            else
                return base.Equals(obj);
        }

        public override int GetHashCode() =>
            HashCode.Combine(base.GetHashCode(),
                HashCode.Combine(Mask_Unk, Width4, Height4, Always0_08, Always0_0a, Byte_0c, Byte_0d, Word_0e),
                HashCode.Combine(Width, Height, Width_Unk, Height_Unk, Flags, Mask),
                HashCode.Combine(P_Child0, P_Child1, P_Child2, P_Child3, P_Child4));
    }
}
