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

        public List<DbAnimation> Animations { get; set; }
        public List<DbMeshMaterialReference> MeshMaterialReferences { get; set; }

        public List<DbN64GspVertexCommand> N64GspVertexCommands { get; set; }
        public List<DbN64GspCullDisplayListCommand> N64GspCullDisplayListCommands { get; set; }
        public List<DbN64Gsp1TriangleCommand> N64Gsp1TriangleCommands { get; set; }
        public List<DbN64Gsp2TrianglesCommand> N64Gsp2TrianglesCommands { get; set; }

        public List<DbMapping> Mappings { get; set; }
        public List<DbMappingChild> MappingChildren { get; set; }
        public List<DbMappingSub> MappingSubs { get; set; }
        public List<DbMeshMaterial> MeshMaterials { get; set; }
        public List<DbMaterial> Materials { get; set; }
        public List<DbMaterialTexture> MaterialTextures { get; set; }
        public List<DbMaterialTextureChild> MaterialTextureChildren { get; set; }
        public List<DbMesh> Meshes { get; set; }
        public List<DbVertex> Vertices { get; set; }

        public List<DbMeshGroupNode> Nodes_MeshGroupNodes { get; set; }
        public List<DbBasicNode> Nodes_BasicNodes { get; set; }
        public List<DbSelectorNode> Nodes_SelectorNodes { get; set; }
        public List<DbLodSelectorNode> Nodes_LodSelectorNodes { get; set; }
        public List<DbTransformedNode> Nodes_TransformedNodes { get; set; }
        public List<DbTransformedWithPivotNode> Nodes_TransformedWithPivotNodes { get; set; }
        public List<DbTransformedComputedNode> Nodes_TransformedComputedNodes { get; set; }

        public List<DbModel> Models { get; set; }
        public List<DbFlaggedNodeOrInteger> FlaggedNodeOrIntegers { get; set; }
        public List<DbFlaggedNodeOrLodSelectorNodeChildReference> FlaggedNodeOrLodSelectorNodeChildReferences { get; set; }
        public List<DbDataLightStreak> Data_LightStreaks { get; set; }
        public List<DbDataInteger> Data_Integers { get; set; }

        #endregion

        #region Constructor

        public DbModelStructures(int blockItemIndex) : 
            base(blockItemIndex)
        { }

        #endregion

        #region Methods

        public override void Load(AssetsDbContext context)
        {
            Animations = GetStructures(context.Animations);
            MeshMaterialReferences = GetStructures(context.MeshMaterialReferences);

            N64GspVertexCommands = GetStructures(context.N64GspVertexCommands);
            N64GspCullDisplayListCommands = GetStructures(context.N64GspCullDisplayListCommands);
            N64Gsp1TriangleCommands = GetStructures(context.N64Gsp1TriangleCommands);
            N64Gsp2TrianglesCommands = GetStructures(context.N64Gsp2TrianglesCommands);

            Mappings = GetStructures(context.Mappings);
            MappingChildren = GetStructures(context.MappingChildren);
            MappingSubs = GetStructures(context.MappingSubs);
            MeshMaterials = GetStructures(context.MeshMaterials);
            Materials = GetStructures(context.Materials);
            MaterialTextures = GetStructures(context.MaterialTextures);
            MaterialTextureChildren = GetStructures(context.MaterialTextureChildren);
            Meshes = GetStructures(context.Meshes);
            Vertices = GetStructures(context.Vertices);

            Nodes_MeshGroupNodes = GetStructures(context.Nodes_MeshGroupNodes);
            Nodes_BasicNodes = GetStructures(context.Nodes_BasicNodes);
            Nodes_SelectorNodes = GetStructures(context.Nodes_SelectorNodes);
            Nodes_LodSelectorNodes = GetStructures(context.Nodes_LodSelectorNodes);
            Nodes_TransformedNodes = GetStructures(context.Nodes_TransformedNodes);
            Nodes_TransformedWithPivotNodes = GetStructures(context.Nodes_TransformedWithPivotNodes);
            Nodes_TransformedComputedNodes = GetStructures(context.Nodes_TransformedComputedNodes);

            Models = GetStructures(context.Models);
            FlaggedNodeOrIntegers = GetStructures(context.HeaderNodes);
            FlaggedNodeOrLodSelectorNodeChildReferences = GetStructures(context.HeaderAltN);
            Data_LightStreaks = GetStructures(context.Data_LStr);
            Data_Integers = GetStructures(context.Data_Int);
        }

        public override void Load(ByteSerializerGraph g)
        {
            Animations = GetStructures<Animation, DbAnimation>(g);
            MeshMaterialReferences = GetStructures<MeshMaterialReference, DbMeshMaterialReference>(g);

            N64GspVertexCommands = GetStructures<N64GspVertexCommand, DbN64GspVertexCommand>(g);
            N64GspCullDisplayListCommands = GetStructures<N64GspCullDisplayListCommand, DbN64GspCullDisplayListCommand>(g);
            N64Gsp1TriangleCommands = GetStructures<N64Gsp1TriangleCommand, DbN64Gsp1TriangleCommand>(g);
            N64Gsp2TrianglesCommands = GetStructures<N64Gsp2TrianglesCommand, DbN64Gsp2TrianglesCommand>(g);

            Mappings = GetStructures<Mapping, DbMapping>(g);
            MappingChildren = GetStructures<MappingChild, DbMappingChild>(g);
            MappingSubs = GetStructures<MappingSub, DbMappingSub>(g);
            MeshMaterials = GetStructures<MeshMaterial, DbMeshMaterial>(g);
            Materials = GetStructures<Material, DbMaterial>(g);
            MaterialTextures = GetStructures<MaterialTexture, DbMaterialTexture>(g);
            MaterialTextureChildren = GetStructures<MaterialTextureChild, DbMaterialTextureChild>(g);
            Meshes = GetStructures<Mesh, DbMesh>(g);
            Vertices = GetStructures<Vertex, DbVertex>(g);

            Nodes_MeshGroupNodes = GetStructures<MeshGroupNode, DbMeshGroupNode>(g);
            Nodes_BasicNodes = GetStructures<BasicNode, DbBasicNode>(g);
            Nodes_SelectorNodes = GetStructures<SelectorNode, DbSelectorNode>(g);
            Nodes_LodSelectorNodes = GetStructures<LodSelectorNode, DbLodSelectorNode>(g);
            Nodes_TransformedNodes = GetStructures<TransformedNode, DbTransformedNode>(g);
            Nodes_TransformedWithPivotNodes = GetStructures<TransformedWithPivotNode, DbTransformedWithPivotNode>(g);
            Nodes_TransformedComputedNodes = GetStructures<TransformedComputedNode, DbTransformedComputedNode>(g);

            Models = GetStructures<Model, DbModel>(g);
            FlaggedNodeOrIntegers = GetStructures<FlaggedNodeOrInteger, DbFlaggedNodeOrInteger>(g);
            FlaggedNodeOrLodSelectorNodeChildReferences = GetStructures<FlaggedNodeOrLodSelectorNodeChildReference, DbFlaggedNodeOrLodSelectorNodeChildReference>(g);
            Data_LightStreaks = GetStructures<LightStreakOrInteger, DbDataLightStreak>(g, x => x.LightStreak != null);
            Data_Integers = GetStructures<LightStreakOrInteger, DbDataInteger>(g, x => x.Integer.HasValue);
        }

        public bool Equals(DbModelStructures other)
        {
            if (!Animations.SequenceEqual(other.Animations)) return false;
            if (!MeshMaterialReferences.SequenceEqual(other.MeshMaterialReferences)) return false;

            if (!N64GspVertexCommands.SequenceEqual(other.N64GspVertexCommands)) return false;
            if (!N64GspCullDisplayListCommands.SequenceEqual(other.N64GspCullDisplayListCommands)) return false;
            if (!N64Gsp1TriangleCommands.SequenceEqual(other.N64Gsp1TriangleCommands)) return false;
            if (!N64Gsp2TrianglesCommands.SequenceEqual(other.N64Gsp2TrianglesCommands)) return false;

            if (!Mappings.SequenceEqual(other.Mappings)) return false;
            if (!MappingChildren.SequenceEqual(other.MappingChildren)) return false;
            if (!MappingSubs.SequenceEqual(other.MappingSubs)) return false;
            if (!MeshMaterials.SequenceEqual(other.MeshMaterials)) return false;
            if (!Materials.SequenceEqual(other.Materials)) return false;
            if (!MaterialTextures.SequenceEqual(other.MaterialTextures)) return false;
            if (!MaterialTextureChildren.SequenceEqual(other.MaterialTextureChildren)) return false;
            if (!Meshes.SequenceEqual(other.Meshes)) return false;
            if (!Vertices.SequenceEqual(other.Vertices)) return false;

            if (!Nodes_MeshGroupNodes.SequenceEqual(other.Nodes_MeshGroupNodes)) return false;
            if (!Nodes_BasicNodes.SequenceEqual(other.Nodes_BasicNodes)) return false;
            if (!Nodes_SelectorNodes.SequenceEqual(other.Nodes_SelectorNodes)) return false;
            if (!Nodes_LodSelectorNodes.SequenceEqual(other.Nodes_LodSelectorNodes)) return false;
            if (!Nodes_TransformedNodes.SequenceEqual(other.Nodes_TransformedNodes)) return false;
            if (!Nodes_TransformedWithPivotNodes.SequenceEqual(other.Nodes_TransformedWithPivotNodes)) return false;
            if (!Nodes_TransformedComputedNodes.SequenceEqual(other.Nodes_TransformedComputedNodes)) return false;

            if (!Models.SequenceEqual(other.Models)) return false;
            if (!FlaggedNodeOrIntegers.SequenceEqual(other.FlaggedNodeOrIntegers)) return false;
            if (!FlaggedNodeOrLodSelectorNodeChildReferences.SequenceEqual(other.FlaggedNodeOrLodSelectorNodeChildReferences)) return false;
            if (!Data_LightStreaks.SequenceEqual(other.Data_LightStreaks)) return false;
            if (!Data_Integers.SequenceEqual(other.Data_Integers)) return false;

            return true;
        }

        #endregion
    }
}
