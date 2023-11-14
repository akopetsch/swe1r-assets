// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

// TODO: fix namespace
namespace ByteSerialization.Attributes
{
    public enum ReferenceHandling
    {
        HighPriority = 1,
        DefaultPriority = 0,
        LowPriority = -1,
        Postpone = -2,
        ForceReuse = -3,
    }
}
