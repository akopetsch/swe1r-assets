// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization.Attributes;

namespace ByteSerialization.Components.Attributes.Reference
{
    public class ReferenceConfiguration
    {
        public int? Alignment { get; set; } = 4;
        public ReferenceHandling Handling { get; set; } = ReferenceHandling.DefaultPriority;
        public int Order { get; set; } = 0;
    }
}
