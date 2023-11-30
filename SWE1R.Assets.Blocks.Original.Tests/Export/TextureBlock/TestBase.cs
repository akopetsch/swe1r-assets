// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using SWE1R.Assets.Blocks.Original.Tests.Export.TextureBlock.ModelBlockTexturesFixtures;
using SWE1R.Assets.Blocks.TextureBlock;
using Xunit.Abstractions;

namespace SWE1R.Assets.Blocks.Original.Tests.Export.TextureBlock
{
    public class TestBase<TModelBlockFixture> where TModelBlockFixture : ModelBlockTexturesFixtureBase
    {
        #region Fields

        private readonly ITestOutputHelper _output;
        private readonly string _blockIdName;

        #endregion

        #region Properties

        protected TModelBlockFixture ModelBlockFixture { get; }

        #endregion

        #region Constructor

        protected TestBase(TModelBlockFixture modelBlockFixture, ITestOutputHelper output, string blockIdName)
        {
            ModelBlockFixture = modelBlockFixture;
            _output = output;
            _blockIdName = blockIdName;
        }

        #endregion

        #region Methods

        protected void CompareItem(int index)
        {
            Block<TextureBlockItem> textureBlock = new OriginalBlockProvider().LoadBlock<TextureBlockItem>(_blockIdName);
            TextureBlockItem textureBlockItem = textureBlock[index];

            var materials = ModelBlockFixture.Catalog.GetMaterials(index).Select(x => x.Material).ToList();
            var materialTextures = ModelBlockFixture.Catalog.GetMaterialTextures(index).Select(x => x.MaterialTexture).ToList();

            if (materialTextures.Count > 0) // false 14 times
            {
                var format = materialTextures.Select(x => x.Format).Distinct().Single();
                if (index != 294 && index != 382) // TODO: hardcoded index
                {
                    short width = materialTextures.Select(x => x.Width).Distinct().Single();
                    short height = materialTextures.Select(x => x.Height).Distinct().Single();
                }
            }
        }

        #endregion
    }
}
