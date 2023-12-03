// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using Newtonsoft.Json;
using SWE1R.Assets.Blocks.Original.Resources;

namespace SWE1R.Assets.Blocks.Original.MaterialTexturesCatalog
{
    public class OriginalMaterialTexturesCatalogProvider
    {
        public OriginalMaterialTexturesCatalog Catalog { get; private set; }

        public void Load()
        {
            using Stream resourceStream = new OriginalBlocksResourceHelper()
                .ReadEmbeddedResource(OriginalMaterialTexturesCatalog.JsonFilename);
            using var resourceStreamReader = new StreamReader(resourceStream);
            string json = resourceStreamReader.ReadToEnd();
            Catalog = JsonConvert.DeserializeObject<OriginalMaterialTexturesCatalog>(json);
        }
    }
}
