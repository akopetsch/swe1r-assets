// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using SWE1R.Assets.Blocks.Metadata;
using SWE1R.Assets.Blocks.Original.SQLite.Entities.ModelBlock;
using SWE1R.Assets.Blocks.Original.SQLite.Entities.ModelBlock.Anims;
using SWE1R.Assets.Blocks.Original.SQLite.Entities.ModelBlock.Meshes;
using SWE1R.Assets.Blocks.Original.SQLite.Entities.ModelBlock.Meshes.VertexIndices;
using SWE1R.Assets.Blocks.Original.SQLite.Entities.ModelBlock.Nodes;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.SQLite;

namespace SWE1R.Assets.Blocks.Original.SQLite
{
    public class AssetsDbContext : DbContext
    {
        #region Properties

        public DbSet<BlockMetadata> Blocks { get; set; }
        public DbSet<BlockItemMetadata> BlockItems { get; set; }
        public DbSet<BlockItemMetadataByValue> BlockItemValues { get; set; }

        public DbSet<DbAnimation> Anims { get; set; }
        public DbSet<DbDoubleMaterial> DoubleMaterials { get; set; }

        public DbSet<DbIndicesChunk01> IndicesChunks01 { get; set; }
        public DbSet<DbIndicesChunk03> IndicesChunks03 { get; set; }
        public DbSet<DbIndicesChunk05> IndicesChunks05 { get; set; }
        public DbSet<DbIndicesChunk06> IndicesChunks06 { get; set; }

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

        #region Constructor

        public AssetsDbContext() :
            base(new SQLiteConnection("data source=AssetsDb/AssetsDb.sqlite"), true)
        { }

        #endregion
        
        #region Methods

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
        }

        public void AddModelStructures(DbModelStructures dbModelStructures)
        {
            Anims.AddRange(dbModelStructures.Anims);
            DoubleMaterials.AddRange(dbModelStructures.DoubleMaterials);

            IndicesChunks01.AddRange(dbModelStructures.IndicesChunks01);
            IndicesChunks03.AddRange(dbModelStructures.IndicesChunks03);
            IndicesChunks05.AddRange(dbModelStructures.IndicesChunks05);
            IndicesChunks06.AddRange(dbModelStructures.IndicesChunks06);

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

        #endregion
    }
}
