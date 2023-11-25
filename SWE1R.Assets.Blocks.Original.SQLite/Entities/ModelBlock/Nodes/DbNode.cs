// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization.Nodes;
using SWE1R.Assets.Blocks.ModelBlock.Nodes;

namespace SWE1R.Assets.Blocks.Original.SQLite.Entities.ModelBlock.Nodes
{
    public abstract class DbNode<TSource> : DbBlockItemStructure<TSource> 
        where TSource : FlaggedNode
    {
        public int Bitfield1 { get; set; }
        public int Bitfield2 { get; set; }
        public short Number { get; set; }
        public short Padding1 { get; set; }
        public int Padding2 { get; set; }
        public int ChildrenCount { get; set; }
        public int P_Children { get; set; }

        public override void CopyFrom(Node node)
        {
            base.CopyFrom(node);

            var n = (FlaggedNode)node.Value;

            Bitfield1 = n.Bitfield1;
            Bitfield2 = n.Bitfield2;
            Number = n.Number;
            Padding1 = n.Padding1;
            Padding2 = n.Padding2;
            ChildrenCount = n.Children?.Count ?? 0;
            P_Children = GetPropertyPointer(node, nameof(n.Children));
        }

        public override bool Equals(DbBlockItemStructure<TSource> other)
        {
            var _other = (DbNode<TSource>)other;

            if (!base.Equals(_other))
                return false;

            if (Bitfield1 != _other.Bitfield1) return false;
            if (Bitfield2 != _other.Bitfield2) return false;
            if (Number != _other.Number) return false;
            if (Padding1 != _other.Padding1) return false;
            if (Padding2 != _other.Padding2) return false;
            if (ChildrenCount != _other.ChildrenCount) return false;
            if (P_Children != _other.P_Children) return false;

            return true;
        }

        public override int GetHashCode() =>
            HashCode.Combine(base.GetHashCode(), Bitfield1, Bitfield2, 
                Number, Padding1, Padding2, ChildrenCount, P_Children);
    }
}
