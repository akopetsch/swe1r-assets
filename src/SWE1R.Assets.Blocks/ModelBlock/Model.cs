// SPDX-License-Identifier: GPL-2.0-only

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
        #region Properties (serialized)

        [RecordTypeIdentifier(ModelType.MAlt, typeof(MAltModel))]
        [RecordTypeIdentifier(ModelType.Modl, typeof(ModlModel))]
        [RecordTypeIdentifier(ModelType.Part, typeof(PartModel))]
        [RecordTypeIdentifier(ModelType.Podd, typeof(PoddModel))]
        [RecordTypeIdentifier(ModelType.Pupp, typeof(PuppModel))]
        [RecordTypeIdentifier(ModelType.Scen, typeof(ScenModel))]
        [RecordTypeIdentifier(ModelType.Trak, typeof(TrakModel))]
        [Order(0)] public ModelType Type { get; protected set; }
        
        [SerializeUntil(-1)]
        [Order(1)] public List<FlaggedNodeOrInteger> Nodes { get; set; }

        [Indicator("Data")]
        [Order(2)] public HeaderData Data { get; set; }

        [Indicator("Anim")]
        [SerializeUntilNullPointer]
        [ElementReference(ReferenceHandling.LowPriority)]
        [Order(3)] public List<Animation> Animations { get; set; }
        
        [Indicator("AltN")]
        [SerializeUntilNullPointer]
        [Order(4)] public List<FlaggedNodeOrGroup5066ChildReference> AltN { get; set; }

        [Length(4)]
        [Order(5)] private char[] HEnd { get; set; } = "HEnd".ToCharArray();

        #endregion

        #region Methods (serialization)

        public abstract bool HasExtraAlignment(FlaggedNode flaggedNode, ByteSerializerGraph graph);
        public abstract bool HasExtraAlignment(Animation anim, ByteSerializerGraph graph);

        #endregion

        #region Methods (helper - INode)

        public IEnumerable<INode> GetHeaderFlaggedNodes() =>
            Nodes.Select(x => x.FlaggedNode)
            .Where(x => x != null).Distinct().Cast<INode>().ToList() ?? Enumerable.Empty<INode>();

        public IEnumerable<TransformableD065> GetAnimationsTransformableD065s() =>
            Animations?.Select(x => x.TargetOrInteger.Target?.TransformableD065)
            .Where(x => x != null).Distinct().ToList() ?? Enumerable.Empty<TransformableD065>();

        public IEnumerable<FlaggedNode> GetAltNFlaggedNodes() =>
            AltN?.Select(x => x.FlaggedNode)
            .Where(x => x != null).Distinct().ToList() ?? Enumerable.Empty<FlaggedNode>();

        public ReadOnlyCollection<INode> GetAllNodes()
        {
            var rootNodes = new List<INode>();
            rootNodes.AddRange(GetHeaderFlaggedNodes());
            //rootNodes.AddRange(GetAnimationsTransformableD065s()); // is contained in GetHeaderFlaggedNodes anyways
            rootNodes.AddRange(GetAltNFlaggedNodes());
            List<INode> allNodes = rootNodes.SelectMany(x => x.GetSelfAndDescendants()).ToList();
            return allNodes.AsReadOnly();
        }

        #endregion

        #region Methods (helper - Material/MaterialTexture)

        public ReadOnlyCollection<Material> GetMaterials() =>
            GetAllNodes().OfType<Mesh>().Select(x => x.Material).Distinct().ToList().AsReadOnly();

        public ReadOnlyCollection<MaterialTexture> GetMaterialTextures()
        {
            var materialTextures = new List<MaterialTexture>();

            // Model.Nodes
            materialTextures.AddRange(
                GetMaterials().Select(x => x.Texture)
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
