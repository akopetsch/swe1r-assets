// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

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
