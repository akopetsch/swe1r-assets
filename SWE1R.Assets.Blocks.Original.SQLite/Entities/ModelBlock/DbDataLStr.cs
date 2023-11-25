// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization.Nodes;
using SWE1R.Assets.Blocks.ModelBlock;
using System.ComponentModel.DataAnnotations.Schema;

namespace SWE1R.Assets.Blocks.Original.SQLite.Entities.ModelBlock
{
    [Table("Model_Data_LStr")]
    public class DbDataLStr : DbBlockItemStructure<LightStreakOrInteger>
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }

        public override void CopyFrom(Node node)
        {
            base.CopyFrom(node);

            var lightStreakOrInteger = (LightStreakOrInteger)node.Value;

            X = lightStreakOrInteger.LightStreak.Vector.X;
            Y = lightStreakOrInteger.LightStreak.Vector.Y;
            Z = lightStreakOrInteger.LightStreak.Vector.Z;
        }

        public override bool Equals(DbBlockItemStructure<LightStreakOrInteger> other)
        {
            var _other = (DbDataLStr)other;

            if (!base.Equals(_other))
                return false;

            if (X != _other.X)
                return false;
            if (Y != _other.Y)
                return false;
            if (Z != _other.Z)
                return false;

            return true;
        }

        public override bool Equals(object obj)
        {
            if (obj is DbDataLStr)
                return this.Equals((DbDataLStr)obj);
            else
                return base.Equals(obj);
        }

        public override int GetHashCode() => 
            HashCode.Combine(base.GetHashCode(), X, Y, Z);
    }
}
