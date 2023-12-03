// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization;
using SWE1R.Assets.Blocks.Metadata.IdNames;
using SWE1R.Assets.Blocks.Original.SQLite.Entities.SpriteBlock;
using SWE1R.Assets.Blocks.SpriteBlock;

namespace SWE1R.Assets.Blocks.Original.SQLite.CommandLine
{
    public class AssetsDbGenerator
    {
        public AssetsDbContext AssetsDbContext { get; }
        public OriginalBlocksProvider OriginalBlocksProvider { get; }

        public AssetsDbGenerator(AssetsDbContext assetsDbContext)
        {
            AssetsDbContext = assetsDbContext;
        }

        public void Generate()
        {
            OriginalBlocksProvider.Load();

            var spriteBlock = OriginalBlocksProvider.GetBlock<SpriteBlockItem>(SpriteBlockIdNames.Default);

            foreach (SpriteBlockItem spriteBlockItem in spriteBlock)
            {
                Console.WriteLine(spriteBlockItem.Index);
                spriteBlockItem.Load(out ByteSerializerContext byteSerializerContext);
                var dbSpriteStructures = new DbSpriteStructures(spriteBlockItem.Index.Value);
                dbSpriteStructures.Load(byteSerializerContext.Graph);
                AssetsDbContext.AddSpriteStructures(dbSpriteStructures);
            }
            AssetsDbContext.SaveChanges();
        }
    }
}
