// SPDX-License-Identifier: MIT

using ByteSerialization.Nodes;
using SWE1R.Assets.Blocks.ModelBlock;
using SWE1R.Assets.Blocks.ModelBlock.Nodes;
using System.ComponentModel.DataAnnotations.Schema;

namespace SWE1R.Assets.Blocks.Original.SQLite.Entities.ModelBlock.Nodes
{
    [Table($"{nameof(Model)}_{nameof(FlaggedNode)}_{nameof(LodSelectorNode)}")]
    public class DbLodSelectorNode : DbNode<LodSelectorNode>
    {
        public float LodDistances0 { get; set; }
        public float LodDistances1 { get; set; }
        public float LodDistances2 { get; set; }
        public float LodDistances3 { get; set; }
        public float LodDistances4 { get; set; }
        public float LodDistances5 { get; set; }
        public float LodDistances6 { get; set; }
        public float LodDistances7 { get; set; }
        public int Unk0 { get; set; }
        public int Unk1 { get; set; }
        public int Unk2 { get; set; }

        public override void CopyFrom(Node node)
        {
            base.CopyFrom(node);

            var n = (LodSelectorNode)node.Value;

            LodDistances0 = n.LodDistances[0];
            LodDistances1 = n.LodDistances[1];
            LodDistances2 = n.LodDistances[2];
            LodDistances3 = n.LodDistances[3];
            LodDistances4 = n.LodDistances[4];
            LodDistances5 = n.LodDistances[5];
            LodDistances6 = n.LodDistances[6];
            LodDistances7 = n.LodDistances[7];
            Unk0 = n.Unk[0];
            Unk1 = n.Unk[1];
            Unk2 = n.Unk[2];
        }

        public override bool Equals(DbBlockItemStructure<LodSelectorNode> other)
        {
            var _other = (DbLodSelectorNode)other;

            if (!base.Equals(_other))
                return false;

            if (LodDistances0 != _other.LodDistances0) return false;
            if (LodDistances1 != _other.LodDistances1) return false;
            if (LodDistances2 != _other.LodDistances2) return false;
            if (LodDistances3 != _other.LodDistances3) return false;
            if (LodDistances4 != _other.LodDistances4) return false;
            if (LodDistances5 != _other.LodDistances5) return false;
            if (LodDistances6 != _other.LodDistances6) return false;
            if (LodDistances7 != _other.LodDistances7) return false;
            if (Unk0 != _other.Unk0) return false;
            if (Unk1 != _other.Unk1) return false;
            if (Unk2 != _other.Unk2) return false;

            return true;
        }

        public override bool Equals(object obj)
        {
            if (obj is DbLodSelectorNode)
                return this.Equals((DbLodSelectorNode)obj);
            else
                return base.Equals(obj);
        }

        public override int GetHashCode() =>
            HashCode.Combine(base.GetHashCode(),
                HashCode.Combine(
                    LodDistances0, LodDistances1, LodDistances2, LodDistances3, 
                    LodDistances4, LodDistances5, LodDistances6, LodDistances7),
                HashCode.Combine(Unk0, Unk1, Unk2));
    }
}
