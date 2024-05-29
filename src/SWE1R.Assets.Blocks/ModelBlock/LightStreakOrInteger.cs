// SPDX-License-Identifier: GPL-2.0-only

using ByteSerialization.Attributes;

namespace SWE1R.Assets.Blocks.ModelBlock
{
    public class LightStreakOrInteger
    {
        #region Properties (serialized)

        [TypeIdentifier(LightStreak.identifierString, typeof(LightStreak))]
        [TypeDefault(typeof(int))]
        [Order(0)] public object Value { get; private set; }

        #endregion

        #region Properties (C union style access)

        public LightStreak LightStreak
        {
            get => Value as LightStreak;
            set => Value = value;
        }

        public int? Integer
        {
            get => Value as int?;
            set => Value = value;
        }

        #endregion

        #region Properties (helper)

        public int StructureSize => 
            LightStreak != null ? LightStreak.StructureSize : sizeof(int);

        #endregion

        #region Constructor

        public LightStreakOrInteger()
        { }

        #endregion
    }
}
