// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using SWE1R.Assets.Blocks;
using SWE1R.Assets.Blocks.Metadata;
using SWE1R.Assets.Blocks.ModelBlock;
using SWE1R.Assets.Blocks.Original;
using SWE1R.Assets.Blocks.SplineBlock;
using SWE1R.Assets.Blocks.SpriteBlock;
using SWE1R.Assets.Blocks.TextureBlock;
using System.Text;

namespace FiddleApp
{
    public class TestClassGenerator
    {
        private readonly MetadataProvider _metadataProvider = new();
        private readonly OriginalBlocksProvider _originalBlockProvider = new();

        public void Generate()
        {
            _originalBlockProvider.Init();
            string modelsSnippet = GenerateFoo<ModelBlockItem>();
            string spritesSnippet = GenerateFoo<SpriteBlockItem>();
            string splinesSnippet = GenerateFoo<SplineBlockItem>();
            string texturesSnippet = GenerateFoo<TextureBlockItem>();
        }

        private string GenerateFoo<TBlockItem>() where TBlockItem : BlockItem, new()
        {
            var sb = new StringBuilder();
            foreach (BlockItemValueMetadata blockItemValueMetadata in _metadataProvider.GetBlockItemValues<TBlockItem>())
            {
                int id = blockItemValueMetadata.Id;
                string tabs = new string(' ', 4 * 2);
                sb.AppendLine($"{tabs}[Fact]\r\n{tabs}public void Test_{id:d5}() => CompareItem({id});");
            }
            return sb.ToString();
        }
    }
}
