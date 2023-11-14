// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

namespace SWE1R.Assets.Blocks.Original
{
    public class OriginalBlockProvider
    {
        private readonly BlockDefaultFilenames blockDefaultFilenames = new();

        public Block<TItem> LoadBlock<TItem>(string blockIdName) where TItem : BlockItem, new() =>
            Block.Load<TItem>(GetBlockPath<TItem>(blockIdName));

        public string GetBlockPath<TItem>(string blockIdName) where TItem : BlockItem, new()
        {
            string folderName = blockDefaultFilenames.GetDefaultFilename<TItem>();
            string filename = $"{blockIdName}.bin";
            return Path.Combine(folderName, filename);
        }
    }
}
