// SPDX-License-Identifier: MIT

using ByteSerialization.Nodes;
using SWE1R.Assets.Blocks.ModelBlock;
using SWE1R.Assets.Blocks.ModelBlock.Animations;
using SWE1R.Assets.Blocks.ModelBlock.Materials;
using SWE1R.Assets.Blocks.ModelBlock.Meshes;
using SWE1R.Assets.Blocks.ModelBlock.Meshes.N64GspCommands;
using SWE1R.Assets.Blocks.ModelBlock.Nodes;
using SWE1R.Assets.Blocks.Original.SQLite.Entities.ModelBlock.Anims;
using SWE1R.Assets.Blocks.Original.SQLite.Entities.ModelBlock.Meshes;
using SWE1R.Assets.Blocks.Original.SQLite.Entities.ModelBlock.Meshes.N64GspCommands;
using SWE1R.Assets.Blocks.Original.SQLite.Entities.ModelBlock.Nodes;

namespace SWE1R.Assets.Blocks.Original.SQLite.Entities.ModelBlock
{
    public class DbModelStructures : DbBlockItemStructures, IEquatable<DbModelStructures>
    {
        #region Properties (entities)

        public List<DbAnimation> Anims { get; set; }
        public List<DbMaterialReference> DoubleMaterials { get; set; }

        public List<DbN64GspVertexCommand> N64GspVertexCommands { get; set; }
        public List<DbN64GspCullDisplayListCommand> N64GspCullDisplayListCommands { get; set; }
        public List<DbN64Gsp1TriangleCommand> N64Gsp1TriangleCommands { get; set; }
        public List<DbN64Gsp2TrianglesCommand> N64Gsp2TrianglesCommands { get; set; }

        public List<DbMapping> Mappings { get; set; }
        public List<DbMappingChild> MappingChildren { get; set; }
        public List<DbMappingSub> MappingSubs { get; set; }
        public List<DbMaterial> Materials { get; set; }
        public List<DbMaterialProperties> MaterialProperties { get; set; }
        public List<DbMaterialTexture> MaterialTextures { get; set; }
        public List<DbMaterialTextureChild> MaterialTextureChilds { get; set; }
        public List<DbMesh> Meshes { get; set; }
        public List<DbVertex> Vertices { get; set; }

        public List<DbMeshGroupNode> Nodes_MeshGroup { get; set; }
        public List<DbBasicNode> Nodes_Basic { get; set; }
        public List<DbSelectorNode> Nodes_Selector { get; set; }
        public List<DbLodSelectorNode> Nodes_LodSelector { get; set; }
        public List<DbTransformedNode> Nodes_Transformed { get; set; }
        public List<DbTransformedWithPivotNode> Nodes_TransformedWithPivot { get; set; }
        public List<DbTransformedComputedNode> Nodes_TransformedComputed { get; set; }

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

            N64GspVertexCommands = GetStructures(context.N64GspVertexCommands);
            N64GspCullDisplayListCommands = GetStructures(context.N64GspCullDisplayListCommands);
            N64Gsp1TriangleCommands = GetStructures(context.N64Gsp1TriangleCommands);
            N64Gsp2TrianglesCommands = GetStructures(context.N64Gsp2TrianglesCommands);

            Mappings = GetStructures(context.Mappings);
            MappingChildren = GetStructures(context.MappingChildren);
            MappingSubs = GetStructures(context.MappingSubs);
            Materials = GetStructures(context.Materials);
            MaterialProperties = GetStructures(context.MaterialProperties);
            MaterialTextures = GetStructures(context.MaterialTextures);
            MaterialTextureChilds = GetStructures(context.MaterialTextureChilds);
            Meshes = GetStructures(context.Meshes);
            Vertices = GetStructures(context.Vertices);

            Nodes_MeshGroup = GetStructures(context.Nodes_MeshGroup);
            Nodes_Basic = GetStructures(context.Nodes_Basic);
            Nodes_Selector = GetStructures(context.Nodes_Selector);
            Nodes_LodSelector = GetStructures(context.Nodes_LodSelector);
            Nodes_Transformed = GetStructures(context.Nodes_Transformed);
            Nodes_TransformedWithPivot = GetStructures(context.Nodes_TransformedWithPivot);
            Nodes_TransformedComputed = GetStructures(context.Nodes_TransformedComputed);

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

            N64GspVertexCommands = GetStructures<N64GspVertexCommand, DbN64GspVertexCommand>(g);
            N64GspCullDisplayListCommands = GetStructures<N64GspCullDisplayListCommand, DbN64GspCullDisplayListCommand>(g);
            N64Gsp1TriangleCommands = GetStructures<N64Gsp1TriangleCommand, DbN64Gsp1TriangleCommand>(g);
            N64Gsp2TrianglesCommands = GetStructures<N64Gsp2TrianglesCommand, DbN64Gsp2TrianglesCommand>(g);

            Mappings = GetStructures<Mapping, DbMapping>(g);
            MappingChildren = GetStructures<MappingChild, DbMappingChild>(g);
            MappingSubs = GetStructures<MappingSub, DbMappingSub>(g);
            Materials = GetStructures<Material, DbMaterial>(g);
            MaterialProperties = GetStructures<MaterialProperties, DbMaterialProperties>(g);
            MaterialTextures = GetStructures<MaterialTexture, DbMaterialTexture>(g);
            MaterialTextureChilds = GetStructures<MaterialTextureChild, DbMaterialTextureChild>(g);
            Meshes = GetStructures<Mesh, DbMesh>(g);
            Vertices = GetStructures<Vertex, DbVertex>(g);

            Nodes_MeshGroup = GetStructures<MeshGroupNode, DbMeshGroupNode>(g);
            Nodes_Basic = GetStructures<BasicNode, DbBasicNode>(g);
            Nodes_Selector = GetStructures<SelectorNode, DbSelectorNode>(g);
            Nodes_LodSelector = GetStructures<LodSelectorNode, DbLodSelectorNode>(g);
            Nodes_Transformed = GetStructures<TransformedNode, DbTransformedNode>(g);
            Nodes_TransformedWithPivot = GetStructures<TransformedWithPivotNode, DbTransformedWithPivotNode>(g);
            Nodes_TransformedComputed = GetStructures<TransformedComputedNode, DbTransformedComputedNode>(g);

            Models = GetStructures<Model, DbModelHeader>(g);
            HeaderNodes = GetStructures<FlaggedNodeOrInteger, DbModelHeaderNode>(g);
            HeaderAltN = GetStructures<FlaggedNodeOrLodSelectorNodeChildReference, DbModelHeaderAltN>(g);
            Data_LStr = GetStructures<LightStreakOrInteger, DbDataLStr>(g, x => x.LightStreak != null);
            Data_Int = GetStructures<LightStreakOrInteger, DbDataInt>(g, x => x.Integer.HasValue);
        }

        public bool Equals(DbModelStructures other)
        {
            if (!Anims.SequenceEqual(other.Anims)) return false;
            if (!DoubleMaterials.SequenceEqual(other.DoubleMaterials)) return false;

            if (!N64GspVertexCommands.SequenceEqual(other.N64GspVertexCommands)) return false;
            if (!N64GspCullDisplayListCommands.SequenceEqual(other.N64GspCullDisplayListCommands)) return false;
            if (!N64Gsp1TriangleCommands.SequenceEqual(other.N64Gsp1TriangleCommands)) return false;
            if (!N64Gsp2TrianglesCommands.SequenceEqual(other.N64Gsp2TrianglesCommands)) return false;

            if (!Mappings.SequenceEqual(other.Mappings)) return false;
            if (!MappingChildren.SequenceEqual(other.MappingChildren)) return false;
            if (!MappingSubs.SequenceEqual(other.MappingSubs)) return false;
            if (!Materials.SequenceEqual(other.Materials)) return false;
            if (!MaterialProperties.SequenceEqual(other.MaterialProperties)) return false;
            if (!MaterialTextures.SequenceEqual(other.MaterialTextures)) return false;
            if (!MaterialTextureChilds.SequenceEqual(other.MaterialTextureChilds)) return false;
            if (!Meshes.SequenceEqual(other.Meshes)) return false;
            if (!Vertices.SequenceEqual(other.Vertices)) return false;

            if (!Nodes_MeshGroup.SequenceEqual(other.Nodes_MeshGroup)) return false;
            if (!Nodes_Basic.SequenceEqual(other.Nodes_Basic)) return false;
            if (!Nodes_Selector.SequenceEqual(other.Nodes_Selector)) return false;
            if (!Nodes_LodSelector.SequenceEqual(other.Nodes_LodSelector)) return false;
            if (!Nodes_Transformed.SequenceEqual(other.Nodes_Transformed)) return false;
            if (!Nodes_TransformedWithPivot.SequenceEqual(other.Nodes_TransformedWithPivot)) return false;
            if (!Nodes_TransformedComputed.SequenceEqual(other.Nodes_TransformedComputed)) return false;

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
