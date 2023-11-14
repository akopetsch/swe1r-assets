// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization.Attributes.Limiting;
using ByteSerialization.Components.Values.Composites.Collections;

namespace ByteSerialization.Attributes.Padding
{
    public class PaddingComponent : AttributeComponent<PaddingAttribute>, ILimitingComponent
    {
        #region Methods

        public bool IsDeserializedUntilEnd(CollectionComponent collection)
        {
            long position = Context.Position;
            int alignment = Attribute.Alignment;
            long n = (alignment - position % alignment) % alignment;
            if (n == 0)
                return true;
            else
                return false;
        }

        #endregion
    }
}
