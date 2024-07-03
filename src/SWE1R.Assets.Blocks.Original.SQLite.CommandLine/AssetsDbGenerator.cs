// SPDX-License-Identifier: MIT

using ByteSerialization;
using Spectre.Console;
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
            AnsiConsole.WriteLine("Load original blocks");
            OriginalBlocksProvider.Load();

            AnsiConsole.WriteLine("Re-create database");
            AssetsDbContext.Database.EnsureDeleted();
            AssetsDbContext.Database.EnsureCreated();

            AnsiConsole.WriteLine("Import block items");
            ImportModels();
            ImportSprites();

            AnsiConsole.WriteLine("Save database");
            AssetsDbContext.SaveChanges();
        }

        private void ImportModels() =>
            ImportBlockItems<ModelBlockItem, DbModelStructures>(
                (x, y) => x.AddModelStructures(y));

        private void ImportSprites() =>
            ImportBlockItems<SpriteBlockItem, DbSpriteStructures>(
                (x, y) => x.AddSpriteStructures(y));

        private void ImportBlockItems<TBlockItem, TDbBlockItemStructures>(
            Action<AssetsDbContext, TDbBlockItemStructures> dbAddAction)
            where TBlockItem : BlockItem, new()
            where TDbBlockItemStructures: DbBlockItemStructures, new()
        {
            AnsiConsole.Progress().Start(ctx =>
            {
                ProgressTask itemsTask = ctx.AddTask(GetItemsTaskDescription<TBlockItem>());
                ProgressTask currentItemTask = null;
                
                (int valueId, TBlockItem blockItem)[] blockItemsByValueId = GetBlockItemsByValueId<TBlockItem>();
                itemsTask.MaxValue = blockItemsByValueId.Length - 1;
                foreach ((int valueId, TBlockItem blockItem) in blockItemsByValueId)
                {
                    currentItemTask ??= AddCurrentItemTask(ctx, valueId);
                    currentItemTask.Value = 0;
                    currentItemTask.Description = GetCurrentItemTaskDescription(valueId);

                    blockItem.Load(out ByteSerializerContext byteSerializerContext);
                    var dbBlockItemStructures = new TDbBlockItemStructures
                    {
                        BlockItemValueId = valueId
                    };
                    dbBlockItemStructures.Load(byteSerializerContext.Graph);
                    dbAddAction.Invoke(AssetsDbContext, dbBlockItemStructures);
                    
                    itemsTask.Value++;
                    currentItemTask.Value = itemsTask.MaxValue;
                }
            });
        }

        private (int valueId, TItem blockItem)[] GetBlockItemsByValueId<TItem>()
            where TItem : BlockItem, new() =>
            MetadataProvider
            .GetBlockItemValues<TItem>()
            .Select(x => (x.Id, OriginalBlocksProvider.GetFirstBlockItemByValueId<TItem>(x.Id)))
            .ToArray();

        #endregion

        #region Methods (task progress)

        private string GetItemsTaskDescription<TBlockItem>() where TBlockItem : BlockItem =>
            $"Importing {typeof(TBlockItem).Name}s";

        private ProgressTask AddCurrentItemTask(ProgressContext ctx, int valueId)
        {
            ProgressTask task = ctx.AddTask(GetCurrentItemTaskDescription(valueId));
            task.IsIndeterminate = true;
            return task;
        }

        private string GetCurrentItemTaskDescription(int valueId) =>
            $"... (current {nameof(valueId)}: {valueId})";

        #endregion
    }
}
