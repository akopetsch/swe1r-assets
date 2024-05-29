// SPDX-License-Identifier: MIT

using ByteSerialization.Attributes;
using ByteSerialization.Components.Values;
using ByteSerialization.Components.Values.Composites.Records;
using System.Collections.Generic;

namespace SWE1R.Assets.Blocks.SplineBlock
{
    public class Spline : BlockItemValue
    {
        [Order(0)] public SplineSegmentHeader Header { get; set; }
        
        [Length(typeof(LengthHelper))]
        [Order(1)] public List<SplineSegment> Segments { get; set; }

        private class LengthHelper : IBindingHelper
        {
            public int GetValue(PropertyComponent property) =>
                property.GetAncestorValue<Spline>().Header.ElementsCount;
        }

        public void UpdateSegmentsCount() => // TODO: implement in BindingComponent
            Header.ElementsCount = Segments.Count;
    }
}
