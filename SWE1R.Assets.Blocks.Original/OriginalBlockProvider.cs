// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using SWE1R.Assets.Blocks.Original.Resources;

namespace SWE1R.Assets.Blocks.Original
{
    public class OriginalBlockProvider
    {
        public Block<TItem> LoadBlock<TItem>(string blockIdName) where TItem : BlockItem, new()
        {
            string resourcePath = GetBlockResourcePath<TItem>(blockIdName);
            Stream resourceStream = new OriginalBlocksResourceHelper().ReadEmbeddedResource(resourcePath);
            return Block.Load<TItem>(resourceStream);
        }

        private string GetBlockResourcePath<TItem>(string blockIdName) where TItem : BlockItem
        {
            string blockDefaultFilename = BlockDefaultFilenames.GetDefaultFilename<TItem>();
            string blockIdFilename = $"{blockIdName}.bin";
            return $"{blockDefaultFilename}.{blockIdFilename}";
        }
    }
}
