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
        public float Floats0 { get; set; }
        public float Floats1 { get; set; }
        public float Floats2 { get; set; }
        public float Floats3 { get; set; }
        public float Floats4 { get; set; }
        public float Floats5 { get; set; }
        public float Floats6 { get; set; }
        public float Floats7 { get; set; }
        public int Ints0 { get; set; }
        public int Ints1 { get; set; }
        public int Ints2 { get; set; }

        public override void CopyFrom(Node node)
        {
            base.CopyFrom(node);

            var n = (LodSelectorNode)node.Value;

            Floats0 = n.Floats[0];
            Floats1 = n.Floats[1];
            Floats2 = n.Floats[2];
            Floats3 = n.Floats[3];
            Floats4 = n.Floats[4];
            Floats5 = n.Floats[5];
            Floats6 = n.Floats[6];
            Floats7 = n.Floats[7];
            Ints0 = n.Ints[0];
            Ints1 = n.Ints[1];
            Ints2 = n.Ints[2];
        }

        public override bool Equals(DbBlockItemStructure<LodSelectorNode> other)
        {
            var _other = (DbLodSelectorNode)other;

            if (!base.Equals(_other))
                return false;

            if (Floats0 != _other.Floats0) return false;
            if (Floats1 != _other.Floats1) return false;
            if (Floats2 != _other.Floats2) return false;
            if (Floats3 != _other.Floats3) return false;
            if (Floats4 != _other.Floats4) return false;
            if (Floats5 != _other.Floats5) return false;
            if (Floats6 != _other.Floats6) return false;
            if (Floats7 != _other.Floats7) return false;
            if (Ints0 != _other.Ints0) return false;
            if (Ints1 != _other.Ints1) return false;
            if (Ints2 != _other.Ints2) return false;

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
                HashCode.Combine(Floats0, Floats1, Floats2, Floats3, Floats4, Floats5, Floats6, Floats7),
                HashCode.Combine(Ints0, Ints1, Ints2));
    }
}
