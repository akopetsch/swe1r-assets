﻿// SPDX-License-Identifier: MIT

using ByteSerialization.Nodes;
using SWE1R.Assets.Blocks.ModelBlock;
using SWE1R.Assets.Blocks.ModelBlock.Behaviours;
using System.ComponentModel.DataAnnotations.Schema;

namespace SWE1R.Assets.Blocks.Original.SQLite.Entities.ModelBlock.Behaviours
{
    [Table($"{nameof(Model)}_{nameof(TriggerDescription)}")]
    public class DbTriggerDescription : DbBlockItemStructure<TriggerDescription>
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
        public int P_AffectedNode { get; set; }
        public TriggerType TriggerType { get; set; }
        public short Word_26 { get; set; }
        public int P_Next { get; set; }

        #endregion

        public override void CopyFrom(Node node)
        {
            base.CopyFrom(node);

            var x = (TriggerDescription)node.Value;

            Vector_00_X = x.Center.X;
            Vector_00_Y = x.Center.Y;
            Vector_00_Z = x.Center.Z;
            Vector_0c_X = x.Direction.X;
            Vector_0c_Y = x.Direction.Y;
            Vector_0c_Z = x.Direction.Z;
            Word_18 = x.Word_18;
            Byte_1a = x.Byte_1a;
            Byte_1b = x.Byte_1b;
            Word_1c = x.Word_1c;
            Byte_1e = x.Byte_1e;
            Byte_1f = x.Byte_1f;
            P_AffectedNode = GetPropertyPointer(node, nameof(x.AffectedNode));
            TriggerType = x.TriggerType;
            Word_26 = x.Word_26;
            P_Next = GetPropertyPointer(node, nameof(x.Next));
        }

        public override bool Equals(DbBlockItemStructure<TriggerDescription> other)
        {
            var x = (DbTriggerDescription)other;

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
            if (P_AffectedNode != x.P_AffectedNode) return false;
            if (TriggerType != x.TriggerType) return false;
            if (Word_26 != x.Word_26) return false;
            if (P_Next != x.P_Next) return false;

            return true;
        }

        public override bool Equals(object obj)
        {
            if (obj is DbTriggerDescription x)
                return Equals(x);
            else
                return base.Equals(obj);
        }

        public override int GetHashCode() =>
            CombineHashCodes(base.GetHashCode(),
                Vector_00_X, Vector_00_Y, Vector_00_Z,
                Vector_0c_X, Vector_0c_Y, Vector_0c_Z,
                Word_18, Byte_1a, Byte_1b, Word_1c, Byte_1e, Byte_1f,
                P_AffectedNode, TriggerType, Word_26, P_Next);
    }
}