// SPDX-License-Identifier: MIT

using ByteSerialization.Nodes;
using SWE1R.Assets.Blocks.ModelBlock;
using SWE1R.Assets.Blocks.ModelBlock.Meshes;
using System.ComponentModel.DataAnnotations.Schema;

namespace SWE1R.Assets.Blocks.Original.SQLite.Entities.ModelBlock.Meshes
{
    [Table($"{nameof(Model)}_{nameof(MappingChild)}")]
    public class DbMappingChild : DbBlockItemStructure<MappingChild>
    {
        #region Properties

        public float Vector_00_X { get; set; }
        public float Vector_00_Y { get; set; }
        public float Vector_00_Z { get; set; }
        public float Vector_0c_X { get; set; }
        public float Vector_0c_Y { get; set; }
        public float Vector_0c_Z { get; set; }
        public short Word_18 { get; set; }
        public byte Byte_1a { get; set; }
        public byte Byte_1b { get; set; }
        public short Word_1c { get; set; }
        public byte Byte_1e { get; set; }
        public byte Byte_1f { get; set; }
        public int P_FlaggedNode_20 { get; set; }
        public short Word_24 { get; set; }
        public short Word_26 { get; set; }
        public int P_Next { get; set; }

        #endregion

        public override void CopyFrom(Node node)
        {
            base.CopyFrom(node);

            var x = (MappingChild)node.Value;

            Vector_00_X = x.Vector_00.X;
            Vector_00_Y = x.Vector_00.Y;
            Vector_00_Z = x.Vector_00.Z;
            Vector_0c_X = x.Vector_0c.X;
            Vector_0c_Y = x.Vector_0c.Y;
            Vector_0c_Z = x.Vector_0c.Z;
            Word_18 = x.Word_18;
            Byte_1a = x.Byte_1a;
            Byte_1b = x.Byte_1b;
            Word_1c = x.Word_1c;
            Byte_1e = x.Byte_1e;
            Byte_1f = x.Byte_1f;
            P_FlaggedNode_20 = GetPropertyPointer(node, nameof(x.FlaggedNode_20));
            Word_24 = x.Word_24;
            Word_26 = x.Word_26;
            P_Next = GetPropertyPointer(node, nameof(x.Next));
        }

        public override bool Equals(DbBlockItemStructure<MappingChild> other)
        {
            var x = (DbMappingChild)other;

            if (!base.Equals(x))
                return false;

            if (Vector_00_X != x.Vector_00_X) return false;
            if (Vector_00_Y != x.Vector_00_Y) return false;
            if (Vector_00_Z != x.Vector_00_Z) return false;
            if (Vector_0c_X != x.Vector_0c_X) return false;
            if (Vector_0c_Y != x.Vector_0c_Y) return false;
            if (Vector_0c_Z != x.Vector_0c_Z) return false;
            if (Word_18 != x.Word_18) return false;
            if (Byte_1a != x.Byte_1a) return false;
            if (Byte_1b != x.Byte_1b) return false;
            if (Word_1c != x.Word_1c) return false;
            if (Byte_1e != x.Byte_1e) return false;
            if (Byte_1f != x.Byte_1f) return false;
            if (P_FlaggedNode_20 != x.P_FlaggedNode_20) return false;
            if (Word_24 != x.Word_24) return false;
            if (Word_26 != x.Word_26) return false;
            if (P_Next != x.P_Next) return false;

            return true;
        }

        public override bool Equals(object obj)
        {
            if (obj is DbMappingChild x)
                return Equals(x);
            else
                return base.Equals(obj);
        }

        public override int GetHashCode() =>
            CombineHashCodes(base.GetHashCode(),
                Vector_00_X, Vector_00_Y, Vector_00_Z, Vector_0c_Y, Vector_0c_Z, Word_18, Byte_1a, Byte_1b,
                Word_1c, Byte_1e, Byte_1f, P_FlaggedNode_20, Word_24, Word_26, P_Next);
    }
}
