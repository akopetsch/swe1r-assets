// SPDX-License-Identifier: GPL-2.0-only

using ByteSerialization.Nodes;
using SWE1R.Assets.Blocks.ModelBlock.Meshes;
using System.ComponentModel.DataAnnotations.Schema;

namespace SWE1R.Assets.Blocks.Original.SQLite.Entities.ModelBlock.Meshes
{
    [Table("Model_MappingChild")]
    public class DbMappingChild : DbBlockItemStructure<MappingChild>
    {
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

        public override void CopyFrom(Node node)
        {
            base.CopyFrom(node);

            var m = (MappingChild)node.Value;

            Vector_00_X = m.Vector_00.X;
            Vector_00_Y = m.Vector_00.Y;
            Vector_00_Z = m.Vector_00.Z;
            Vector_0c_X = m.Vector_0c.X;
            Vector_0c_Y = m.Vector_0c.Y;
            Vector_0c_Z = m.Vector_0c.Z;
            Word_18 = m.Word_18;
            Byte_1a = m.Byte_1a;
            Byte_1b = m.Byte_1b;
            Word_1c = m.Word_1c;
            Byte_1e = m.Byte_1e;
            Byte_1f = m.Byte_1f;
            P_FlaggedNode_20 = GetPropertyPointer(node, nameof(m.FlaggedNode_20));
             Word_24 = m.Word_24;
            Word_26 = m.Word_26;
            P_Next = GetPropertyPointer(node, nameof(m.Next));
        }

        public override bool Equals(DbBlockItemStructure<MappingChild> other)
        {
            var _other = (DbMappingChild)other;

            if (!base.Equals(_other))
                return false;

            if (Vector_00_X != _other.Vector_00_X) return false;
            if (Vector_00_Y != _other.Vector_00_Y) return false;
            if (Vector_00_Z != _other.Vector_00_Z) return false;
            if (Vector_0c_X != _other.Vector_0c_X) return false;
            if (Vector_0c_Y != _other.Vector_0c_Y) return false;
            if (Vector_0c_Z != _other.Vector_0c_Z) return false;
            if (Word_18 != _other.Word_18) return false;
            if (Byte_1a != _other.Byte_1a) return false;
            if (Byte_1b != _other.Byte_1b) return false;
            if (Word_1c != _other.Word_1c) return false;
            if (Byte_1e != _other.Byte_1e) return false;
            if (Byte_1f != _other.Byte_1f) return false;
            if (P_FlaggedNode_20 != _other.P_FlaggedNode_20) return false;
            if (Word_24 != _other.Word_24) return false;
            if (Word_26 != _other.Word_26) return false;
            if (P_Next != _other.P_Next) return false;

            return true;
        }

        public override bool Equals(object obj)
        {
            if (obj is DbMappingChild)
                return this.Equals((DbMappingChild)obj);
            else
                return base.Equals(obj);
        }

        public override int GetHashCode() =>
            HashCode.Combine(base.GetHashCode(),
                HashCode.Combine(Vector_00_X, Vector_00_Y, Vector_00_Z, Vector_0c_Y, Vector_0c_Z, Word_18, Byte_1a, Byte_1b),
                HashCode.Combine(Word_1c, Byte_1e, Byte_1f, P_FlaggedNode_20, Word_24, Word_26, P_Next));
    }
}
