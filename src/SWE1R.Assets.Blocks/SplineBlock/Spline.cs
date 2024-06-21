// SPDX-License-Identifier: MIT

using ByteSerialization.Attributes;
using ByteSerialization.Components.Values;
using ByteSerialization.Components.Values.Composites.Records;
using System.Collections.Generic;

namespace SWE1R.Assets.Blocks.SplineBlock
{
    public class Spline : BlockItemValue
    {
        #region Classes (serialization)

        private class SegmentsLengthHelper : IBindingHelper
        {
            public int GetValue(PropertyComponent property) =>
                property.GetAncestorValue<Spline>().Header.ElementsCount;
        }

        #endregion

        #region Properties (serialized)

        [Order(0)]
        public SplineSegmentHeader Header { get; set; }
        [Order(1), Length(typeof(SegmentsLengthHelper))]
        public List<SplineSegment> Segments { get; set; }

        #endregion

        #region Methods (helper)

        public void UpdateSegmentsCount() => // TODO: implement in BindingComponent
            Header.ElementsCount = Segments.Count;

        #endregion
    }
}
