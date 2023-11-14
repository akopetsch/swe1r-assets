﻿// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

namespace ByteSerialization.Components
{
    public interface ISerializableComponent
    {
        void Serialize();
        void Deserialize();
    }
}
