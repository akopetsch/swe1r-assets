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
        #region Properties

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

        #endregion

        public override void CopyFrom(Node node)
        {
            base.CopyFrom(node);

            var x = (LodSelectorNode)node.Value;

            LodDistances0 = x.LodDistances[0];
            LodDistances1 = x.LodDistances[1];
            LodDistances2 = x.LodDistances[2];
            LodDistances3 = x.LodDistances[3];
            LodDistances4 = x.LodDistances[4];
            LodDistances5 = x.LodDistances[5];
            LodDistances6 = x.LodDistances[6];
            LodDistances7 = x.LodDistances[7];
            Unk0 = x.Unk[0];
            Unk1 = x.Unk[1];
            Unk2 = x.Unk[2];
        }

        public override bool Equals(DbBlockItemStructure<LodSelectorNode> other)
        {
            var x = (DbLodSelectorNode)other;

            if (!base.Equals(x))
                return false;

            if (LodDistances0 != x.LodDistances0) return false;
            if (LodDistances1 != x.LodDistances1) return false;
            if (LodDistances2 != x.LodDistances2) return false;
            if (LodDistances3 != x.LodDistances3) return false;
            if (LodDistances4 != x.LodDistances4) return false;
            if (LodDistances5 != x.LodDistances5) return false;
            if (LodDistances6 != x.LodDistances6) return false;
            if (LodDistances7 != x.LodDistances7) return false;
            if (Unk0 != x.Unk0) return false;
            if (Unk1 != x.Unk1) return false;
            if (Unk2 != x.Unk2) return false;

            return true;
        }

        public override bool Equals(object obj)
        {
            if (obj is DbLodSelectorNode x)
                return Equals(x);
            else
                return base.Equals(obj);
        }

        public override int GetHashCode() =>
            CombineHashCodes(base.GetHashCode(),
                LodDistances0, LodDistances1, LodDistances2, LodDistances3, 
                LodDistances4, LodDistances5, LodDistances6, LodDistances7,
                Unk0, Unk1, Unk2);
    }
}
