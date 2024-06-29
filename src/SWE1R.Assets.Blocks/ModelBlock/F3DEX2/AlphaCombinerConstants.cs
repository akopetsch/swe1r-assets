// SPDX-License-Identifier: MIT

namespace SWE1R.Assets.Blocks.ModelBlock.F3DEX2
{
    /// <summary>
    /// See also:
    /// <list type="bullet">
    ///   <item>
    ///     <see href="https://ultra64.ca/files/documentation/online-manuals/man/header/gbi.htm#:~:text=Alpha%20combiner%20constants">
    ///       ultra64.ca - 'Online Manuals (OS 2.0J)' - gbi.h - 'Alpha combiner constants'</see></item>
    ///   <item>
    ///     <see href="https://ultra64.ca/files/documentation/online-manuals/man/n64man/gdp/gDPSetCombineMode.html#:~:text=Alpha%20Combiner">
    ///       ultra64.ca - 'Online Manuals (OS 2.0J)' - 'N64® Functions Menu' - 'gDPSetCombineMode'</see></item>
    ///   <item>
    ///     <see href="https://ultra64.ca/files/documentation/online-manuals/man/pro-man/pro12/index12.6.html">
    ///       ultra64.ca - 'Online Manuals (OS 2.0J)' - 'Programming Manual' - '12. RDP Programming' - '12.6 CC: Color Combiner'</see></item>
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
