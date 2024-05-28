// Copyright 2024 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using System.IO;

namespace SWE1R.Assets.Blocks.Utils
{
    public abstract class ResourceHelperBase
    {
        public Stream ReadEmbeddedResource(string name)
        {
            string fullName = $"{GetType().Namespace}.{name}";
            return GetType().Assembly.GetManifestResourceStream(fullName);
        }
    }
}
