// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization.Nodes;
using SWE1R.Assets.Blocks.ModelBlock;
using SWE1R.Assets.Blocks.ModelBlock.Animations;
using SWE1R.Assets.Blocks.ModelBlock.Meshes;
using SWE1R.Assets.Blocks.ModelBlock.Meshes.VertexIndices;
using SWE1R.Assets.Blocks.ModelBlock.Nodes;
using SWE1R.Assets.Blocks.Original.SQLite.Entities.ModelBlock.Anims;
using SWE1R.Assets.Blocks.Original.SQLite.Entities.ModelBlock.Meshes;
using SWE1R.Assets.Blocks.Original.SQLite.Entities.ModelBlock.Meshes.VertexIndices;
using SWE1R.Assets.Blocks.Original.SQLite.Entities.ModelBlock.Nodes;

namespace SWE1R.Assets.Blocks.Original.SQLite.Entities.ModelBlock
{
    public class DbModelStructures : IEquatable<DbModelStructures>
    {
        #region Properties

        public int Model { get; }

        #endregion

        #region Properties (entities)

        public List<DbAnimation> Anims { get; set; }
        public List<DbDoubleMaterial> DoubleMaterials { get; set; }

        public List<DbIndicesChunk01> IndicesChunks01 { get; set; }
        public List<DbIndicesChunk03> IndicesChunks03 { get; set; }
        public List<DbIndicesChunk05> IndicesChunks05 { get; set; }
        public List<DbIndicesChunk06> IndicesChunks06 { get; set; }

        public List<DbMapping> Mappings { get; set; }
        public List<DbMappingChild> MappingChildren { get; set; }
        public List<DbMappingSub> MappingSubs { get; set; }
        public List<DbMaterial> Materials { get; set; }
        public List<DbMaterialProperties> MaterialProperties { get; set; }
        public List<DbMaterialTexture> MaterialTextures { get; set; }
        public List<DbMaterialTextureChild> MaterialTextureChilds { get; set; }
        public List<DbMesh> Meshes { get; set; }
        public List<DbVertex> Vertices { get; set; }

        public List<DbNode3064> Nodes3064 { get; set; }
        public List<DbNode5064> Nodes5064 { get; set; }
        public List<DbNode5065> Nodes5065 { get; set; }
        public List<DbNode5066> Nodes5066 { get; set; }
        public List<DbNodeD064> NodesD064 { get; set; }
        public List<DbNodeD065> NodesD065 { get; set; }
        public List<DbNodeD066> NodesD066 { get; set; }

        public List<DbModelHeader> Models { get; set; }
        public List<DbModelHeaderNode> HeaderNodes { get; set; }
        public List<DbModelHeaderAltN> HeaderAltN { get; set; }
        public List<DbDataLStr> Data_LStr { get; set; }
        public List<DbDataInt> Data_Int { get; set; }

        #endregion

        #region Constructor

        private DbModelStructures(int model) =>
            Model = model;

        #endregion

        #region Methods (load)

        public static DbModelStructures Load(int model, AssetsDbContext context)
        {
            var dbModelStructures = new DbModelStructures(model);

            // use DbSet.AsNoTracking() to improve performance:
            // https://stackoverflow.com/a/18169894

            dbModelStructures.Anims = context.Anims.AsNoTracking().OfModel(model).OrderByOffset().ToList();
            dbModelStructures.DoubleMaterials = context.DoubleMaterials.AsNoTracking().OfModel(model).OrderByOffset().ToList();

            dbModelStructures.IndicesChunks01 = context.IndicesChunks01.AsNoTracking().OfModel(model).OrderByOffset().ToList();
            dbModelStructures.IndicesChunks03 = context.IndicesChunks03.AsNoTracking().OfModel(model).OrderByOffset().ToList();
            dbModelStructures.IndicesChunks05 = context.IndicesChunks05.AsNoTracking().OfModel(model).OrderByOffset().ToList();
            dbModelStructures.IndicesChunks06 = context.IndicesChunks06.AsNoTracking().OfModel(model).OrderByOffset().ToList();

            dbModelStructures.Mappings = context.Mappings.AsNoTracking().OfModel(model).OrderByOffset().ToList();
            dbModelStructures.MappingChildren = context.MappingChildren.AsNoTracking().OfModel(model).OrderByOffset().ToList();
            dbModelStructures.MappingSubs = context.MappingSubs.AsNoTracking().OfModel(model).OrderByOffset().ToList();
            dbModelStructures.Materials = context.Materials.AsNoTracking().OfModel(model).OrderByOffset().ToList();
            dbModelStructures.MaterialProperties = context.MaterialProperties.AsNoTracking().OfModel(model).OrderByOffset().ToList();
            dbModelStructures.MaterialTextures = context.MaterialTextures.AsNoTracking().OfModel(model).OrderByOffset().ToList();
            dbModelStructures.MaterialTextureChilds = context.MaterialTextureChilds.AsNoTracking().OfModel(model).OrderByOffset().ToList();
            dbModelStructures.Meshes = context.Meshes.AsNoTracking().OfModel(model).OrderByOffset().ToList();
            dbModelStructures.Vertices = context.Vertices.AsNoTracking().OfModel(model).OrderByOffset().ToList();

            dbModelStructures.Nodes3064 = context.Nodes3064.AsNoTracking().OfModel(model).OrderByOffset().ToList();
            dbModelStructures.Nodes5064 = context.Nodes5064.AsNoTracking().OfModel(model).OrderByOffset().ToList();
            dbModelStructures.Nodes5065 = context.Nodes5065.AsNoTracking().OfModel(model).OrderByOffset().ToList();
            dbModelStructures.Nodes5066 = context.Nodes5066.AsNoTracking().OfModel(model).OrderByOffset().ToList();
            dbModelStructures.NodesD064 = context.NodesD064.AsNoTracking().OfModel(model).OrderByOffset().ToList();
            dbModelStructures.NodesD065 = context.NodesD065.AsNoTracking().OfModel(model).OrderByOffset().ToList();
            dbModelStructures.NodesD066 = context.NodesD066.AsNoTracking().OfModel(model).OrderByOffset().ToList();

            dbModelStructures.Models = context.Headers.AsNoTracking().OfModel(model).OrderByOffset().ToList();
            dbModelStructures.HeaderNodes = context.HeaderNodes.AsNoTracking().OfModel(model).OrderByOffset().ToList();
            dbModelStructures.HeaderAltN = context.HeaderAltN.AsNoTracking().OfModel(model).OrderByOffset().ToList();
            dbModelStructures.Data_LStr = context.Data_LStr.AsNoTracking().OfModel(model).OrderByOffset().ToList();
            dbModelStructures.Data_Int = context.Data_Int.AsNoTracking().OfModel(model).OrderByOffset().ToList();

            return dbModelStructures;
        }

        public static DbModelStructures Load(int model, Graph g)
        {
            var dbModelStructures = new DbModelStructures(model);

            dbModelStructures.Anims = Get<Animation, DbAnimation>(g).OrderByOffset().ToList();
            dbModelStructures.DoubleMaterials = Get<DoubleMaterial, DbDoubleMaterial>(g).OrderByOffset().ToList();

            dbModelStructures.IndicesChunks01 = Get<IndicesChunk01, DbIndicesChunk01>(g).OrderByOffset().ToList();
            dbModelStructures.IndicesChunks03 = Get<IndicesChunk03, DbIndicesChunk03>(g).OrderByOffset().ToList();
            dbModelStructures.IndicesChunks05 = Get<IndicesChunk05, DbIndicesChunk05>(g).OrderByOffset().ToList();
            dbModelStructures.IndicesChunks06 = Get<IndicesChunk06, DbIndicesChunk06>(g).OrderByOffset().ToList();

            dbModelStructures.Mappings = Get<Mapping, DbMapping>(g).OrderByOffset().ToList();
            dbModelStructures.MappingChildren = Get<MappingChild, DbMappingChild>(g).OrderByOffset().ToList();
            dbModelStructures.MappingSubs = Get<MappingSub, DbMappingSub>(g).OrderByOffset().ToList();
            dbModelStructures.Materials = Get<Material, DbMaterial>(g).OrderByOffset().ToList();
            dbModelStructures.MaterialProperties = Get<MaterialProperties, DbMaterialProperties>(g).OrderByOffset().ToList();
            dbModelStructures.MaterialTextures = Get<MaterialTexture, DbMaterialTexture>(g).OrderByOffset().ToList();
            dbModelStructures.MaterialTextureChilds = Get<MaterialTextureChild, DbMaterialTextureChild>(g).OrderByOffset().ToList();
            dbModelStructures.Meshes = Get<Mesh, DbMesh>(g).OrderByOffset().ToList();
            dbModelStructures.Vertices = Get<Vertex, DbVertex>(g).OrderByOffset().ToList();

            dbModelStructures.Nodes3064 = Get<MeshGroup3064, DbNode3064>(g).OrderByOffset().ToList();
            dbModelStructures.Nodes5064 = Get<Group5064, DbNode5064>(g).OrderByOffset().ToList();
            dbModelStructures.Nodes5065 = Get<Group5065, DbNode5065>(g).OrderByOffset().ToList();
            dbModelStructures.Nodes5066 = Get<Group5066, DbNode5066>(g).OrderByOffset().ToList();
            dbModelStructures.NodesD064 = Get<TransformableD064, DbNodeD064>(g).OrderByOffset().ToList();
            dbModelStructures.NodesD065 = Get<TransformableD065, DbNodeD065>(g).OrderByOffset().ToList();
            dbModelStructures.NodesD066 = Get<UnknownD066, DbNodeD066>(g).OrderByOffset().ToList();

            dbModelStructures.Models = Get<Model, DbModelHeader>(g).OrderByOffset().ToList();
            dbModelStructures.HeaderNodes = Get<FlaggedNodeOrInteger, DbModelHeaderNode>(g).OrderByOffset().ToList();
            dbModelStructures.HeaderAltN = Get<FlaggedNodeOrGroup5066ChildReference, DbModelHeaderAltN> (g).OrderByOffset().ToList();
            dbModelStructures.Data_LStr = Get<LightStreakOrInteger, DbDataLStr>(g, x => x.LightStreak != null).OrderByOffset().ToList();
            dbModelStructures.Data_Int = Get<LightStreakOrInteger, DbDataInt>(g, x => x.Integer.HasValue).OrderByOffset().ToList();

            return dbModelStructures;
        }

        private static IEnumerable<TDatabase> Get<TSource, TDatabase>(Graph graph, Func<TSource, bool> filter = null) 
            where TDatabase : DbBlockItemStructure<TSource>, new()
        {
            var valueComponents = graph.GetValueComponents<TSource>();
            if (filter != null)
                valueComponents = valueComponents.Where(x => filter.Invoke((TSource)x.Value));
            foreach (var c in valueComponents)
            {
                var s = new TDatabase();
                s.CopyFrom(c.Node);
                yield return s;
            }
        }

        #endregion

        #region Methods (: IEquatable<DbModelStructures>)

        public bool Equals(DbModelStructures other)
        {
            if (!Anims.SequenceEqual(other.Anims)) return false;
            if (!DoubleMaterials.SequenceEqual(other.DoubleMaterials)) return false;

            if (!IndicesChunks01.SequenceEqual(other.IndicesChunks01)) return false;
            if (!IndicesChunks03.SequenceEqual(other.IndicesChunks03)) return false;
            if (!IndicesChunks05.SequenceEqual(other.IndicesChunks05)) return false;
            if (!IndicesChunks06.SequenceEqual(other.IndicesChunks06)) return false;

            if (!Mappings.SequenceEqual(other.Mappings)) return false;
            if (!MappingChildren.SequenceEqual(other.MappingChildren)) return false;
            if (!MappingSubs.SequenceEqual(other.MappingSubs)) return false;
            if (!Materials.SequenceEqual(other.Materials)) return false;
            if (!MaterialProperties.SequenceEqual(other.MaterialProperties)) return false;
            if (!MaterialTextures.SequenceEqual(other.MaterialTextures)) return false;
            if (!MaterialTextureChilds.SequenceEqual(other.MaterialTextureChilds)) return false;
            if (!Meshes.SequenceEqual(other.Meshes)) return false;
            if (!Vertices.SequenceEqual(other.Vertices)) return false;

            if (!Nodes3064.SequenceEqual(other.Nodes3064)) return false;
            if (!Nodes5064.SequenceEqual(other.Nodes5064)) return false;
            if (!Nodes5065.SequenceEqual(other.Nodes5065)) return false;
            if (!Nodes5066.SequenceEqual(other.Nodes5066)) return false;
            if (!NodesD064.SequenceEqual(other.NodesD064)) return false;
            if (!NodesD065.SequenceEqual(other.NodesD065)) return false;
            if (!NodesD066.SequenceEqual(other.NodesD066)) return false;

            if (!Models.SequenceEqual(other.Models)) return false;
            if (!HeaderNodes.SequenceEqual(other.HeaderNodes)) return false;
            if (!HeaderAltN.SequenceEqual(other.HeaderAltN)) return false;
            if (!Data_LStr.SequenceEqual(other.Data_LStr)) return false;
            if (!Data_Int.SequenceEqual(other.Data_Int)) return false;

            return true;
        }

        #endregion
    }
}
