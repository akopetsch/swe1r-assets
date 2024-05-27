// Copyright 2024 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

namespace ByteSerialization.Components.Values.Customs
{
    public interface ICustomSerializable
    {
        void Serialize(CustomComponent customComponent);
        void Deserialize(CustomComponent customComponent);
    }
}
