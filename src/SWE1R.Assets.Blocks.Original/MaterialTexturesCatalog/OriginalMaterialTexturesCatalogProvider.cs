// SPDX-License-Identifier: MIT

using Newtonsoft.Json;
using SWE1R.Assets.Blocks.Original.Resources;

namespace SWE1R.Assets.Blocks.Original.MaterialTexturesCatalog
{
    public class OriginalMaterialTexturesCatalogProvider
    {
        public OriginalMaterialTexturesCatalog Catalog { get; private set; }

        public void Load()
        {
            using Stream resourceStream = new ResourceHelper()
                .ReadEmbeddedResource(OriginalMaterialTexturesCatalog.JsonFilename);
            using var resourceStreamReader = new StreamReader(resourceStream);
            string json = resourceStreamReader.ReadToEnd();
            Catalog = JsonConvert.DeserializeObject<OriginalMaterialTexturesCatalog>(json);
        }
    }
}
