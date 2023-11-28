// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using SWE1R.Assets.Blocks.ModelBlock;

namespace SWE1R.Assets.Blocks.Original.Tests.ModelBlockFixtures
{
    public abstract class ModelBlockFixtureBase : IDisposable
    {
        public Block<ModelBlockItem> ModelBlock { get; }

        public ModelBlockFixtureBase(string blockIdName)
        {
            ModelBlock = new OriginalBlockProvider().LoadBlock<ModelBlockItem>(blockIdName);
            foreach (var modelBlockItem in ModelBlock)
                modelBlockItem.Load();
        }

        public void Dispose() { }
    }
}
