// SPDX-License-Identifier: MIT

using ByteSerialization.Nodes;
using SWE1R.Assets.Blocks.ModelBlock;
using System.ComponentModel.DataAnnotations.Schema;

namespace SWE1R.Assets.Blocks.Original.SQLite.Entities.ModelBlock
{
    [Table($"{nameof(Model)}_{nameof(Model.Data)}_{nameof(LightStreakOrInteger.LightStreak)}")]
    public class DbDataLightStreak : DbBlockItemStructure<LightStreakOrInteger>
    {
        #region Properties

        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }

        #endregion

        public override void CopyFrom(Node node)
        {
            base.CopyFrom(node);

            var x = (LightStreakOrInteger)node.Value;

            X = x.LightStreak.Vector.X;
            Y = x.LightStreak.Vector.Y;
            Z = x.LightStreak.Vector.Z;
        }

        public override bool Equals(DbBlockItemStructure<LightStreakOrInteger> other)
        {
            var x = (DbDataLightStreak)other;

            if (!base.Equals(x))
                return false;

            if (X != x.X)
                return false;
            if (Y != x.Y)
                return false;
            if (Z != x.Z)
                return false;

            return true;
        }

        public override bool Equals(object obj)
        {
            if (obj is DbDataLightStreak x)
                return Equals(x);
            else
                return base.Equals(obj);
        }

        public override int GetHashCode() => 
            CombineHashCodes(base.GetHashCode(), 
                X, Y, Z);
    }
}
