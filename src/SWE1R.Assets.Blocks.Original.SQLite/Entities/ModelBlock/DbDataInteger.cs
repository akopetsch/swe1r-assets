// SPDX-License-Identifier: MIT

using ByteSerialization.Nodes;
using SWE1R.Assets.Blocks.ModelBlock;
using System.ComponentModel.DataAnnotations.Schema;

namespace SWE1R.Assets.Blocks.Original.SQLite.Entities.ModelBlock
{
    [Table($"{nameof(Model)}_{nameof(Model.Data)}_{nameof(LightStreakOrInteger.Integer)}")]
    public class DbDataInteger : DbBlockItemStructure<LightStreakOrInteger>
    {
        #region Properties

        public int Value { get; set; }

        #endregion

        public override void CopyFrom(Node node)
        {
            base.CopyFrom(node);

            var x = (LightStreakOrInteger)node.Value;

            Value = x.Integer.Value;
        }

        public override bool Equals(DbBlockItemStructure<LightStreakOrInteger> other)
        {
            var x = (DbDataInteger)other;

            if (!base.Equals(x))
                return false;

            if (Value != x.Value)
                return false;

            return true;
        }

        public override bool Equals(object obj)
        {
            if (obj is DbDataInteger x)
                return Equals(x);
            else
                return base.Equals(obj);
        }

        public override int GetHashCode() =>
            CombineHashCodes(base.GetHashCode(), 
                Value);
    }
}
