// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

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
        #region Properties (serialization)

        [Offset(0xf4)]
        [Order(0)] public float Float_0f4 { get; set; }
        [Order(1)] public float Float_0f8 { get; set; }
        [Order(2)] public float Float_0fc { get; set; }
        [Order(3)] public int Bitmask { get; set; }
        [Order(4)] public int FramesCount { get; set; }
        [Order(5)] public float Float_108 { get; set; }
        [Order(6)] public float Float_10c { get; set; }
        [Order(7)] public float Float_110 { get; set; }
        [Order(8)] public int Int114 { get; set; }
        [Order(9)] public int Int118 { get; set; }
        
        [Length(nameof(FramesCount))]
        [Reference(ReferenceHandling.HighPriority)]
        [Order(10)] public List<float> KeyframeTimestamps { get; set; }
        
        [Order(11)] public KeyframesOrInteger KeyframesOrInteger { get; set; }
        [Order(12)] public TargetOrInteger TargetOrInteger { get; set; }
        [Order(13)] public int Int_128 { get; set; }

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
                (r.Root.Value as Header).HasExtraAlignment((Animation)r.Value, r.Context.Graph) ? 8 : 4;
        }

        #endregion

        #region Methods

        public void UpdateFramesCount() => // TODO: implement in BindingComponent
            FramesCount = KeyframeTimestamps.Count;
            // TODO: throw exception if (IList)(KeyframesOrInteger.Keyframes.Value).Count is invalid

        #endregion
    }
}
