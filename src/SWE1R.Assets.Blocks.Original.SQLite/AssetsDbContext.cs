// SPDX-License-Identifier: MIT

using Microsoft.EntityFrameworkCore;
using SWE1R.Assets.Blocks.Metadata;
using SWE1R.Assets.Blocks.Original.SQLite.Entities.ModelBlock;
using SWE1R.Assets.Blocks.Original.SQLite.Entities.ModelBlock.Anims;
using SWE1R.Assets.Blocks.Original.SQLite.Entities.ModelBlock.Meshes;
using SWE1R.Assets.Blocks.Original.SQLite.Entities.ModelBlock.Meshes.N64GspCommands;
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

        public DbSet<DbAnimation> Animations { get; set; }
        public DbSet<DbMaterialReference> MaterialReferences { get; set; }

        public DbSet<DbN64GspVertexCommand> N64GspVertexCommands { get; set; }
        public DbSet<DbN64GspCullDisplayListCommand> N64GspCullDisplayListCommands { get; set; }
        public DbSet<DbN64Gsp1TriangleCommand> N64Gsp1TriangleCommands { get; set; }
        public DbSet<DbN64Gsp2TrianglesCommand> N64Gsp2TrianglesCommands { get; set; }

        public DbSet<DbMapping> Mappings { get; set; }
        public DbSet<DbMappingChild> MappingChildren { get; set; }
        public DbSet<DbMappingSub> MappingSubs { get; set; }
        public DbSet<DbMeshMaterial> MeshMaterials { get; set; }
        public DbSet<DbMaterial> Materials { get; set; }
        public DbSet<DbMaterialTexture> MaterialTextures { get; set; }
        public DbSet<DbMaterialTextureChild> MaterialTextureChildren { get; set; }
        public DbSet<DbMesh> Meshes { get; set; }
        public DbSet<DbVertex> Vertices { get; set; }

        public DbSet<DbMeshGroupNode> Nodes_MeshGroupNodes { get; set; }
        public DbSet<DbBasicNode> Nodes_BasicNodes { get; set; }
        public DbSet<DbSelectorNode> Nodes_SelectorNodes { get; set; }
        public DbSet<DbLodSelectorNode> Nodes_LodSelectorNodes { get; set; }
        public DbSet<DbTransformedNode> Nodes_TransformedNodes { get; set; }
        public DbSet<DbTransformedWithPivotNode> Nodes_TransformedWithPivotNodes { get; set; }
        public DbSet<DbTransformedComputedNode> Nodes_TransformedComputedNodes { get; set; }
        
        public DbSet<DbModel> Models { get; set; }
        public DbSet<DbFlaggedNodeOrInteger> HeaderNodes { get; set; }
        public DbSet<DbFlaggedNodeOrLodSelectorNodeChildReference> HeaderAltN { get; set; }
        public DbSet<DbDataLightStreak> Data_LStr { get; set; }
        public DbSet<DbDataInteger> Data_Int { get; set; }

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
            Animations.AddRange(dbModelStructures.Animations);
            MaterialReferences.AddRange(dbModelStructures.MaterialReferences);

            N64GspVertexCommands.AddRange(dbModelStructures.N64GspVertexCommands);
            N64GspCullDisplayListCommands.AddRange(dbModelStructures.N64GspCullDisplayListCommands);
            N64Gsp1TriangleCommands.AddRange(dbModelStructures.N64Gsp1TriangleCommands);
            N64Gsp2TrianglesCommands.AddRange(dbModelStructures.N64Gsp2TrianglesCommands);

            Mappings.AddRange(dbModelStructures.Mappings);
            MappingChildren.AddRange(dbModelStructures.MappingChildren);
            MappingSubs.AddRange(dbModelStructures.MappingSubs);
            MeshMaterials.AddRange(dbModelStructures.MeshMaterials);
            Materials.AddRange(dbModelStructures.Materials);
            MaterialTextures.AddRange(dbModelStructures.MaterialTextures);
            MaterialTextureChildren.AddRange(dbModelStructures.MaterialTextureChildren);

            Meshes.AddRange(dbModelStructures.Meshes);
            Vertices.AddRange(dbModelStructures.Vertices);

            Nodes_MeshGroupNodes.AddRange(dbModelStructures.Nodes_MeshGroupNodes);
            Nodes_BasicNodes.AddRange(dbModelStructures.Nodes_BasicNodes);
            Nodes_SelectorNodes.AddRange(dbModelStructures.Nodes_SelectorNodes);
            Nodes_LodSelectorNodes.AddRange(dbModelStructures.Nodes_LodSelectorNodes);
            Nodes_TransformedNodes.AddRange(dbModelStructures.Nodes_TransformedNodes);
            Nodes_TransformedWithPivotNodes.AddRange(dbModelStructures.Nodes_TransformedWithPivotNodes);
            Nodes_TransformedComputedNodes.AddRange(dbModelStructures.Nodes_TransformedComputedNodes);

            Models.AddRange(dbModelStructures.Models);
            HeaderNodes.AddRange(dbModelStructures.FlaggedNodeOrIntegers);
            HeaderAltN.AddRange(dbModelStructures.FlaggedNodeOrLodSelectorNodeChildReferences);
            Data_LStr.AddRange(dbModelStructures.Data_LightStreaks);
            Data_Int.AddRange(dbModelStructures.Data_Integers);
        }

        public void AddSpriteStructures(DbSpriteStructures dbSpriteStructures)
        {
            Sprites.AddRange(dbSpriteStructures.Sprites);
            SpritePages.AddRange(dbSpriteStructures.SpritePages);
        }

        #endregion
    }
}
