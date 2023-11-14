// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization.Components.Values.Composites.Collections;

namespace ByteSerialization.Attributes.Limiting
{
    public interface ILimitingComponent
    {
        bool IsDeserializedUntilEnd(CollectionComponent collection);
    }
}
