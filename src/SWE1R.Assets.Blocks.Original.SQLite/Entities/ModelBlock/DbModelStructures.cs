// SPDX-License-Identifier: MIT

using ByteSerialization.Nodes;
using SWE1R.Assets.Blocks.ModelBlock;
using SWE1R.Assets.Blocks.ModelBlock.Animations;
using SWE1R.Assets.Blocks.ModelBlock.Materials;
using SWE1R.Assets.Blocks.ModelBlock.Meshes;
using SWE1R.Assets.Blocks.ModelBlock.N64Sdk;
using SWE1R.Assets.Blocks.ModelBlock.N64Sdk.GraphicsCommands;
using SWE1R.Assets.Blocks.ModelBlock.Nodes;
using SWE1R.Assets.Blocks.Original.SQLite.Entities.ModelBlock.Anims;
using SWE1R.Assets.Blocks.Original.SQLite.Entities.ModelBlock.Meshes;
using SWE1R.Assets.Blocks.Original.SQLite.Entities.ModelBlock.N64Sdk;
using SWE1R.Assets.Blocks.Original.SQLite.Entities.ModelBlock.N64Sdk.GraphicsCommands;
using SWE1R.Assets.Blocks.Original.SQLite.Entities.ModelBlock.Nodes;

namespace SWE1R.Assets.Blocks.Original.SQLite.Entities.ModelBlock
{
    public class DbModelStructures : DbBlockItemStructures, IEquatable<DbModelStructures>
    {
        #region Properties (entities)

        public List<DbModel> Models { get; set; }
        public List<DbFlaggedNodeOrInteger> FlaggedNodeOrIntegers { get; set; }
        public List<DbFlaggedNodeOrLodSelectorNodeChildReference> FlaggedNodeOrLodSelectorNodeChildReferences { get; set; }
        public List<DbDataLightStreak> Data_LightStreaks { get; set; }
        public List<DbDataInteger> Data_Integers { get; set; }

        public List<DbAnimation> Animations { get; set; }
        public List<DbMeshMaterialReference> MeshMaterialReferences { get; set; }

        public List<DbVtx> N64Sdk_Vtxs { get; set; }
        public List<DbGSpVertexCommand> N64Sdk_GraphicsCommands_GSpVertexCommands { get; set; }
        public List<DbGSpCullDisplayListCommand> N64Sdk_GraphicsCommands_GSpCullDisplayListCommands { get; set; }
        public List<DbGSp1TriangleCommand> N64Sdk_GraphicsCommands_GSp1TriangleCommands { get; set; }
        public List<DbGSp2TrianglesCommand> N64Sdk_GraphicsCommands_GSp2TrianglesCommands { get; set; }

        public List<DbMapping> Mappings { get; set; }
        public List<DbMappingChild> MappingChildren { get; set; }
        public List<DbMappingSub> MappingSubs { get; set; }
        public List<DbMeshMaterial> MeshMaterials { get; set; }
        public List<DbMaterial> Materials { get; set; }
        public List<DbMaterialTexture> MaterialTextures { get; set; }
        public List<DbMaterialTextureChild> MaterialTextureChildren { get; set; }
        public List<DbMesh> Meshes { get; set; }
        
        public List<DbMeshGroupNode> Nodes_MeshGroupNodes { get; set; }
        public List<DbBasicNode> Nodes_BasicNodes { get; set; }
        public List<DbSelectorNode> Nodes_SelectorNodes { get; set; }
        public List<DbLodSelectorNode> Nodes_LodSelectorNodes { get; set; }
        public List<DbTransformedNode> Nodes_TransformedNodes { get; set; }
        public List<DbTransformedWithPivotNode> Nodes_TransformedWithPivotNodes { get; set; }
        public List<DbTransformedComputedNode> Nodes_TransformedComputedNodes { get; set; }

        #endregion

        #region Constructor

        public DbModelStructures(int blockItemIndex) : 
            base(blockItemIndex)
        { }

        #endregion

        #region Methods

        public override void Load(AssetsDbContext context)
        {
            Models = GetStructures(context.Models);
            FlaggedNodeOrIntegers = GetStructures(context.FlaggedNodeOrIntegers);
            FlaggedNodeOrLodSelectorNodeChildReferences = GetStructures(context.FlaggedNodeOrLodSelectorNodeChildReferences);
            Data_LightStreaks = GetStructures(context.Data_LightStreaks);
            Data_Integers = GetStructures(context.Data_Integers);

            Animations = GetStructures(context.Animations);
            MeshMaterialReferences = GetStructures(context.MeshMaterialReferences);

            N64Sdk_Vtxs = GetStructures(context.N64Sdk_Vtxs);
            N64Sdk_GraphicsCommands_GSpVertexCommands = GetStructures(context.N64Sdk_GSpVertexCommands);
            N64Sdk_GraphicsCommands_GSpCullDisplayListCommands = GetStructures(context.N64Sdk_GSpCullDisplayListCommands);
            N64Sdk_GraphicsCommands_GSp1TriangleCommands = GetStructures(context.N64Sdk_GSp1TriangleCommands);
            N64Sdk_GraphicsCommands_GSp2TrianglesCommands = GetStructures(context.N64Sdk_GSp2TrianglesCommands);

            Mappings = GetStructures(context.Mappings);
            MappingChildren = GetStructures(context.MappingChildren);
            MappingSubs = GetStructures(context.MappingSubs);
            MeshMaterials = GetStructures(context.MeshMaterials);
            Materials = GetStructures(context.Materials);
            MaterialTextures = GetStructures(context.MaterialTextures);
            MaterialTextureChildren = GetStructures(context.MaterialTextureChildren);
            Meshes = GetStructures(context.Meshes);

            Nodes_MeshGroupNodes = GetStructures(context.Nodes_MeshGroupNodes);
            Nodes_BasicNodes = GetStructures(context.Nodes_BasicNodes);
            Nodes_SelectorNodes = GetStructures(context.Nodes_SelectorNodes);
            Nodes_LodSelectorNodes = GetStructures(context.Nodes_LodSelectorNodes);
            Nodes_TransformedNodes = GetStructures(context.Nodes_TransformedNodes);
            Nodes_TransformedWithPivotNodes = GetStructures(context.Nodes_TransformedWithPivotNodes);
            Nodes_TransformedComputedNodes = GetStructures(context.Nodes_TransformedComputedNodes);
        }

        public override void Load(ByteSerializerGraph g)
        {
            Models = GetStructures<Model, DbModel>(g);
            FlaggedNodeOrIntegers = GetStructures<FlaggedNodeOrInteger, DbFlaggedNodeOrInteger>(g);
            FlaggedNodeOrLodSelectorNodeChildReferences = GetStructures<FlaggedNodeOrLodSelectorNodeChildReference, DbFlaggedNodeOrLodSelectorNodeChildReference>(g);
            Data_LightStreaks = GetStructures<LightStreakOrInteger, DbDataLightStreak>(g, x => x.LightStreak != null);
            Data_Integers = GetStructures<LightStreakOrInteger, DbDataInteger>(g, x => x.Integer.HasValue);

            Animations = GetStructures<Animation, DbAnimation>(g);
            MeshMaterialReferences = GetStructures<MeshMaterialReference, DbMeshMaterialReference>(g);

            N64Sdk_Vtxs = GetStructures<Vtx, DbVtx>(g);
            N64Sdk_GraphicsCommands_GSpVertexCommands = GetStructures<GSpVertexCommand, DbGSpVertexCommand>(g);
            N64Sdk_GraphicsCommands_GSpCullDisplayListCommands = GetStructures<GSpCullDisplayListCommand, DbGSpCullDisplayListCommand>(g);
            N64Sdk_GraphicsCommands_GSp1TriangleCommands = GetStructures<GSp1TriangleCommand, DbGSp1TriangleCommand>(g);
            N64Sdk_GraphicsCommands_GSp2TrianglesCommands = GetStructures<GSp2TrianglesCommand, DbGSp2TrianglesCommand>(g);

            Mappings = GetStructures<Mapping, DbMapping>(g);
            MappingChildren = GetStructures<MappingChild, DbMappingChild>(g);
            MappingSubs = GetStructures<MappingSub, DbMappingSub>(g);
            MeshMaterials = GetStructures<MeshMaterial, DbMeshMaterial>(g);
            Materials = GetStructures<Material, DbMaterial>(g);
            MaterialTextures = GetStructures<MaterialTexture, DbMaterialTexture>(g);
            MaterialTextureChildren = GetStructures<MaterialTextureChild, DbMaterialTextureChild>(g);
            Meshes = GetStructures<Mesh, DbMesh>(g);
            
            Nodes_MeshGroupNodes = GetStructures<MeshGroupNode, DbMeshGroupNode>(g);
            Nodes_BasicNodes = GetStructures<BasicNode, DbBasicNode>(g);
            Nodes_SelectorNodes = GetStructures<SelectorNode, DbSelectorNode>(g);
            Nodes_LodSelectorNodes = GetStructures<LodSelectorNode, DbLodSelectorNode>(g);
            Nodes_TransformedNodes = GetStructures<TransformedNode, DbTransformedNode>(g);
            Nodes_TransformedWithPivotNodes = GetStructures<TransformedWithPivotNode, DbTransformedWithPivotNode>(g);
            Nodes_TransformedComputedNodes = GetStructures<TransformedComputedNode, DbTransformedComputedNode>(g);
        }

        public bool Equals(DbModelStructures other)
        {
            if (!Models.SequenceEqual(other.Models)) return false;
            if (!FlaggedNodeOrIntegers.SequenceEqual(other.FlaggedNodeOrIntegers)) return false;
            if (!FlaggedNodeOrLodSelectorNodeChildReferences.SequenceEqual(other.FlaggedNodeOrLodSelectorNodeChildReferences)) return false;
            if (!Data_LightStreaks.SequenceEqual(other.Data_LightStreaks)) return false;
            if (!Data_Integers.SequenceEqual(other.Data_Integers)) return false;

            if (!Animations.SequenceEqual(other.Animations)) return false;
            if (!MeshMaterialReferences.SequenceEqual(other.MeshMaterialReferences)) return false;

            if (!N64Sdk_Vtxs.SequenceEqual(other.N64Sdk_Vtxs)) return false;
            if (!N64Sdk_GraphicsCommands_GSpVertexCommands.SequenceEqual(other.N64Sdk_GraphicsCommands_GSpVertexCommands)) return false;
            if (!N64Sdk_GraphicsCommands_GSpCullDisplayListCommands.SequenceEqual(other.N64Sdk_GraphicsCommands_GSpCullDisplayListCommands)) return false;
            if (!N64Sdk_GraphicsCommands_GSp1TriangleCommands.SequenceEqual(other.N64Sdk_GraphicsCommands_GSp1TriangleCommands)) return false;
            if (!N64Sdk_GraphicsCommands_GSp2TrianglesCommands.SequenceEqual(other.N64Sdk_GraphicsCommands_GSp2TrianglesCommands)) return false;

            if (!Mappings.SequenceEqual(other.Mappings)) return false;
            if (!MappingChildren.SequenceEqual(other.MappingChildren)) return false;
            if (!MappingSubs.SequenceEqual(other.MappingSubs)) return false;
            if (!MeshMaterials.SequenceEqual(other.MeshMaterials)) return false;
            if (!Materials.SequenceEqual(other.Materials)) return false;
            if (!MaterialTextures.SequenceEqual(other.MaterialTextures)) return false;
            if (!MaterialTextureChildren.SequenceEqual(other.MaterialTextureChildren)) return false;
            if (!Meshes.SequenceEqual(other.Meshes)) return false;

            if (!Nodes_MeshGroupNodes.SequenceEqual(other.Nodes_MeshGroupNodes)) return false;
            if (!Nodes_BasicNodes.SequenceEqual(other.Nodes_BasicNodes)) return false;
            if (!Nodes_SelectorNodes.SequenceEqual(other.Nodes_SelectorNodes)) return false;
            if (!Nodes_LodSelectorNodes.SequenceEqual(other.Nodes_LodSelectorNodes)) return false;
            if (!Nodes_TransformedNodes.SequenceEqual(other.Nodes_TransformedNodes)) return false;
            if (!Nodes_TransformedWithPivotNodes.SequenceEqual(other.Nodes_TransformedWithPivotNodes)) return false;
            if (!Nodes_TransformedComputedNodes.SequenceEqual(other.Nodes_TransformedComputedNodes)) return false;

            return true;
        }

        #endregion
    }
}
