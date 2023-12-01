// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using SWE1R.Assets.Blocks;
using SWE1R.Assets.Blocks.Metadata;
using SWE1R.Assets.Blocks.ModelBlock;
using SWE1R.Assets.Blocks.Original;

namespace FiddleApp
{
    public class TestClassGenerator
    {
        private readonly OriginalBlocksProvider _originalBlockProvider = new();

        public void Generate()
        {
            _originalBlockProvider.Init();
            var foo = _originalBlockProvider.GetBlockItem<ModelBlockItem>(1145);
        }

        private List<BlockMetadata> GenerateFoo<TBlockItem>() where TBlockItem : BlockItem, new()
        {
            return null;
        }
    }
}
