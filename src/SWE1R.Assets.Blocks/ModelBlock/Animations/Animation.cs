// SPDX-License-Identifier: MIT

using ByteSerialization.Attributes;
using ByteSerialization.Attributes.Helpers;
using ByteSerialization.Components.Values.Composites.Records;
using System.Collections.Generic;

namespace SWE1R.Assets.Blocks.ModelBlock.Animations
{
    /// <summary>
    /// <para>
    /// See also:
    /// <list type="bullet">
    ///   <item>
    ///     <see href="https://github.com/tim-tim707/SW_RACER_RE/blob/d2d15c27d81e51e91996563795643c91439147aa/src/types.h#L1546">
    ///       github.com - tim-tim707/SW_RACER_RE - types.h - swrModel_Animation</see></item>
    ///   <item>
    ///     <see href="https://github.com/Olganix/Sw_Racer/blob/76c8ad9cea549ea18457846a135a7f25d48b3813/include/Swr_Model.h#L553">
    ///       github.com - Olganix/Sw_Racer - Swr_Model.h - SWR_Anim_Header</see></item>
    /// </list>
    /// </para>
    /// </summary>
    [Alignment(typeof(AlignmentHelper))]
    public class Animation
    {
        #region Fields (const)

        private const byte _animationTypeMask = 0xF;
        private const int _flags1Mask = ~_animationTypeMask;

        #endregion

        #region Properties (serialized)

        [Order(0), Offset(0xf4)]
        public float AnimationEndTime { get; set; }
        [Order(1)]
        public float AnimationDuration { get; set; }
        [Order(2)]
        public float Duration3 { get; set; }
        [Order(3)]
        private uint Flags { get; set; } // union
        /// <summary>
        /// Always a value from 1 to 634.
        /// </summary>
        [Order(4)]
        public int KeyframesCount { get; set; }
        [Order(5)]
        public float Duration4 { get; set; }
        [Order(6)]
        public float Duration5 { get; set; }
        [Order(7)]
        public float AnimationSpeed { get; set; }
        /// <summary>
        /// Always 0.
        /// </summary>
        [Order(8)]
        public int AnimationTime { get; set; }
        /// <summary>
        /// Always 0.
        /// </summary>
        [Order(9)]
        public int KeyframeIndex { get; set; }
        [Order(10), Reference(ReferenceHandling.HighPriority), Length(nameof(KeyframesCount))]
        public List<float> KeyframesTimes { get; set; }
        [Order(11)]
        public KeyframesOrInteger KeyframesOrInteger { get; set; } // union
        [Order(12)]
        public TargetOrInteger TargetOrInteger { get; set; } // union
        /// <summary>
        /// Always one of the values in <see cref="OriginalUnk11Values">OriginalInt_128Values</see>
        /// </summary>
        [Order(13)]
        public int Unk11 { get; set; }

        #endregion

        #region Properties (C union style access)

        // TODO: implement BitFieldAttribute in BinarySerialization

        public AnimationType AnimationType
        {
            get => (AnimationType)(Flags & _animationTypeMask);
            set => Flags = (uint)value & (uint)Flags1;
        }

        public AnimationFlags Flags1
        {
            get => (AnimationFlags)(Flags & _flags1Mask);
            set => Flags = (uint)AnimationType & (uint)value;
        }

        #endregion

        #region Properties (original values)

        /// <summary>
        /// The original values of <see cref="Unk11">Int_128</see>.
        /// </summary>
        public static readonly int[] OriginalUnk11Values = new int[] {
            0b00000001, // = 0x01 =  1 (3549 times)
            0b00000100, // = 0x04 =  4 (25 times)
            0b00000101, // = 0x05 =  5 (4 times)
            0b00000110, // = 0x06 =  6 (3 times)
            0b00100010, // = 0x22 = 34 (3 times)
            0b01100100, // = 0x34 = 52 (2 times)
            0b00111010, // = 0x3a = 58 (1 time)
        };

        #endregion

        #region Classes (serialization)

        private class AlignmentHelper : IAlignmentHelper
        {
            public int GetAlignment(RecordComponent r) =>
                (r.Root.Value as Model).HasExtraAlignment((Animation)r.Value, r.Context.Graph) ? 8 : 4;
        }

        #endregion

        #region Methods

        public void UpdateFramesCount() => // TODO: implement in BindingComponent
            KeyframesCount = KeyframesTimes.Count;
            // TODO: throw exception if KeyframesOrInteger.Keyframes.Floats.Count is invalid

        #endregion
    }
}
