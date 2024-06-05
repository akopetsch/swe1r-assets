// SPDX-License-Identifier: MIT

using ByteSerialization.Nodes;
using SWE1R.Assets.Blocks.ModelBlock;
using SWE1R.Assets.Blocks.ModelBlock.Nodes;
using System.ComponentModel.DataAnnotations.Schema;

namespace SWE1R.Assets.Blocks.Original.SQLite.Entities.ModelBlock.Nodes
{
    [Table($"{nameof(Model)}_{nameof(FlaggedNode)}_{nameof(TransformedComputedNode)}")]
    public class DbTransformedComputedNode : DbNode<TransformedComputedNode>
    {
        public short FollowModelPosition { get; set; }
        public short OrientationOption { get; set; }
        public float UpVector_X { get; set; }
        public float UpVector_Y { get; set; }
        public float UpVector_Z { get; set; }

        public override void CopyFrom(Node node)
        {
            base.CopyFrom(node);

            var n = (TransformedComputedNode)node.Value;

            FollowModelPosition = n.FollowModelPosition;
            OrientationOption = n.OrientationOption;
            UpVector_X = n.UpVector.X;
            UpVector_Y = n.UpVector.Y;
            UpVector_Z = n.UpVector.Z;
        }

        public override bool Equals(DbBlockItemStructure<TransformedComputedNode> other)
        {
            var _other = (DbTransformedComputedNode)other;

            if (!base.Equals(_other))
                return false;

            if (FollowModelPosition != _other.FollowModelPosition) return false;
            if (OrientationOption != _other.OrientationOption) return false;
            if (UpVector_X != _other.UpVector_X) return false;
            if (UpVector_Y != _other.UpVector_Y) return false;
            if (UpVector_Z != _other.UpVector_Z) return false;

            return true;
        }

        public override bool Equals(object obj)
        {
            if (obj is DbTransformedComputedNode)
                return this.Equals((DbTransformedComputedNode)obj);
            else
                return base.Equals(obj);
        }

        public override int GetHashCode() =>
            HashCode.Combine(base.GetHashCode(), FollowModelPosition, FollowModelPosition,
                HashCode.Combine(UpVector_X, UpVector_Y, UpVector_Z));
    }
}
