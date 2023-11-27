// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization.Attributes;
using ByteSerialization.Attributes.Reference;
using ByteSerialization.Components.Values;
using ByteSerialization.Components.Values.Composites.Collections;
using ByteSerialization.Components.Values.Composites.Records;
using System.Diagnostics;

namespace SWE1R.Assets.Blocks.SpriteBlock
{
    public class SpriteTile
    {
        #region Constants

        public const int MaxWidth = 64;
        public const int MaxHeight = 32;

        #endregion

        #region Properties (serialization)

        /// <summary>
        /// Always a value from 2 to 64.
        /// </summary>
        [Order(0)] 
        public short Width { get; set; }
        /// <summary>
        /// Always a value from 1 to 32.
        /// </summary>
        [Order(1)] 
        public short Height { get; set; }
        /// <summary>
        /// Never null. Always has a length from 16 to 4096.
        /// </summary>
        [Order(2), Reference(ReferenceHandling.Postpone), Length(typeof(LengthHelper))] 
        public byte[] PixelsBytes { get; set; } // TODO: type

        #endregion

        #region Classes (serialization)

        private class LengthHelper : IBindingHelper
        {
            public int GetValue(PropertyComponent property)
            {
                var recordComponent = (RecordComponent)property.GetAncestorValueComponent<SpriteTile>();

                int startPosition = GetPixelsBytesPointer(recordComponent);

                int endPosition;
                var elementComponent = recordComponent.Get<CollectionElementComponent>();
                if (elementComponent.IsLastElement)
                {
                    // TODO: HACK: stream could be longer than the SpriteData's data bytes
                    endPosition = (int)property.Context.Stream.Length;
                }
                else
                {
                    endPosition = GetPixelsBytesPointer(elementComponent.NextElement.Get<RecordComponent>());
                }

                int length = endPosition - startPosition;
                Debug.Assert(endPosition % 4 == 0);
                return length;
            }

            private int GetPixelsBytesPointer(RecordComponent recordComponent) =>
                recordComponent.Properties[nameof(PixelsBytes)].Get<ReferenceComponent>().Pointer.Value;
        }

        #endregion

        #region Methods (: object)

        public override string ToString() =>
            $"({nameof(Width)}={Width}, {nameof(Height)}={Height})";

        #endregion
    }
}
