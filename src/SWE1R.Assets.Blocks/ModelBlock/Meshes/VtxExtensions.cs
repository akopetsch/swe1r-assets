// SPDX-License-Identifier: MIT

using SWE1R.Assets.Blocks.ModelBlock.Materials;
using SWE1R.Assets.Blocks.ModelBlock.N64Sdk;
using System.Numerics;

namespace SWE1R.Assets.Blocks.ModelBlock.Meshes
{
    public static class VtxExtensions // TODO: move to another class
    {
        #region Constants

        public const int UvDivisor = 4096;
        public const int UvDoubleDivisor = 2 * UvDivisor;

        #endregion


        #region Methods (export)

        public static Vector2 GetEffectiveUV(this Vtx vtx, MaterialTextureChild materialTextureChild)
        {
            float u, v, uMax, vMax;
            if (materialTextureChild != null)
            {
                uMax = materialTextureChild.HasDoubleWidth ? UvDoubleDivisor : UvDivisor;
                vMax = materialTextureChild.HasDoubleHeight ? UvDoubleDivisor : UvDivisor;
                u = vtx.U / uMax;
                v = vtx.V / vMax;
                if (materialTextureChild.IsFlippedHorizontally)
                    u -= 1;
                if (materialTextureChild.IsFlippedVertically)
                    v -= 1;
                // TODO: also consider Material.Properties.IsFlipped
            }
            else
            {
                uMax = UvDivisor;
                vMax = UvDivisor;
                u = vtx.U / uMax;
                v = vtx.V / vMax;
            }

            return new Vector2(u, v);
        }

        #endregion
    }
}
