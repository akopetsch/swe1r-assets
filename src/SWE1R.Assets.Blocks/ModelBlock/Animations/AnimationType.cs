// SPDX-License-Identifier: MIT

namespace SWE1R.Assets.Blocks.ModelBlock.Animations
{
    // 

    /// <summary>
    /// <para>
    /// See also:
    /// <list type="bullet">
    ///   <item>
    ///     <see href="https://github.com/tim-tim707/SW_RACER_RE/blob/d2d15c27d81e51e91996563795643c91439147aa/src/Swr/swrModel.c#L525">
    ///       github.com - tim-tim707/SW_RACER_RE - swrModel.c - swrModel_ByteSwapAnimation(swrModel_Animation *)</see></item>
    ///   <item>
    ///     <see href="https://github.com/tim-tim707/SW_RACER_RE/blob/d2d15c27d81e51e91996563795643c91439147aa/src/Swr/swrModel.c#L1069">
    ///       github.com - tim-tim707/SW_RACER_RE - swrModel.c - swrModel_UpdateAnimations()</see></item>
    /// </list>
    /// </para>
    /// </summary>
    public enum AnimationType : byte
    {
        _1 = 0x1,
        TextureFlipbook = 0x2,

        _4 = 0x4,
        _5 = 0x5,
        _6 = 0x6,
        _7 = 0x7,
        AxisAngle = 0x8,
        Translate = 0x9,
        Scale = 0xA,
        TextureScrollX = 0xB,
        TextureScrollY = 0xC,
    }
}
