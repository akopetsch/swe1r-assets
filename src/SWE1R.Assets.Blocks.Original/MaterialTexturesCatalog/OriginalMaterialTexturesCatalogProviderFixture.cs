// SPDX-License-Identifier: MIT

namespace SWE1R.Assets.Blocks.Original.MaterialTexturesCatalog
{
    public class OriginalMaterialTexturesCatalogProviderFixture : IDisposable
    {
        #region Properties

        public OriginalMaterialTexturesCatalogProvider Provider { get; } = new();

        #endregion

        #region Constructor

        public OriginalMaterialTexturesCatalogProviderFixture() =>
            Provider.Load();

        #endregion

        #region Methods (: IDisposable)

        public void Dispose() { }

        #endregion
    }
}
