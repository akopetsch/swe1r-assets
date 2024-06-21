// SPDX-License-Identifier: MIT

using ByteSerialization;
using SWE1R.Assets.Blocks.Metadata;
using SWE1R.Assets.Blocks.ModelBlock;
using SWE1R.Assets.Blocks.Original.SQLite.Entities;
using SWE1R.Assets.Blocks.Original.SQLite.Entities.ModelBlock;
using SWE1R.Assets.Blocks.Original.SQLite.Entities.SpriteBlock;
using SWE1R.Assets.Blocks.SpriteBlock;

namespace SWE1R.Assets.Blocks.Original.SQLite.CommandLine
{
    public class AssetsDbGenerator
    {
        #region Properties

        public AssetsDbContext AssetsDbContext { get; }
        public OriginalBlocksProvider OriginalBlocksProvider { get; }
        public MetadataProvider MetadataProvider { get; }

        #endregion

        #region Constructor

        public AssetsDbGenerator(AssetsDbContext assetsDbContext)
        {
            AssetsDbContext = assetsDbContext;
            OriginalBlocksProvider = new();
            MetadataProvider = new();
        }

        #endregion

        #region Methods

        public void Generate()
        {
            OriginalBlocksProvider.Load();

            AssetsDbContext.Database.EnsureDeleted();
            AssetsDbContext.Database.EnsureCreated();

            ImportModels();
            ImportSprites();
            
            AssetsDbContext.SaveChanges();
        }

        private void ImportModels()
        {
            Console.WriteLine($"{nameof(ImportModels)}()");
            ImportBlockItems<ModelBlockItem, DbModelStructures>((x, y) => x.AddModelStructures(y));
        }

        private void ImportSprites()
        {
            Console.WriteLine($"{nameof(ImportSprites)}()");
            ImportBlockItems<SpriteBlockItem, DbSpriteStructures>((x, y) => x.AddSpriteStructures(y));
        }

        private void ImportBlockItems<TBlockItem, TDbBlockItemStructures>(Action<AssetsDbContext, TDbBlockItemStructures> dbAddAction)
            where TBlockItem : BlockItem, new()
            where TDbBlockItemStructures: DbBlockItemStructures, new()
        {
            (int valueId, TBlockItem blockItem)[] blockItemsByValueId = GetBlockItemsByValueId<TBlockItem>();
            foreach ((int valueId, TBlockItem blockItem) in blockItemsByValueId)
            {
                Console.WriteLine(valueId);
                blockItem.Load(out ByteSerializerContext byteSerializerContext);
                var dbBlockItemStructures = new TDbBlockItemStructures
                {
                    BlockItemValueId = valueId
                };
                dbBlockItemStructures.Load(byteSerializerContext.Graph);
                dbAddAction.Invoke(AssetsDbContext, dbBlockItemStructures);
            }
        }

        private (int valueId, TItem blockItem)[] GetBlockItemsByValueId<TItem>() 
            where TItem : BlockItem, new() =>
            MetadataProvider
            .GetBlockItemValues<TItem>()
            .Select(x => (x.Id, OriginalBlocksProvider.GetFirstBlockItemByValueId<TItem>(x.Id)))
            .ToArray();

        #endregion
    }
}
