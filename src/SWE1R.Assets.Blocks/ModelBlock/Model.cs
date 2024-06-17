// SPDX-License-Identifier: MIT

using ByteSerialization.Attributes;
using ByteSerialization.Nodes;
using SWE1R.Assets.Blocks.ModelBlock.Animations;
using SWE1R.Assets.Blocks.ModelBlock.Materials;
using SWE1R.Assets.Blocks.ModelBlock.Meshes;
using SWE1R.Assets.Blocks.ModelBlock.Nodes;
using SWE1R.Assets.Blocks.ModelBlock.Types;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace SWE1R.Assets.Blocks.ModelBlock
{
    public abstract class Model : BlockItemValue
    {
        #region Types

        private enum Indicator : int
        {
            Data = ('D' << 24) + ('a' << 16) + ('t' << 8) + ('a' << 0),
            Anim = ('A' << 24) + ('n' << 16) + ('i' << 8) + ('m' << 0),
            AltN = ('A' << 24) + ('l' << 16) + ('t' << 8) + ('N' << 0),
            HEnd = ('H' << 24) + ('E' << 16) + ('n' << 8) + ('d' << 0),
        }

        #endregion

        #region Properties (serialized)

        [RecordTypeIdentifier(ModelType.MAlt, typeof(MAltModel))]
        [RecordTypeIdentifier(ModelType.Modl, typeof(ModlModel))]
        [RecordTypeIdentifier(ModelType.Part, typeof(PartModel))]
        [RecordTypeIdentifier(ModelType.Podd, typeof(PoddModel))]
        [RecordTypeIdentifier(ModelType.Pupp, typeof(PuppModel))]
        [RecordTypeIdentifier(ModelType.Scen, typeof(ScenModel))]
        [RecordTypeIdentifier(ModelType.Trak, typeof(TrakModel))]
        [Order(0)]
        public ModelType Type { get; protected set; }
        
        [Order(1), SerializeUntil(-1)]
        public List<FlaggedNodeOrInteger> Nodes { get; set; }
        
        [Order(2), Indicator(Indicator.Data)]
        public HeaderData Data { get; set; }
        
        [Order(3), Indicator(Indicator.Anim), SerializeUntilNullPointer, ElementReference(ReferenceHandling.LowPriority)]
        public List<Animation> Animations { get; set; }

        [Order(4), Indicator(Indicator.AltN), SerializeUntilNullPointer]
        public List<FlaggedNodeOrLodSelectorNodeChildReference> AltN { get; set; }

        [Order(5)]
        private Indicator HEnd { get; set; } = Indicator.HEnd;

        #endregion

        #region Methods (serialization)

        public abstract bool HasExtraAlignment(FlaggedNode flaggedNode, ByteSerializerGraph graph);
        public abstract bool HasExtraAlignment(Animation anim, ByteSerializerGraph graph);

        #endregion

        #region Methods (helper - INode)

        public IEnumerable<FlaggedNode> GetHeaderFlaggedNodes() =>
            Nodes
            .Select(x => x.FlaggedNode)
            .Where(x => x != null)
            .Distinct()
            .ToList() 
            ?? Enumerable.Empty<FlaggedNode>();

        public IEnumerable<FlaggedNode> GetAltNFlaggedNodes() =>
            AltN?
            .Select(x => x.FlaggedNode)
            .Where(x => x != null)
            .Distinct()
            .ToList()
            ?? Enumerable.Empty<FlaggedNode>();

        public IEnumerable<TransformedWithPivotNode> GetAnimationsTransformedWithPivotNodes() =>
            Animations?
            .Select(x => x.TargetOrInteger.Target?.TransformedWithPivotNode)
            .Where(x => x != null)
            .Distinct()
            .ToList() 
            ?? Enumerable.Empty<TransformedWithPivotNode>();

        public ReadOnlyCollection<INode> GetAllNodes()
        {
            var rootNodes = new List<INode>();
            rootNodes.AddRange(GetHeaderFlaggedNodes());
            //rootNodes.AddRange(GetAnimationsTransformedWithPivotNodes()); // is contained in GetHeaderFlaggedNodes anyways
            rootNodes.AddRange(GetAltNFlaggedNodes());
            List<INode> allNodes = rootNodes.SelectMany(x => x.GetSelfAndDescendants()).ToList();
            return allNodes.AsReadOnly();
        }

        #endregion

        #region Methods (helper - Material/MaterialTexture)

        public ReadOnlyCollection<MeshMaterial> GetMeshMaterials() =>
            GetAllNodes().OfType<Mesh>().Select(x => x.MeshMaterial).Distinct().ToList().AsReadOnly();

        public ReadOnlyCollection<MaterialTexture> GetMaterialTextures()
        {
            var materialTextures = new List<MaterialTexture>();

            // Model.Nodes
            materialTextures.AddRange(
                GetMeshMaterials().Select(x => x.MaterialTexture)
                .Where(x => x != null));

            // Model.Animations
            if (Animations != null)
            {
                foreach (var animation in Animations)
                {
                    List<MaterialTexture> animationMaterialTextures = 
                        animation.KeyframesOrInteger.Keyframes?.MaterialTextures;
                    if (animationMaterialTextures != null)
                        materialTextures.AddRange(animationMaterialTextures);
                }
            }
            
            return materialTextures.AsReadOnly();
        }

        #endregion
    }
}
