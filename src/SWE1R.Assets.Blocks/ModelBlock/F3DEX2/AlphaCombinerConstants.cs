// SPDX-License-Identifier: MIT

namespace SWE1R.Assets.Blocks.ModelBlock.F3DEX2
{
    /// <summary>
    /// See also:
    /// <list type="bullet">
    ///   <item>
    ///     <see href="http://n64devkit.square7.ch/header/gbi.htm#:~:text=Alpha%20combiner%20constants">
    ///       n64devkit.square7.ch - gbi.h - 'Alpha combiner constants'</see></item>
    ///   <item>
    ///     <see href="http://n64devkit.square7.ch/n64man/gdp/gDPSetCombineLERP.htm">
    ///       n64devkit.square7.ch - 'gDPSetCombineLERP'</see></item>
    /// </list>
    /// </summary>
    public enum AlphaCombinerConstants
    {
        /// <summary>
        /// Combined alpha from 1 Cycle mode
        /// </summary>
        G_ACMUX_COMBINED = 0,

        /// <summary>
        /// Texture alpha
        /// </summary>
        G_ACMUX_TEXEL0 = 1,

        /// <summary>
        /// Texture alpha from tile+1
        /// </summary>
        G_ACMUX_TEXEL1 = 2,

        /// <summary>
        /// Primitive alpha
        /// </summary>
        G_ACMUX_PRIMITIVE = 3,

        /// <summary>
        /// Shading alpha
        /// </summary>
        G_ACMUX_SHADE = 4,

        /// <summary>
        /// Environment alpha
        /// </summary>
        G_ACMUX_ENVIRONMENT = 5,

        /// <summary>
        /// LOD coefficient
        /// </summary>
        G_ACMUX_LOD_FRACTION = 0,

        /// <summary>
        /// PRIM_LOD_FRAC
        /// </summary>
        G_ACMUX_PRIM_LOD_FRAC = 6,

        /// <summary>
        /// 1.0
        /// </summary>
        G_ACMUX_1 = 6,

        /// <summary>
        /// 0.0
        /// </summary>
        G_ACMUX_0 = 7
    }
}
