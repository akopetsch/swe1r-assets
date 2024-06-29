// SPDX-License-Identifier: MIT

namespace SWE1R.Assets.Blocks.ModelBlock.F3DEX2
{
    /// <summary>
    /// See also:
    /// <list type="bullet">
    ///   <item>
    ///     <see href="https://ultra64.ca/files/documentation/online-manuals/man/header/gbi.htm#:~:text=Color%20combiner%20constants">
    ///       ultra64.ca - 'Online Manuals (OS 2.0J)' - gbi.h - 'Color combiner constants'</see></item>
    ///   <item>
    ///     <see href="https://ultra64.ca/files/documentation/online-manuals/man/n64man/gdp/gDPSetCombineMode.html">
    ///       ultra64.ca - 'Online Manuals (OS 2.0J)' - 'N64® Functions Menu' - 'gDPSetCombineMode'</see></item>
    ///   <item>
    ///     <see href="https://ultra64.ca/files/documentation/online-manuals/man/pro-man/pro12/index12.6.html">
    ///       ultra64.ca - 'Online Manuals (OS 2.0J)' - 'Programming Manual' - '12. RDP Programming' - '12.6 CC: Color Combiner'</see></item>
    /// </list>
    /// </summary>
    public enum ColorCombinerConstant
    {
        /// <summary>
        /// Color combined from 1st cycle
        /// </summary>
        G_CCMUX_COMBINED = 0,

        /// <summary>
        /// Texture color
        /// </summary>
        G_CCMUX_TEXEL0 = 1,

        /// <summary>
        /// Texture color from tile+1
        /// </summary>
        G_CCMUX_TEXEL1 = 2,

        /// <summary>
        /// Primitive color
        /// </summary>
        G_CCMUX_PRIMITIVE = 3,

        /// <summary>
        /// Shading color
        /// </summary>
        G_CCMUX_SHADE = 4,

        /// <summary>
        /// Environment color
        /// </summary>
        G_CCMUX_ENVIRONMENT = 5,

        /// <summary>
        /// Chroma key center value
        /// </summary>
        G_CCMUX_CENTER = 6,

        /// <summary>
        /// Chroma key scale value
        /// </summary>
        G_CCMUX_SCALE = 6,

        /// <summary>
        /// Combined alpha from 1st cycle
        /// </summary>
        G_CCMUX_COMBINED_ALPHA = 7,

        /// <summary>
        /// Texture alpha
        /// </summary>
        G_CCMUX_TEXEL0_ALPHA = 8,

        /// <summary>
        /// Texture alpha from tile+1
        /// </summary>
        G_CCMUX_TEXEL1_ALPHA = 9,

        /// <summary>
        /// Primitive alpha
        /// </summary>
        G_CCMUX_PRIMITIVE_ALPHA = 10,

        /// <summary>
        /// Shading alpha
        /// </summary>
        G_CCMUX_SHADE_ALPHA = 11,

        /// <summary>
        /// Environment alpha
        /// </summary>
        G_CCMUX_ENV_ALPHA = 12,

        /// <summary>
        /// LOD coefficient
        /// </summary>
        G_CCMUX_LOD_FRACTION = 13,

        /// <summary>
        /// Primitive LOD coefficient
        /// </summary>
        G_CCMUX_PRIM_LOD_FRAC = 14,

        /// <summary>
        /// Random noise
        /// </summary>
        G_CCMUX_NOISE = 7,

        /// <summary>
        /// Color conversion constant K4
        /// </summary>
        G_CCMUX_K4 = 7,

        /// <summary>
        /// Color conversion constant K5
        /// </summary>
        G_CCMUX_K5 = 15,

        /// <summary>
        /// 1.0
        /// </summary>
        G_CCMUX_1 = 6,

        /// <summary>
        /// 0.0
        /// </summary>
        G_CCMUX_0 = 31,
    }
}
