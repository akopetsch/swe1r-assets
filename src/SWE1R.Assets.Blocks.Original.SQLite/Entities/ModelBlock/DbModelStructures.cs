// SPDX-License-Identifier: GPL-2.0-only

using ByteSerialization.Nodes;
using SWE1R.Assets.Blocks.ModelBlock;
using SWE1R.Assets.Blocks.ModelBlock.Animations;
using SWE1R.Assets.Blocks.ModelBlock.Materials;
using SWE1R.Assets.Blocks.ModelBlock.Meshes;
using SWE1R.Assets.Blocks.ModelBlock.Meshes.VertexIndices;
using SWE1R.Assets.Blocks.ModelBlock.Nodes;
using SWE1R.Assets.Blocks.Original.SQLite.Entities.ModelBlock.Anims;
using SWE1R.Assets.Blocks.Original.SQLite.Entities.ModelBlock.Meshes;
using SWE1R.Assets.Blocks.Original.SQLite.Entities.ModelBlock.Meshes.VertexIndices;
using SWE1R.Assets.Blocks.Original.SQLite.Entities.ModelBlock.Nodes;

namespace SWE1R.Assets.Blocks.Original.SQLite.Entities.ModelBlock
{
    public class DbModelStructures : DbBlockItemStructures, IEquatable<DbModelStructures>
    {
        #region Properties (entities)

        public List<DbAnimation> Anims { get; set; }
        public List<DbMaterialReference> DoubleMaterials { get; set; }

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

        public DbModelStructures(int blockItemIndex) : 
            base(blockItemIndex)
        { }

        #endregion

        #region Methods

        public override void Load(AssetsDbContext context)
        {
            Anims = GetStructures(context.Anims);
            DoubleMaterials = GetStructures(context.DoubleMaterials);

            IndicesChunks01 = GetStructures(context.IndicesChunks01);
            IndicesChunks03 = GetStructures(context.IndicesChunks03);
            IndicesChunks05 = GetStructures(context.IndicesChunks05);
            IndicesChunks06 = GetStructures(context.IndicesChunks06);

            Mappings = GetStructures(context.Mappings);
            MappingChildren = GetStructures(context.MappingChildren);
            MappingSubs = GetStructures(context.MappingSubs);
            Materials = GetStructures(context.Materials);
            MaterialProperties = GetStructures(context.MaterialProperties);
            MaterialTextures = GetStructures(context.MaterialTextures);
            MaterialTextureChilds = GetStructures(context.MaterialTextureChilds);
            Meshes = GetStructures(context.Meshes);
            Vertices = GetStructures(context.Vertices);

            Nodes3064 = GetStructures(context.Nodes3064);
            Nodes5064 = GetStructures(context.Nodes5064);
            Nodes5065 = GetStructures(context.Nodes5065);
            Nodes5066 = GetStructures(context.Nodes5066);
            NodesD064 = GetStructures(context.NodesD064);
            NodesD065 = GetStructures(context.NodesD065);
            NodesD066 = GetStructures(context.NodesD066);

            Models = GetStructures(context.Headers);
            HeaderNodes = GetStructures(context.HeaderNodes);
            HeaderAltN = GetStructures(context.HeaderAltN);
            Data_LStr = GetStructures(context.Data_LStr);
            Data_Int = GetStructures(context.Data_Int);
        }

        public override void Load(ByteSerializerGraph g)
        {
            Anims = GetStructures<Animation, DbAnimation>(g);
            DoubleMaterials = GetStructures<MaterialReference, DbMaterialReference>(g);

            IndicesChunks01 = GetStructures<IndicesChunk01, DbIndicesChunk01>(g);
            IndicesChunks03 = GetStructures<IndicesChunk03, DbIndicesChunk03>(g);
            IndicesChunks05 = GetStructures<IndicesChunk05, DbIndicesChunk05>(g);
            IndicesChunks06 = GetStructures<IndicesChunk06, DbIndicesChunk06>(g);

            Mappings = GetStructures<Mapping, DbMapping>(g);
            MappingChildren = GetStructures<MappingChild, DbMappingChild>(g);
            MappingSubs = GetStructures<MappingSub, DbMappingSub>(g);
            Materials = GetStructures<Material, DbMaterial>(g);
            MaterialProperties = GetStructures<MaterialProperties, DbMaterialProperties>(g);
            MaterialTextures = GetStructures<MaterialTexture, DbMaterialTexture>(g);
            MaterialTextureChilds = GetStructures<MaterialTextureChild, DbMaterialTextureChild>(g);
            Meshes = GetStructures<Mesh, DbMesh>(g);
            Vertices = GetStructures<Vertex, DbVertex>(g);

            Nodes3064 = GetStructures<MeshGroup3064, DbNode3064>(g);
            Nodes5064 = GetStructures<Group5064, DbNode5064>(g);
            Nodes5065 = GetStructures<Group5065, DbNode5065>(g);
            Nodes5066 = GetStructures<Group5066, DbNode5066>(g);
            NodesD064 = GetStructures<TransformableD064, DbNodeD064>(g);
            NodesD065 = GetStructures<TransformableD065, DbNodeD065>(g);
            NodesD066 = GetStructures<UnknownD066, DbNodeD066>(g);

            Models = GetStructures<Model, DbModelHeader>(g);
            HeaderNodes = GetStructures<FlaggedNodeOrInteger, DbModelHeaderNode>(g);
            HeaderAltN = GetStructures<FlaggedNodeOrGroup5066ChildReference, DbModelHeaderAltN>(g);
            Data_LStr = GetStructures<LightStreakOrInteger, DbDataLStr>(g, x => x.LightStreak != null);
            Data_Int = GetStructures<LightStreakOrInteger, DbDataInt>(g, x => x.Integer.HasValue);
        }

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
