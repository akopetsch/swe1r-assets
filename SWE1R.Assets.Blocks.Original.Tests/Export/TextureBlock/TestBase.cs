// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using SWE1R.Assets.Blocks.TextureBlock;
using Xunit.Abstractions;

namespace SWE1R.Assets.Blocks.Original.Tests.Export.TextureBlock
{
    public class TestBase
    {
        #region Fields

        private readonly ITestOutputHelper _output;
        private readonly string _blockIdName;

        #endregion

        #region Constructor

        protected TestBase(ITestOutputHelper output, string blockIdName)
        {
            _output = output;
            _blockIdName = blockIdName;
        }

        #endregion

        #region Methods

        protected void CompareItem(int index)
        {
            var textureBlock = new OriginalBlockProvider().LoadBlock<TextureBlockItem>(_blockIdName);



            Assert.True(true);
        }

        #endregion
    }
}
