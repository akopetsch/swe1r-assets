// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization.Nodes;
using SWE1R.Assets.Blocks.Original.SQLite.Entities.ModelBlock;
using System.Data.Entity;

namespace SWE1R.Assets.Blocks.Original.SQLite.Entities
{
    public abstract class DbBlockItemStructures
    {
        #region Properties

        public int BlockItemIndex { get; set; }

        #endregion

        #region Constructor

        public DbBlockItemStructures(int blockItemIndex) =>
            BlockItemIndex = blockItemIndex;

        #endregion

        #region Methods

        public abstract void Load(AssetsDbContext context);

        public abstract void Load(Graph g);

        protected List<T> GetStructures<T>(DbSet<T> dbSet) where T : DbBlockItemStructure =>
            dbSet.AsNoTracking().OfBlockItem(BlockItemIndex).OrderByOffset().ToList();
        // use DbSet.AsNoTracking() to improve performance:
        // https://stackoverflow.com/a/18169894

        protected List<TEntity> GetStructures<TSource, TEntity>(Graph graph, Func<TSource, bool> filter = null)
            where TEntity : DbBlockItemStructure<TSource>, new()
        {
            var valueComponents = graph.GetValueComponents<TSource>().ToList();
            if (filter != null)
                valueComponents = valueComponents.Where(x => filter.Invoke((TSource)x.Value)).ToList();
            var entities = new List<TEntity>(valueComponents.Count);
            foreach (var c in valueComponents)
            {
                var entity = new TEntity();
                entity.CopyFrom(c.Node);
                entities.Add(entity);
            }
            return entities.OrderByOffset().ToList();
        }

        #endregion
    }
}
