// Copyright 2024 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization.Nodes;
using SWE1R.Assets.Blocks.ModelBlock.Meshes;
using System.ComponentModel.DataAnnotations.Schema;

namespace SWE1R.Assets.Blocks.Original.SQLite.Entities.ModelBlock.Meshes
{
    [Table("Model_MappingSub")]
    public class DbMappingSub : DbBlockItemStructure<MappingSub>
    {
        public int Int_0 { get; set; }
        public int Int_1 { get; set; }
        public int P_Child { get; set; }

        public override void CopyFrom(Node node)
        {
            base.CopyFrom(node);

            var m = (MappingSub)node.Value;

            Int_0 = m.Int_0;
            Int_1 = m.Int_1;
            P_Child = GetPropertyPointer(node, nameof(m.Child));
        }

        public override bool Equals(DbBlockItemStructure<MappingSub> other)
        {
            var _other = (DbMappingSub)other;

            if (!base.Equals(_other))
                return false;

            if (Int_0 != _other.Int_0) return false;
            if (Int_1 != _other.Int_1) return false;
            if (P_Child != _other.P_Child) return false;

            return true;
        }

        public override bool Equals(object obj)
        {
            if (obj is DbMappingSub)
                return this.Equals((DbMappingSub)obj);
            else
                return base.Equals(obj);
        }

        public override int GetHashCode() =>
            HashCode.Combine(base.GetHashCode(), Int_0, Int_1, P_Child);
    }
}
