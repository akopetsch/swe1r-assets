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
        #region Properties

        public short FollowModelPosition { get; set; }
        public short OrientationOption { get; set; }
        public float UpVector_X { get; set; }
        public float UpVector_Y { get; set; }
        public float UpVector_Z { get; set; }

        #endregion

        public override void CopyFrom(Node node)
        {
            base.CopyFrom(node);

            var x = (TransformedComputedNode)node.Value;

            FollowModelPosition = x.FollowModelPosition;
            OrientationOption = x.OrientationOption;
            UpVector_X = x.UpVector.X;
            UpVector_Y = x.UpVector.Y;
            UpVector_Z = x.UpVector.Z;
        }

        public override bool Equals(DbBlockItemStructure<TransformedComputedNode> other)
        {
            var x = (DbTransformedComputedNode)other;

            if (!base.Equals(x))
                return false;

            if (FollowModelPosition != x.FollowModelPosition) return false;
            if (OrientationOption != x.OrientationOption) return false;
            if (UpVector_X != x.UpVector_X) return false;
            if (UpVector_Y != x.UpVector_Y) return false;
            if (UpVector_Z != x.UpVector_Z) return false;

            return true;
        }

        public override bool Equals(object obj)
        {
            if (obj is DbTransformedComputedNode x)
                return Equals(x);
            else
                return base.Equals(obj);
        }

        public override int GetHashCode() =>
            CombineHashCodes(base.GetHashCode(), 
                FollowModelPosition, FollowModelPosition,
                UpVector_X, UpVector_Y, UpVector_Z);
    }
}
