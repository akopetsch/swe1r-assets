// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using System;
using System.IO;

namespace SWE1R.Assets.Blocks.Resources
{
    public static class ResourceHelper
    {
        public static Stream ReadEmbeddedResource(string name)
        {
            Type type = typeof(ResourceHelper);
            string fullName = $"{type.Namespace}.{name}";
            return type.Assembly.GetManifestResourceStream(fullName);
        }
    }
}
