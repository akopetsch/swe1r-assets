// SPDX-License-Identifier: MIT

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
