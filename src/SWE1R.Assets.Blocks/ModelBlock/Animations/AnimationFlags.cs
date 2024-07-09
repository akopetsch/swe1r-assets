// SPDX-License-Identifier: MIT

using System;

namespace SWE1R.Assets.Blocks.ModelBlock.Animations
{
    /// <summary>
    /// <para>
    /// See also:
    /// <list type="bullet">
    ///   <item>
    ///     <see href="https://github.com/tim-tim707/SW_RACER_RE/blob/ae5d8a07762f739a6cb0be2d42fb269aaadc9a36/src/types_enums.h#L78">
    ///       github.com - tim-tim707/SW_RACER_RE - types_enums.h - swrModel_AnimationFlags</see></item>
    /// </list>
    /// </para>
    /// </summary>
    [Flags]
    public enum AnimationFlags : uint
    {
        /// <summary>
        /// If set, the animation will loop when it reaches the end;
        /// otherwise it just stops there.
        /// </summary>
        Loop = 0x10,
        
        Unknown_20 = 0x20,

        /// <summary>
        /// If set and looping is enabled, the animation will 
        /// transition instead of just jumping when looping.
        /// </summary>
        LoopWithTransition = 0x40,
        
        Unknown_1000 = 0x1000,

        Reset = 0x1000000,
        
        /// <summary>
        /// A transition to a different animation time is planned.
        /// </summary>
        Transition = 0x20000000,
        
        /// <summary>
        /// An actual transition to a different animation time is ongoing.
        /// </summary>
        TransitioningNow = 0x40000000,

        Enabled = 0x10000000,
        
        Disabled = 0x80000000,
    }
}
