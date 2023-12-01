// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

namespace SWE1R.Assets.Blocks.Original.Tests.Export.TextureBlock.ModelBlockTexturesFixtures
{
    public abstract class ModelBlockTexturesFixtureBase : IDisposable
    {
        #region Properties

        public ModelBlockMaterialsAndMaterialTextures Catalog { get; }

        #endregion

        #region Constructor

        public ModelBlockTexturesFixtureBase(string blockIdName)
        {
            Catalog = ModelBlockMaterialsAndMaterialTextures.LoadJson(blockIdName) ??
                ModelBlockMaterialsAndMaterialTextures.Load(blockIdName);
        }

        #endregion

        #region Methods (: IDisposable)

        public void Dispose() { }

        #endregion
    }
}
