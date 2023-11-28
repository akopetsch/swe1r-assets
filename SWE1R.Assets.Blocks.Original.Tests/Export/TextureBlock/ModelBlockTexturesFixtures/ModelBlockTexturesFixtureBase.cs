// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using SWE1R.Assets.Blocks.ModelBlock;
using SWE1R.Assets.Blocks.Original.Tests.Export.TextureBlock.ModelBlockTexturesFixtures;

namespace SWE1R.Assets.Blocks.Original.Tests.Export.TextureBlock.ModelBlockFixtures
{
    public abstract class ModelBlockTexturesFixtureBase : IDisposable
    {
        #region Properties

        public ModelBlockMaterialsAndMaterialTextures Catalog { get; }

        #endregion

        #region Constructor

        public ModelBlockTexturesFixtureBase(string blockIdName)
        {
            Block<ModelBlockItem> modelBlock = new OriginalBlockProvider().LoadBlock<ModelBlockItem>(blockIdName);

            Catalog = ModelBlockMaterialsAndMaterialTextures.LoadJson(blockIdName) ??
                ModelBlockMaterialsAndMaterialTextures.Load(blockIdName);
        }

        #endregion

        #region Methods (: IDisposable)

        public void Dispose() { }

        #endregion

        #region Methods

        

        #endregion
    }
}
