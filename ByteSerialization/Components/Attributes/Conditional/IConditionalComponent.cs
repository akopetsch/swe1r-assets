// Copyright 2024 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization.Nodes;

namespace ByteSerialization.Components.Attributes.Conditional
{
    public interface IConditionalComponent
    {
        bool IsSerialized(Node node);
    }
}
