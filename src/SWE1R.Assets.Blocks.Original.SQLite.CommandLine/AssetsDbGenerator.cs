// SPDX-License-Identifier: GPL-2.0-only

using ByteSerialization;
using Microsoft.EntityFrameworkCore;
using SWE1R.Assets.Blocks.Metadata;
using SWE1R.Assets.Blocks.ModelBlock;
using SWE1R.Assets.Blocks.Original.SQLite.Entities.ModelBlock;
using SWE1R.Assets.Blocks.Original.SQLite.Entities.SpriteBlock;
using SWE1R.Assets.Blocks.SpriteBlock;

namespace SWE1R.Assets.Blocks.Original.SQLite.CommandLine
{
    public class AssetsDbGenerator
    {
        public AssetsDbContext AssetsDbContext { get; }
        public OriginalBlocksProvider OriginalBlocksProvider { get; }
        public MetadataProvider MetadataProvider { get; }

        public AssetsDbGenerator(AssetsDbContext assetsDbContext)
        {
            AssetsDbContext = assetsDbContext;
            OriginalBlocksProvider = new();
            MetadataProvider = new();
        }

        public void Generate()
        {
            OriginalBlocksProvider.Load();

            AssetsDbContext.Database.Migrate();
            ImportModels();
            ImportSprites();
            AssetsDbContext.SaveChanges();
        }

        private void ImportModels()
        {
            Console.WriteLine($"{nameof(ImportModels)}()");
            foreach ((int valueId, ModelBlockItem modelBlockItem) in GetBlockItemsByValueId<ModelBlockItem>())
            {
                Console.WriteLine(valueId);
                modelBlockItem.Load(out ByteSerializerContext byteSerializerContext);
                var dbModelStructures = new DbModelStructures(valueId);
                dbModelStructures.Load(byteSerializerContext.Graph);
                AssetsDbContext.AddModelStructures(dbModelStructures);
            }
        }

        private void ImportSprites()
        {
            Console.WriteLine($"{nameof(ImportSprites)}()");
            foreach ((int valueId, SpriteBlockItem spriteBlockItem) in GetBlockItemsByValueId<SpriteBlockItem>())
            {
                Console.WriteLine(valueId);
                spriteBlockItem.Load(out ByteSerializerContext byteSerializerContext);
                var dbSpriteStructures = new DbSpriteStructures(valueId);
                dbSpriteStructures.Load(byteSerializerContext.Graph);
                AssetsDbContext.AddSpriteStructures(dbSpriteStructures);
            }
        }

        private IEnumerable<(int valueId, TItem blockItem)> GetBlockItemsByValueId<TItem>() where TItem : BlockItem, new() =>
            MetadataProvider.GetBlockItemValues<TItem>()
            .Select(x => (x.Id, OriginalBlocksProvider.GetFirstBlockItemByValueId<TItem>(x.Id)));
    }
}
