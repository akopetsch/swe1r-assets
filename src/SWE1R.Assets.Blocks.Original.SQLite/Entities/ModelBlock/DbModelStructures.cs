// SPDX-License-Identifier: MIT

using ByteSerialization.Nodes;
using SWE1R.Assets.Blocks.ModelBlock;
using SWE1R.Assets.Blocks.ModelBlock.Animations;
using SWE1R.Assets.Blocks.ModelBlock.F3DEX2;
using SWE1R.Assets.Blocks.ModelBlock.Materials;
using SWE1R.Assets.Blocks.ModelBlock.Meshes;
using SWE1R.Assets.Blocks.ModelBlock.Nodes;
using SWE1R.Assets.Blocks.Original.SQLite.Entities.ModelBlock.Anims;
using SWE1R.Assets.Blocks.Original.SQLite.Entities.ModelBlock.Meshes;
using SWE1R.Assets.Blocks.Original.SQLite.Entities.ModelBlock.F3DEX2;
using SWE1R.Assets.Blocks.Original.SQLite.Entities.ModelBlock.Nodes;
using SWE1R.Assets.Blocks.ModelBlock.Behaviours;
using SWE1R.Assets.Blocks.Original.SQLite.Entities.ModelBlock.Behaviours;
using SWE1R.Assets.Blocks.Original.SQLite.Entities.ModelBlock.Materials;

namespace SWE1R.Assets.Blocks.Original.SQLite.Entities.ModelBlock
{
    public class DbModelStructures : DbBlockItemStructures, IEquatable<DbModelStructures>
    {
        #region Properties (entities)

        public List<DbModel> Models { get; set; }
        public List<DbFlaggedNodeOrInteger> FlaggedNodeOrIntegers { get; set; }
        public List<DbFlaggedNodeOrLodSelectorNodeChildReference> FlaggedNodeOrLodSelectorNodeChildReferences { get; set; }
        public List<DbDataInteger> Data_Integers { get; set; }
        public List<DbDataLightStreak> Data_LightStreaks { get; set; }

        // Animations
        public List<DbAnimation> Animations { get; set; }
        public List<DbMeshMaterialReference> MeshMaterialReferences { get; set; }

        // Behaviours
        public List<DbBehaviour> Behaviours { get; set; }
        public List<DbTriggerDescription> TriggerDescriptions { get; set; }
        public List<DbTriggerReference> TriggerReferences { get; set; }

        // F3DEX2
        public List<DbGsp1TriangleCommand> Gsp1TriangleCommands { get; set; }
        public List<DbGsp2TrianglesCommand> Gsp2TrianglesCommands { get; set; }
        public List<DbGspCullDisplayListCommand> GspCullDisplayListCommands { get; set; }
        public List<DbGspVertexCommand> GspVertexCommands { get; set; }
        public List<DbVtx> Vtxs { get; set; }

        // Materials
        public List<DbMaterial> Materials { get; set; }
        public List<DbMaterialTexture> MaterialTextures { get; set; }
        public List<DbMaterialTextureChild> MaterialTextureChildren { get; set; }
        public List<DbMeshMaterial> MeshMaterials { get; set; }

        // Meshes
        public List<DbMesh> Meshes { get; set; }
        
        // Nodes
        public List<DbBasicNode> Nodes_BasicNodes { get; set; }
        public List<DbLodSelectorNode> Nodes_LodSelectorNodes { get; set; }
        public List<DbMeshGroupNode> Nodes_MeshGroupNodes { get; set; }
        public List<DbSelectorNode> Nodes_SelectorNodes { get; set; }
        public List<DbTransformedComputedNode> Nodes_TransformedComputedNodes { get; set; }
        public List<DbTransformedNode> Nodes_TransformedNodes { get; set; }
        public List<DbTransformedWithPivotNode> Nodes_TransformedWithPivotNodes { get; set; }

        #endregion

        #region Methods

        public override void Load(AssetsDbContext context)
        {
            Models = GetStructures(context.Models);
            
            FlaggedNodeOrIntegers = GetStructures(context.FlaggedNodeOrIntegers);
            FlaggedNodeOrLodSelectorNodeChildReferences = GetStructures(context.FlaggedNodeOrLodSelectorNodeChildReferences);
            Data_Integers = GetStructures(context.Data_Integers);
            Data_LightStreaks = GetStructures(context.Data_LightStreaks);

            // Animations
            Animations = GetStructures(context.Animations);
            MeshMaterialReferences = GetStructures(context.MeshMaterialReferences);

            // Behaviours
            Behaviours = GetStructures(context.Behaviours);
            TriggerDescriptions = GetStructures(context.TriggerDescriptions);
            TriggerReferences = GetStructures(context.TriggerReferences);

            // F3DEX2
            Gsp1TriangleCommands = GetStructures(context.Gsp1TriangleCommands);
            Gsp2TrianglesCommands = GetStructures(context.Gsp2TrianglesCommands);
            GspCullDisplayListCommands = GetStructures(context.GspCullDisplayListCommands);
            GspVertexCommands = GetStructures(context.GspVertexCommands);
            Vtxs = GetStructures(context.Vtxs);

            // Materials
            Materials = GetStructures(context.Materials);
            MaterialTextures = GetStructures(context.MaterialTextures);
            MaterialTextureChildren = GetStructures(context.MaterialTextureChildren);
            MeshMaterials = GetStructures(context.MeshMaterials);

            // Meshes
            Meshes = GetStructures(context.Meshes);

            // Nodes
            Nodes_BasicNodes = GetStructures(context.Nodes_BasicNodes);
            Nodes_LodSelectorNodes = GetStructures(context.Nodes_LodSelectorNodes);
            Nodes_MeshGroupNodes = GetStructures(context.Nodes_MeshGroupNodes);
            Nodes_SelectorNodes = GetStructures(context.Nodes_SelectorNodes);
            Nodes_TransformedComputedNodes = GetStructures(context.Nodes_TransformedComputedNodes);
            Nodes_TransformedNodes = GetStructures(context.Nodes_TransformedNodes);
            Nodes_TransformedWithPivotNodes = GetStructures(context.Nodes_TransformedWithPivotNodes);
        }

        public override void Load(ByteSerializerGraph g)
        {
            Models = GetStructures<Model, DbModel>(g);
            FlaggedNodeOrIntegers = GetStructures<FlaggedNodeOrInteger, DbFlaggedNodeOrInteger>(g);
            FlaggedNodeOrLodSelectorNodeChildReferences = GetStructures<FlaggedNodeOrLodSelectorNodeChildReference, DbFlaggedNodeOrLodSelectorNodeChildReference>(g);
            Data_Integers = GetStructures<LightStreakOrInteger, DbDataInteger>(g, x => x.Integer.HasValue);
            Data_LightStreaks = GetStructures<LightStreakOrInteger, DbDataLightStreak>(g, x => x.LightStreak != null);

            // Animations
            Animations = GetStructures<Animation, DbAnimation>(g);
            MeshMaterialReferences = GetStructures<MeshMaterialReference, DbMeshMaterialReference>(g);

            // Behaviours
            Behaviours = GetStructures<Behaviour, DbBehaviour>(g);
            TriggerDescriptions = GetStructures<TriggerDescription, DbTriggerDescription>(g);
            TriggerReferences = GetStructures<TriggerReference, DbTriggerReference>(g);

            // F3DEX2
            Gsp1TriangleCommands = GetStructures<Gsp1TriangleCommand, DbGsp1TriangleCommand>(g);
            Gsp2TrianglesCommands = GetStructures<Gsp2TrianglesCommand, DbGsp2TrianglesCommand>(g);
            GspCullDisplayListCommands = GetStructures<GspCullDisplayListCommand, DbGspCullDisplayListCommand>(g);
            GspVertexCommands = GetStructures<GspVertexCommand, DbGspVertexCommand>(g);
            Vtxs = GetStructures<Vtx, DbVtx>(g);

            // Materials
            Materials = GetStructures<Material, DbMaterial>(g);
            MaterialTextures = GetStructures<MaterialTexture, DbMaterialTexture>(g);
            MaterialTextureChildren = GetStructures<MaterialTextureChild, DbMaterialTextureChild>(g);
            MeshMaterials = GetStructures<MeshMaterial, DbMeshMaterial>(g);

            // Meshes
            Meshes = GetStructures<Mesh, DbMesh>(g);
            
            // Nodes
            Nodes_BasicNodes = GetStructures<BasicNode, DbBasicNode>(g);
            Nodes_LodSelectorNodes = GetStructures<LodSelectorNode, DbLodSelectorNode>(g);
            Nodes_MeshGroupNodes = GetStructures<MeshGroupNode, DbMeshGroupNode>(g);
            Nodes_SelectorNodes = GetStructures<SelectorNode, DbSelectorNode>(g);
            Nodes_TransformedComputedNodes = GetStructures<TransformedComputedNode, DbTransformedComputedNode>(g);
            Nodes_TransformedNodes = GetStructures<TransformedNode, DbTransformedNode>(g);
            Nodes_TransformedWithPivotNodes = GetStructures<TransformedWithPivotNode, DbTransformedWithPivotNode>(g);
        }

        public bool Equals(DbModelStructures other)
        {
            if (!Models.SequenceEqual(other.Models)) return false;

            if (!FlaggedNodeOrIntegers.SequenceEqual(other.FlaggedNodeOrIntegers)) return false;
            if (!FlaggedNodeOrLodSelectorNodeChildReferences.SequenceEqual(other.FlaggedNodeOrLodSelectorNodeChildReferences)) return false;
            if (!Data_Integers.SequenceEqual(other.Data_Integers)) return false;
            if (!Data_LightStreaks.SequenceEqual(other.Data_LightStreaks)) return false;

            // Animations
            if (!Animations.SequenceEqual(other.Animations)) return false;
            if (!MeshMaterialReferences.SequenceEqual(other.MeshMaterialReferences)) return false;

            // Behaviours
            if (!Behaviours.SequenceEqual(other.Behaviours)) return false;
            if (!TriggerDescriptions.SequenceEqual(other.TriggerDescriptions)) return false;
            if (!TriggerReferences.SequenceEqual(other.TriggerReferences)) return false;

            // F3DEX2
            if (!Gsp1TriangleCommands.SequenceEqual(other.Gsp1TriangleCommands)) return false;
            if (!Gsp2TrianglesCommands.SequenceEqual(other.Gsp2TrianglesCommands)) return false;
            if (!GspCullDisplayListCommands.SequenceEqual(other.GspCullDisplayListCommands)) return false;
            if (!GspVertexCommands.SequenceEqual(other.GspVertexCommands)) return false;
            if (!Vtxs.SequenceEqual(other.Vtxs)) return false;

            // Materials
            if (!Materials.SequenceEqual(other.Materials)) return false;
            if (!MaterialTextures.SequenceEqual(other.MaterialTextures)) return false;
            if (!MaterialTextureChildren.SequenceEqual(other.MaterialTextureChildren)) return false;
            if (!MeshMaterials.SequenceEqual(other.MeshMaterials)) return false;

            // Meshes
            if (!Meshes.SequenceEqual(other.Meshes)) return false;

            // Nodes
            if (!Nodes_BasicNodes.SequenceEqual(other.Nodes_BasicNodes)) return false;
            if (!Nodes_LodSelectorNodes.SequenceEqual(other.Nodes_LodSelectorNodes)) return false;
            if (!Nodes_MeshGroupNodes.SequenceEqual(other.Nodes_MeshGroupNodes)) return false;
            if (!Nodes_SelectorNodes.SequenceEqual(other.Nodes_SelectorNodes)) return false;
            if (!Nodes_TransformedComputedNodes.SequenceEqual(other.Nodes_TransformedComputedNodes)) return false;
            if (!Nodes_TransformedNodes.SequenceEqual(other.Nodes_TransformedNodes)) return false;
            if (!Nodes_TransformedWithPivotNodes.SequenceEqual(other.Nodes_TransformedWithPivotNodes)) return false;

            return true;
        }

        #endregion
    }
}
