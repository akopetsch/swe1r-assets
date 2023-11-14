// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization.Nodes;
using SWE1R.Assets.Blocks.ModelBlock;
using System.ComponentModel.DataAnnotations.Schema;

namespace SWE1R.Assets.Blocks.Original.SQLite.Entities.ModelBlock
{
    [Table("Model_Data_Int")]
    public class DbDataInt : DbModelStructure<LightStreakOrInteger>
    {
        public int Value { get; set; }

        public override void CopyFrom(Node node)
        {
            base.CopyFrom(node);

            var lightStreakOrInteger = (LightStreakOrInteger)node.Value;

            Value = lightStreakOrInteger.Integer.Value;
        }

        public override bool Equals(DbModelStructure<LightStreakOrInteger> other)
        {
            var _other = (DbDataInt)other;

            if (!base.Equals(_other))
                return false;

            if (Value != _other.Value)
                return false;

            return true;
        }

        public override bool Equals(object obj)
        {
            if (obj is DbDataInt)
                return this.Equals((DbDataInt)obj);
            else
                return base.Equals(obj);
        }

        public override int GetHashCode() =>
            HashCode.Combine(base.GetHashCode(), Value);
    }
}
