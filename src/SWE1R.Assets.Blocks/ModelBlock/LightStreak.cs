// SPDX-License-Identifier: MIT

using ByteSerialization.Attributes;
using SWE1R.Assets.Blocks.Vectors;

namespace SWE1R.Assets.Blocks.ModelBlock
{
    public class LightStreak
    {
        #region Fields (constants)

        public const string identifierString = "LStr";
        
        #endregion

        #region Properties (serialized)

        [Order(0)]
        public Vector3Single Vector { get; set; }

        #endregion

        #region Properties (serialization)

        public static int StructureSize { get; } = 
            identifierString.Length + Vector3Single.StructureSize;

        #endregion

        #region Constructor

        public LightStreak() => 
            Vector = new Vector3Single();

        public LightStreak(Vector3Single vector) => 
            Vector = vector;

        #endregion
    }
}
