// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization.Nodes;
using SWE1R.Assets.Blocks.ModelBlock;
using System.ComponentModel.DataAnnotations.Schema;

namespace SWE1R.Assets.Blocks.Original.SQLite.Entities.ModelBlock
{
    [Table("Model_HeaderNode")]
    public class DbModelHeaderNode : DbModelStructure<FlaggedNodeOrInteger>
    {
        public int Value { get; set; }

        public override void CopyFrom(Node node)
        {
            base.CopyFrom(node);

            var flaggedNodeOrInteger = (FlaggedNodeOrInteger)node.Value;
            Value = flaggedNodeOrInteger.Integer ??
                GetValuePosition(node.Graph, flaggedNodeOrInteger.FlaggedNode);
        }

        public override bool Equals(DbModelStructure<FlaggedNodeOrInteger> other)
        {
            var _other = (DbModelHeaderNode)other;

            if (!base.Equals(_other))
                return false;

            if (Value != _other.Value)
                return false;

            return true;
        }

        public override bool Equals(object obj)
        {
            if (obj is DbModelHeaderNode)
                return this.Equals((DbModelHeaderNode)obj);
            else
                return base.Equals(obj);
        }

        public override int GetHashCode() =>
            HashCode.Combine(base.GetHashCode(), Value);
    }
}
