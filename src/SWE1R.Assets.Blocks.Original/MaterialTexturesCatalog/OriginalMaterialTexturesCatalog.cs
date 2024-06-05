// SPDX-License-Identifier: MIT

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

        public List<MeshMaterialByValueIds> MeshMaterialsByValueIds { get; set; } =
            new List<MeshMaterialByValueIds>();
        public List<MaterialTextureByValueIds> MaterialTexturesByValueIds { get; set; } =
            new List<MaterialTextureByValueIds>();

        #endregion
    }
}
