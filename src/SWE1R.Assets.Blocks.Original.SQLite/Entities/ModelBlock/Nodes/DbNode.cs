// SPDX-License-Identifier: MIT

using ByteSerialization.Nodes;
using SWE1R.Assets.Blocks.ModelBlock.Nodes;

namespace SWE1R.Assets.Blocks.Original.SQLite.Entities.ModelBlock.Nodes
{
    public abstract class DbNode<TSource> : DbBlockItemStructure<TSource> 
        where TSource : FlaggedNode
    {
        #region Properties

        public int Flags1 { get; set; }
        public int Flags2 { get; set; }
        public short Flags3 { get; set; }
        public short LightIndex { get; set; }
        public int Flags5 { get; set; }
        public int Children_Count { get; set; }
        public int P_Children { get; set; }

        #endregion

        public override void CopyFrom(Node node)
        {
            base.CopyFrom(node);

            var n = (FlaggedNode)node.Value;

            Flags1 = n.Flags1;
            Flags2 = n.Flags2;
            Flags3 = n.Flags3;
            LightIndex = n.LightIndex;
            Flags5 = n.Flags5;
            Children_Count = n.Children?.Count ?? 0;
            P_Children = GetPropertyPointer(node, nameof(n.Children));
        }

        public override bool Equals(DbBlockItemStructure<TSource> other)
        {
            var _other = (DbNode<TSource>)other;

            if (!base.Equals(_other))
                return false;

            if (Flags1 != _other.Flags1) return false;
            if (Flags2 != _other.Flags2) return false;
            if (Flags3 != _other.Flags3) return false;
            if (LightIndex != _other.LightIndex) return false;
            if (Flags5 != _other.Flags5) return false;
            if (Children_Count != _other.Children_Count) return false;
            if (P_Children != _other.P_Children) return false;

            return true;
        }

        public override int GetHashCode() =>
            CombineHashCodes(base.GetHashCode(), 
                Flags1, Flags2, Flags3, LightIndex, Flags5, Children_Count, P_Children);
    }
}
