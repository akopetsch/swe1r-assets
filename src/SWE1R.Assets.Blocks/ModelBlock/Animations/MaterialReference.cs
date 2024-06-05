// SPDX-License-Identifier: MIT

using ByteSerialization.Attributes;
using SWE1R.Assets.Blocks.ModelBlock.Materials;

namespace SWE1R.Assets.Blocks.ModelBlock.Animations
{
    public class MaterialReference
    {
        #region Properties (serialized)

        /// <summary>
        /// Never null.
        /// </summary>
        [Order(0), Reference]
        public MeshMaterial MeshMaterial { get; set; }

        [Order(1), Reference]
        private object NullPointer { get; set; } = null;

        #endregion
    }
}
