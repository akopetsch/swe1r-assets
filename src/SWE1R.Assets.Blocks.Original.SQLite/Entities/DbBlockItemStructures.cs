// SPDX-License-Identifier: GPL-2.0-only

using ByteSerialization.Nodes;
using Microsoft.EntityFrameworkCore;
using SWE1R.Assets.Blocks.Original.SQLite.Entities.ModelBlock;

namespace SWE1R.Assets.Blocks.Original.SQLite.Entities
{
    public abstract class DbBlockItemStructures
    {
        #region Properties

        public int BlockItemValueId { get; set; }

        #endregion

        #region Constructor

        public DbBlockItemStructures(int blockItemValueId) =>
            BlockItemValueId = blockItemValueId;

        #endregion

        #region Methods

        public abstract void Load(AssetsDbContext context);

        public abstract void Load(ByteSerializerGraph g);

        protected List<T> GetStructures<T>(DbSet<T> dbSet) where T : DbBlockItemStructure =>
            dbSet.AsNoTracking().OfBlockItem(BlockItemValueId).OrderByOffset().ToList();
        // use DbSet.AsNoTracking() to improve performance:
        // https://stackoverflow.com/a/18169894

        protected List<TEntity> GetStructures<TSource, TEntity>(ByteSerializerGraph graph, Func<TSource, bool> filter = null)
            where TEntity : DbBlockItemStructure<TSource>, new()
        {
            var valueComponents = graph.GetValueComponents<TSource>().ToList();
            if (filter != null)
                valueComponents = valueComponents.Where(x => filter.Invoke((TSource)x.Value)).ToList();
            var entities = new List<TEntity>(valueComponents.Count);
            foreach (var c in valueComponents)
            {
                var entity = new TEntity() {
                    BlockItemValueId = BlockItemValueId
                };
                entity.CopyFrom(c.Node);
                entities.Add(entity);
            }
            return entities.OrderByOffset().ToList();
        }

        #endregion
    }
}
