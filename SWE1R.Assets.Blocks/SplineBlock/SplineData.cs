// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization.Attributes;
using ByteSerialization.Components.Values;
using ByteSerialization.Components.Values.Composites.Records;
using System.Collections.Generic;

namespace SWE1R.Assets.Blocks.SplineBlock
{
    public class SplineData
    {
        [Order(0)] public SplineSegmentHeader Header { get; set; }
        
        [Length(typeof(LengthHelper))]
        [Order(1)] public List<SplineSegment> Data { get; set; }

        private class LengthHelper : IBindingHelper
        {
            public int GetValue(PropertyComponent property) =>
                property.GetAncestorValue<SplineData>().Header.ElementsCount;
        }

        public void UpdateElementsCount() => // TODO: implement in BindingComponent
            Header.ElementsCount = Data.Count;
    }
}
