// SPDX-License-Identifier: MIT

using ByteSerialization.Nodes;
using SWE1R.Assets.Blocks.ModelBlock;
using SWE1R.Assets.Blocks.ModelBlock.Materials;
using SWE1R.Assets.Blocks.Textures;
using System.ComponentModel.DataAnnotations.Schema;

namespace SWE1R.Assets.Blocks.Original.SQLite.Entities.ModelBlock.Materials
{
    [Table($"{nameof(Model)}_{nameof(MaterialTexture)}")]
    public class DbMaterialTexture : DbBlockItemStructure<MaterialTexture>, IEquatable<DbMaterialTexture>
    {
        #region Properties

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
        public int P_Children_0 { get; set; }
        public int P_Children_1 { get; set; }
        public int P_Children_2 { get; set; }
        public int P_Children_3 { get; set; }
        public int P_Children_4 { get; set; }
        public int I_TextureIndex { get; set; }

        #endregion

        public override void CopyFrom(Node node)
        {
            base.CopyFrom(node);

            var x = (MaterialTexture)node.Value;

            Mask_Unk = x.Mask_Unk;
            Width4 = x.Width4;
            Height4 = x.Height4;
            Always0_08 = x.Always0_08;
            Always0_0a = x.Always0_0a;
            Byte_0c = x.Format.GetByte1();
            Byte_0d = x.Format.GetByte2();
            Word_0e = x.Word_0e;
            Width = x.Width;
            Height = x.Height;
            Width_Unk = x.Width_Unk;
            Height_Unk = x.Height_Unk;
            Flags = x.Flags;
            Mask = x.Mask;
            P_Children_0 = GetValuePosition(node.Context.Graph, x.Children[0]);
            P_Children_1 = GetValuePosition(node.Context.Graph, x.Children[1]);
            P_Children_2 = GetValuePosition(node.Context.Graph, x.Children[2]);
            P_Children_3 = GetValuePosition(node.Context.Graph, x.Children[3]);
            P_Children_4 = GetValuePosition(node.Context.Graph, x.Children[4]);
            I_TextureIndex = x.TextureIndex.SerializedValue;
        }

        public bool Equals(DbMaterialTexture x)
        {
            if (!base.Equals(x))
                return false;

            if (Mask_Unk != x.Mask_Unk) return false;
            if (Width4 != x.Width4)
                if (Height4 != x.Height4) return false;
            if (Always0_08 != x.Always0_08) return false;
            if (Always0_0a != x.Always0_0a) return false;
            if (Byte_0c != x.Byte_0c) return false;
            if (Byte_0d != x.Byte_0d) return false;
            if (Word_0e != x.Word_0e) return false;
            if (Width != x.Width) return false;
            if (Height != x.Height) return false;
            if (Width_Unk != x.Width_Unk) return false;
            if (Height_Unk != x.Height_Unk) return false;
            if (Flags != x.Flags) return false;
            if (Mask != x.Mask) return false;
            if (P_Children_0 != x.P_Children_0) return false;
            if (P_Children_1 != x.P_Children_1) return false;
            if (P_Children_2 != x.P_Children_2) return false;
            if (P_Children_3 != x.P_Children_3) return false;
            if (P_Children_4 != x.P_Children_4) return false;
            if (I_TextureIndex != x.I_TextureIndex) return false;

            return true;
        }

        public override bool Equals(object obj)
        {
            if (obj is DbMaterialTexture x)
                return Equals(x);
            else
                return base.Equals(obj);
        }

        public override int GetHashCode() =>
            CombineHashCodes(base.GetHashCode(),
                Mask_Unk, Width4, Height4, Always0_08, Always0_0a, Byte_0c, Byte_0d, Word_0e,
                Width, Height, Width_Unk, Height_Unk, Flags, Mask,
                P_Children_0, P_Children_1, P_Children_2, P_Children_3, P_Children_4);
    }
}
