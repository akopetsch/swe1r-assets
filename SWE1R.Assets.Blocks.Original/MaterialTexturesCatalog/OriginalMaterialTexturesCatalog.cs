﻿// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using Newtonsoft.Json;

namespace SWE1R.Assets.Blocks.Original.MaterialTexturesCatalog
{
    public class OriginalMaterialTexturesCatalog
    {
        #region Constants / Properties (serialization)

        public const string JsonFilename =
            $"{nameof(OriginalMaterialTexturesCatalog)}.json";

        public static JsonSerializerSettings JsonSerializerSettings { get; } =
            new JsonSerializerSettings {
                ContractResolver = new IgnoreGetterOnlyContractResolver(),
                PreserveReferencesHandling = PreserveReferencesHandling.Objects,
            };

        #endregion

        #region Properties (serialized)

        public List<MaterialByValueIds> MaterialsByValueIds { get; set; } =
            new List<MaterialByValueIds>();
        public List<MaterialTextureByValueIds> MaterialTexturesByValueIds { get; set; } =
            new List<MaterialTextureByValueIds>();

        #endregion
    }
}
