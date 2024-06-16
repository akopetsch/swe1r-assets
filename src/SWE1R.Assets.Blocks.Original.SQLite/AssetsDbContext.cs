// SPDX-License-Identifier: MIT

using Microsoft.EntityFrameworkCore;
using SWE1R.Assets.Blocks.Metadata;
using SWE1R.Assets.Blocks.Original.SQLite.Entities.ModelBlock;
using SWE1R.Assets.Blocks.Original.SQLite.Entities.ModelBlock.Anims;
using SWE1R.Assets.Blocks.Original.SQLite.Entities.ModelBlock.F3DEX2;
using SWE1R.Assets.Blocks.Original.SQLite.Entities.ModelBlock.Meshes;
using SWE1R.Assets.Blocks.Original.SQLite.Entities.ModelBlock.Meshes.Behaviours;
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
        public DbSet<DbDataLightStreak> Data_LightStreaks { get; set; }
        public DbSet<DbDataInteger> Data_Integers { get; set; }

        public DbSet<DbAnimation> Animations { get; set; }
        public DbSet<DbMeshMaterialReference> MeshMaterialReferences { get; set; }

        public DbSet<DbVtx> N64Sdk_Vtxs { get; set; }
        public DbSet<DbGSpVertexCommand> N64Sdk_GSpVertexCommands { get; set; }
        public DbSet<DbGSpCullDisplayListCommand> N64Sdk_GSpCullDisplayListCommands { get; set; }
        public DbSet<DbGSp1TriangleCommand> N64Sdk_GSp1TriangleCommands { get; set; }
        public DbSet<DbGSp2TrianglesCommand> N64Sdk_GSp2TrianglesCommands { get; set; }

        public DbSet<DbMapping> Mappings { get; set; }
        public DbSet<DbMappingChild> MappingChildren { get; set; }
        public DbSet<DbMappingSub> MappingSubs { get; set; }
        public DbSet<DbMeshMaterial> MeshMaterials { get; set; }
        public DbSet<DbMaterial> Materials { get; set; }
        public DbSet<DbMaterialTexture> MaterialTextures { get; set; }
        public DbSet<DbMaterialTextureChild> MaterialTextureChildren { get; set; }
        public DbSet<DbMesh> Meshes { get; set; }
        
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
            Data_LightStreaks.AddRange(dbModelStructures.Data_LightStreaks);
            Data_Integers.AddRange(dbModelStructures.Data_Integers);

            Animations.AddRange(dbModelStructures.Animations);
            MeshMaterialReferences.AddRange(dbModelStructures.MeshMaterialReferences);

            N64Sdk_Vtxs.AddRange(dbModelStructures.N64Sdk_Vtxs);
            N64Sdk_GSpVertexCommands.AddRange(dbModelStructures.N64Sdk_GraphicsCommands_GSpVertexCommands);
            N64Sdk_GSpCullDisplayListCommands.AddRange(dbModelStructures.N64Sdk_GraphicsCommands_GSpCullDisplayListCommands);
            N64Sdk_GSp1TriangleCommands.AddRange(dbModelStructures.N64Sdk_GraphicsCommands_GSp1TriangleCommands);
            N64Sdk_GSp2TrianglesCommands.AddRange(dbModelStructures.N64Sdk_GraphicsCommands_GSp2TrianglesCommands);

            Mappings.AddRange(dbModelStructures.Mappings);
            MappingChildren.AddRange(dbModelStructures.MappingChildren);
            MappingSubs.AddRange(dbModelStructures.MappingSubs);
            MeshMaterials.AddRange(dbModelStructures.MeshMaterials);
            Materials.AddRange(dbModelStructures.Materials);
            MaterialTextures.AddRange(dbModelStructures.MaterialTextures);
            MaterialTextureChildren.AddRange(dbModelStructures.MaterialTextureChildren);

            Meshes.AddRange(dbModelStructures.Meshes);

            Nodes_MeshGroupNodes.AddRange(dbModelStructures.Nodes_MeshGroupNodes);
            Nodes_BasicNodes.AddRange(dbModelStructures.Nodes_BasicNodes);
            Nodes_SelectorNodes.AddRange(dbModelStructures.Nodes_SelectorNodes);
            Nodes_LodSelectorNodes.AddRange(dbModelStructures.Nodes_LodSelectorNodes);
            Nodes_TransformedNodes.AddRange(dbModelStructures.Nodes_TransformedNodes);
            Nodes_TransformedWithPivotNodes.AddRange(dbModelStructures.Nodes_TransformedWithPivotNodes);
            Nodes_TransformedComputedNodes.AddRange(dbModelStructures.Nodes_TransformedComputedNodes);
        }

        public void AddSpriteStructures(DbSpriteStructures dbSpriteStructures)
        {
            Sprites.AddRange(dbSpriteStructures.Sprites);
            SpritePages.AddRange(dbSpriteStructures.SpritePages);
        }

        #endregion
    }
}
