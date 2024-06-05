// SPDX-License-Identifier: MIT

using ByteSerialization.Nodes;
using SWE1R.Assets.Blocks.ModelBlock;
using System.ComponentModel.DataAnnotations.Schema;

namespace SWE1R.Assets.Blocks.Original.SQLite.Entities.ModelBlock
{
    [Table($"{nameof(Model)}_{nameof(FlaggedNodeOrInteger)}")]
    public class DbFlaggedNodeOrInteger : DbBlockItemStructure<FlaggedNodeOrInteger>
    {
        public int I_Value { get; set; }

        public override void CopyFrom(Node node)
        {
            base.CopyFrom(node);

            var x = (FlaggedNodeOrInteger)node.Value;

            I_Value = x.Integer ??
                GetValuePosition(node.Graph, x.FlaggedNode);
        }

        public override bool Equals(DbBlockItemStructure<FlaggedNodeOrInteger> other)
        {
            var _other = (DbFlaggedNodeOrInteger)other;

            if (!base.Equals(_other))
                return false;

            if (I_Value != _other.I_Value)
                return false;

            return true;
        }

        public override bool Equals(object obj)
        {
            if (obj is DbFlaggedNodeOrInteger x)
                return Equals(x);
            else
                return base.Equals(obj);
        }

        public override int GetHashCode() =>
            CombineHashCodes(base.GetHashCode(), 
                I_Value);
    }
}
