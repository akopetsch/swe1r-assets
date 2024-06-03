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

        public DbSet<DbAnimation> Anims { get; set; }
        public DbSet<DbMaterialReference> DoubleMaterials { get; set; }

        public DbSet<DbN64GspVertexCommand> N64GspVertexCommands { get; set; }
        public DbSet<DbN64GspCullDisplayListCommand> N64GspCullDisplayListCommands { get; set; }
        public DbSet<DbN64Gsp1TriangleCommand> N64Gsp1TriangleCommands { get; set; }
        public DbSet<DbN64Gsp2TrianglesCommand> N64Gsp2TrianglesCommands { get; set; }

        public DbSet<DbMapping> Mappings { get; set; }
        public DbSet<DbMappingChild> MappingChildren { get; set; }
        public DbSet<DbMappingSub> MappingSubs { get; set; }
        public DbSet<DbMaterial> Materials { get; set; }
        public DbSet<DbMaterialProperties> MaterialProperties { get; set; }
        public DbSet<DbMaterialTexture> MaterialTextures { get; set; }
        public DbSet<DbMaterialTextureChild> MaterialTextureChilds { get; set; }
        public DbSet<DbMesh> Meshes { get; set; }
        public DbSet<DbVertex> Vertices { get; set; }

        public DbSet<DbNode3064> Nodes3064 { get; set; }
        public DbSet<DbNode5064> Nodes5064 { get; set; }
        public DbSet<DbNode5065> Nodes5065 { get; set; }
        public DbSet<DbNode5066> Nodes5066 { get; set; }
        public DbSet<DbNodeD064> NodesD064 { get; set; }
        public DbSet<DbNodeD065> NodesD065 { get; set; }
        public DbSet<DbNodeD066> NodesD066 { get; set; }
        
        public DbSet<DbModelHeader> Headers { get; set; }
        public DbSet<DbModelHeaderNode> HeaderNodes { get; set; }
        public DbSet<DbModelHeaderAltN> HeaderAltN { get; set; }
        public DbSet<DbDataLStr> Data_LStr { get; set; }
        public DbSet<DbDataInt> Data_Int { get; set; }

        #endregion

        #region Properties (Sprite)

        public DbSet<DbSprite> Sprites { get; set; }
        public DbSet<DbSpritePage> SpritePages { get; set; }

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
            Anims.AddRange(dbModelStructures.Anims);
            DoubleMaterials.AddRange(dbModelStructures.DoubleMaterials);

            N64GspVertexCommands.AddRange(dbModelStructures.N64GspVertexCommands);
            N64GspCullDisplayListCommands.AddRange(dbModelStructures.N64GspCullDisplayListCommands);
            N64Gsp1TriangleCommands.AddRange(dbModelStructures.N64Gsp1TriangleCommands);
            N64Gsp2TrianglesCommands.AddRange(dbModelStructures.N64Gsp2TrianglesCommands);

            Mappings.AddRange(dbModelStructures.Mappings);
            MappingChildren.AddRange(dbModelStructures.MappingChildren);
            MappingSubs.AddRange(dbModelStructures.MappingSubs);
            Materials.AddRange(dbModelStructures.Materials);
            MaterialProperties.AddRange(dbModelStructures.MaterialProperties);
            MaterialTextures.AddRange(dbModelStructures.MaterialTextures);
            MaterialTextureChilds.AddRange(dbModelStructures.MaterialTextureChilds);

            Meshes.AddRange(dbModelStructures.Meshes);
            Vertices.AddRange(dbModelStructures.Vertices);

            Nodes3064.AddRange(dbModelStructures.Nodes3064);
            Nodes5064.AddRange(dbModelStructures.Nodes5064);
            Nodes5065.AddRange(dbModelStructures.Nodes5065);
            Nodes5066.AddRange(dbModelStructures.Nodes5066);
            NodesD064.AddRange(dbModelStructures.NodesD064);
            NodesD065.AddRange(dbModelStructures.NodesD065);
            NodesD066.AddRange(dbModelStructures.NodesD066);

            Headers.AddRange(dbModelStructures.Models);
            HeaderNodes.AddRange(dbModelStructures.HeaderNodes);
            HeaderAltN.AddRange(dbModelStructures.HeaderAltN);
            Data_LStr.AddRange(dbModelStructures.Data_LStr);
            Data_Int.AddRange(dbModelStructures.Data_Int);
        }

        public void AddSpriteStructures(DbSpriteStructures dbSpriteStructures)
        {
            Sprites.AddRange(dbSpriteStructures.Sprites);
            SpritePages.AddRange(dbSpriteStructures.SpritePages);
        }

        #endregion
    }
}
