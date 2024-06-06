// SPDX-License-Identifier: MIT

using ByteSerialization.Nodes;
using SWE1R.Assets.Blocks.ModelBlock;
using SWE1R.Assets.Blocks.ModelBlock.Meshes.Behaviours;
using System.ComponentModel.DataAnnotations.Schema;

namespace SWE1R.Assets.Blocks.Original.SQLite.Entities.ModelBlock.Meshes.Behaviours
{
    [Table($"{nameof(Model)}_{nameof(MappingSub)}")]
    public class DbMappingSub : DbBlockItemStructure<MappingSub>
    {
        #region Properties

        public int Int_0 { get; set; }
        public int Int_1 { get; set; }
        public int P_Child { get; set; }

        #endregion

        public override void CopyFrom(Node node)
        {
            base.CopyFrom(node);

            var x = (MappingSub)node.Value;

            Int_0 = x.Int_0;
            Int_1 = x.Int_1;
            P_Child = GetPropertyPointer(node, nameof(x.Child));
        }

        public override bool Equals(DbBlockItemStructure<MappingSub> other)
        {
            var x = (DbMappingSub)other;

            if (!base.Equals(x))
                return false;

            if (Int_0 != x.Int_0) return false;
            if (Int_1 != x.Int_1) return false;
            if (P_Child != x.P_Child) return false;

            return true;
        }

        public override bool Equals(object obj)
        {
            if (obj is DbMappingSub x)
                return Equals(x);
            else
                return base.Equals(obj);
        }

        public override int GetHashCode() =>
            CombineHashCodes(base.GetHashCode(),
                Int_0, Int_1, P_Child);
    }
}
