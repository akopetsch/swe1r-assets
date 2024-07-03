// SPDX-License-Identifier: MIT

using Microsoft.EntityFrameworkCore;
using SWE1R.Assets.Blocks.Metadata;
using SWE1R.Assets.Blocks.Original.SQLite.Entities.ModelBlock;
using SWE1R.Assets.Blocks.Original.SQLite.Entities.ModelBlock.Anims;
using SWE1R.Assets.Blocks.Original.SQLite.Entities.ModelBlock.Behaviours;
using SWE1R.Assets.Blocks.Original.SQLite.Entities.ModelBlock.F3DEX2;
using SWE1R.Assets.Blocks.Original.SQLite.Entities.ModelBlock.Materials;
using SWE1R.Assets.Blocks.Original.SQLite.Entities.ModelBlock.Meshes;
using SWE1R.Assets.Blocks.Original.SQLite.Entities.ModelBlock.Nodes;
using SWE1R.Assets.Blocks.Original.SQLite.Entities.SpriteBlock;

namespace SWE1R.Assets.Blocks.Original.SQLite
{
    public class AssetsDbContext : DbContext
    {
        #region Properties

        public DbSet<BlockMetadata> Blocks { get; set; }
        public DbSet<BlockItemMetadata> BlockItems { get; set; }
        public DbSet<BlockItemValueMetadata> BlockItemValues { get; set; }

        #endregion

        #region Properties (Model)

        public DbSet<DbModel> Models { get; set; }

        public DbSet<DbFlaggedNodeOrInteger> FlaggedNodeOrIntegers { get; set; }
        public DbSet<DbFlaggedNodeOrLodSelectorNodeChildReference> FlaggedNodeOrLodSelectorNodeChildReferences { get; set; }
        public DbSet<DbDataInteger> Data_Integers { get; set; }
        public DbSet<DbDataLightStreak> Data_LightStreaks { get; set; }

        // Animations
        public DbSet<DbAnimation> Animations { get; set; }
        public DbSet<DbMeshMaterialReference> MeshMaterialReferences { get; set; }

        // Behaviours
        public DbSet<DbBehaviour> Behaviours { get; set; }
        public DbSet<DbTriggerDescription> TriggerDescriptions { get; set; }
        public DbSet<DbTriggerReference> TriggerReferences { get; set; }

        // F3DEX2
        public DbSet<DbGsp1TriangleCommand> Gsp1TriangleCommands { get; set; }
        public DbSet<DbGsp2TrianglesCommand> Gsp2TrianglesCommands { get; set; }
        public DbSet<DbGspCullDisplayListCommand> GspCullDisplayListCommands { get; set; }
        public DbSet<DbGspVertexCommand> GspVertexCommands { get; set; }
        public DbSet<DbVtx> Vtxs { get; set; }

        // Materials
        public DbSet<DbMaterial> Materials { get; set; }
        public DbSet<DbMaterialTexture> MaterialTextures { get; set; }
        public DbSet<DbMaterialTextureChild> MaterialTextureChildren { get; set; }
        public DbSet<DbMeshMaterial> MeshMaterials { get; set; }

        // Meshes
        public DbSet<DbMesh> Meshes { get; set; }
        
        // Nodes
        public DbSet<DbMeshGroupNode> Nodes_MeshGroupNodes { get; set; }
        public DbSet<DbBasicNode> Nodes_BasicNodes { get; set; }
        public DbSet<DbSelectorNode> Nodes_SelectorNodes { get; set; }
        public DbSet<DbLodSelectorNode> Nodes_LodSelectorNodes { get; set; }
        public DbSet<DbTransformedNode> Nodes_TransformedNodes { get; set; }
        public DbSet<DbTransformedWithPivotNode> Nodes_TransformedWithPivotNodes { get; set; }
        public DbSet<DbTransformedComputedNode> Nodes_TransformedComputedNodes { get; set; }
        
        #endregion

        #region Properties (Sprite)

        public DbSet<DbSprite> Sprites { get; set; }

        public DbSet<DbSpriteTile> SpritePages { get; set; }

        #endregion

        #region Methods (: DbContext)

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
            optionsBuilder.UseSqlite("Data Source=AssetsDb/AssetsDb.sqlite");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // composite primary keys:
            modelBuilder.Entity<BlockMetadata>()
                .HasKey(x => new { x.BlockItemType, x.Id });
            modelBuilder.Entity<BlockItemMetadata>()
                .HasKey(x => new { x.BlockItemType, x.BlockId, x.Index });
            modelBuilder.Entity<BlockItemValueMetadata>()
                .HasKey(x => new { x.BlockItemType, x.Id });
        }

        #endregion

        #region Methods

        public void AddModelStructures(DbModelStructures dbModelStructures)
        {
            Models.AddRange(dbModelStructures.Models);

            FlaggedNodeOrIntegers.AddRange(dbModelStructures.FlaggedNodeOrIntegers);
            FlaggedNodeOrLodSelectorNodeChildReferences.AddRange(dbModelStructures.FlaggedNodeOrLodSelectorNodeChildReferences);
            Data_Integers.AddRange(dbModelStructures.Data_Integers);
            Data_LightStreaks.AddRange(dbModelStructures.Data_LightStreaks);

            // Animations
            Animations.AddRange(dbModelStructures.Animations);
            MeshMaterialReferences.AddRange(dbModelStructures.MeshMaterialReferences);

            // Behaviours
            Behaviours.AddRange(dbModelStructures.Behaviours);
            TriggerDescriptions.AddRange(dbModelStructures.TriggerDescriptions);
            TriggerReferences.AddRange(dbModelStructures.TriggerReferences);

            // F3DEX2
            Gsp1TriangleCommands.AddRange(dbModelStructures.Gsp1TriangleCommands);
            Gsp2TrianglesCommands.AddRange(dbModelStructures.Gsp2TrianglesCommands);
            GspCullDisplayListCommands.AddRange(dbModelStructures.GspCullDisplayListCommands);
            GspVertexCommands.AddRange(dbModelStructures.GspVertexCommands);
            Vtxs.AddRange(dbModelStructures.Vtxs);

            // Materials
            Materials.AddRange(dbModelStructures.Materials);
            MaterialTextures.AddRange(dbModelStructures.MaterialTextures);
            MaterialTextureChildren.AddRange(dbModelStructures.MaterialTextureChildren);
            MeshMaterials.AddRange(dbModelStructures.MeshMaterials);

            // Meshes
            Meshes.AddRange(dbModelStructures.Meshes);

            // Nodes
            Nodes_BasicNodes.AddRange(dbModelStructures.Nodes_BasicNodes);
            Nodes_LodSelectorNodes.AddRange(dbModelStructures.Nodes_LodSelectorNodes);
            Nodes_MeshGroupNodes.AddRange(dbModelStructures.Nodes_MeshGroupNodes);
            Nodes_SelectorNodes.AddRange(dbModelStructures.Nodes_SelectorNodes);
            Nodes_TransformedComputedNodes.AddRange(dbModelStructures.Nodes_TransformedComputedNodes);
            Nodes_TransformedNodes.AddRange(dbModelStructures.Nodes_TransformedNodes);
            Nodes_TransformedWithPivotNodes.AddRange(dbModelStructures.Nodes_TransformedWithPivotNodes);
        }

        public void AddSpriteStructures(DbSpriteStructures dbSpriteStructures)
        {
            Sprites.AddRange(dbSpriteStructures.Sprites);

            SpritePages.AddRange(dbSpriteStructures.SpriteTiles);
        }

        #endregion
    }
}
