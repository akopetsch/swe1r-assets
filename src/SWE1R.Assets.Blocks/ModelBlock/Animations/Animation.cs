// SPDX-License-Identifier: GPL-2.0-only

using ByteSerialization.Attributes;
using ByteSerialization.Attributes.Helpers;
using ByteSerialization.Components.Values.Composites.Records;
using System.Collections.Generic;

namespace SWE1R.Assets.Blocks.ModelBlock.Animations
{
    /// <summary>
    /// <see href="https://github.com/akopetsch/Sw_Racer/blob/76c8ad9cea549ea18457846a135a7f25d48b3813/include/Swr_Model.h#L553">SWR_Anim_Header</see>
    /// </summary>
    [Alignment(typeof(AlignmentHelper))]
    public class Animation
    {
        #region Properties (serialized)

        [Offset(0xf4)]
        [Order(0)] public float Float_0f4 { get; set; }
        [Order(1)] public float Float_0f8 { get; set; }
        [Order(2)] public float Float_0fc { get; set; }
        /// <summary>
        /// Always one of the values in <see cref="OriginalBitmaskValues">OriginalBitmaskValues</see>
        /// </summary>
        [Order(3)] public int Bitmask { get; set; }
        /// <summary>
        /// Always a value from 1 to 634.
        /// </summary>
        [Order(4)] public int FramesCount { get; set; }
        [Order(5)] public float Float_108 { get; set; }
        [Order(6)] public float Float_10c { get; set; }
        [Order(7)] public float Float_110 { get; set; }
        /// <summary>
        /// Always 0.
        /// </summary>
        [Order(8)] public int Int114 { get; set; }
        /// <summary>
        /// Always 0.
        /// </summary>
        [Order(9)] public int Int118 { get; set; }
        [Length(nameof(FramesCount))]
        [Reference(ReferenceHandling.HighPriority)]
        [Order(10)] public List<float> KeyframeTimestamps { get; set; }
        [Order(11)] public KeyframesOrInteger KeyframesOrInteger { get; set; }
        [Order(12)] public TargetOrInteger TargetOrInteger { get; set; }
        /// <summary>
        /// Always one of the values in <see cref="OriginalInt_128Values">OriginalInt_128Values</see>
        /// </summary>
        [Order(13)] public int Int_128 { get; set; }

        #endregion

        #region Properties (original values)

        /// <summary>
        /// The original values of <see cref="Bitmask">Bitmask</see>.
        /// </summary>
        public static readonly int[] OriginalBitmaskValues = new int[] {
            0x11000002, // 1 time
            0x11000008, // 1 time
            0x11000012, // 17 times
            0x11000018, // 64 times
            0x11000019, // 1 time
            0x1100001a, // 2 times
            0x1100001b, // 68 times
            0x1100001c, // 74 times
            0x11000028, // 1 time
            0x11000029, // 39 times
            0x11000035, // 38 times
            0x11000038, // 2135 times
            0x11000039, // 1100 times
            0x1100003a, // 16 times
            0x11001038, // 8 times
            0x11001039, // 22 times
        };

        /// <summary>
        /// The original values of <see cref="Int_128">Int_128</see>.
        /// </summary>
        public static readonly int[] OriginalInt_128Values = new int[] {
            0b00000001, // = 0x01 =  1 (3549 times)
            0b00000100, // = 0x04 =  4 (25 times)
            0b00000101, // = 0x05 =  5 (4 times)
            0b00000110, // = 0x06 =  6 (3 times)
            0b00100010, // = 0x22 = 34 (3 times)
            0b01100100, // = 0x34 = 52 (2 times)
            0b00111010, // = 0x3a = 58 (1 time)
        };

        #endregion

        #region Properties (helper)

        public int BitmaskNibble => Bitmask & 0b1111;
        public const int SpecialBitmaskNibble =  0b0101; // = 0x05
        public const int MaterialBitmaskNibble = 0b0010; // = 0x02

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
            FramesCount = KeyframeTimestamps.Count;
            // TODO: throw exception if (IList)(KeyframesOrInteger.Keyframes.Value).Count is invalid

        #endregion
    }
}
