// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using System.IO;

namespace SWE1R.Assets.Blocks
{
    public static class Block
    {
        public static Block<TItem> Load<TItem>(string filename) where TItem : BlockItem, new()
        {
            var block = new Block<TItem>();
            block.Load(filename);
            return block;
        }

        public static Block<TItem> Load<TItem>(Stream stream) where TItem : BlockItem, new()
        {
            var block = new Block<TItem>();
            block.Load(stream);
            return block;
        }
    }
}
