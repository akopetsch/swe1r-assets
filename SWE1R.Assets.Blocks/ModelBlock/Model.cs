// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization.Attributes;
using ByteSerialization.Nodes;
using SWE1R.Assets.Blocks.ModelBlock.Animations;
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
        [SerializeUntil(0)]
        [ElementReference(ReferenceHandling.LowPriority)]
        [Order(3)] public List<Animation> Animations { get; set; }
        
        [Indicator("AltN")]
        [SerializeUntil(0)]
        [Order(4)] public List<FlaggedNodeOrGroup5066ChildReference> AltN { get; set; }

        [Length(4)]
        [Order(5)] private char[] HEnd { get; set; } = "HEnd".ToCharArray();

        #endregion

        #region Methods (serialization)

        public abstract bool HasExtraAlignment(FlaggedNode flaggedNode, ByteSerializerGraph graph);
        public abstract bool HasExtraAlignment(Animation anim, ByteSerializerGraph graph);

        #endregion

        #region Methods (helper - INode)

        public ReadOnlyCollection<FlaggedNode> GetHeaderFlaggedNodes() =>
            (Nodes.Select(x => x.FlaggedNode)
            .Where(x => x != null).Distinct().ToList() ?? new List<FlaggedNode>()).AsReadOnly();

        public ReadOnlyCollection<TransformableD065> GetAnimationsTransformableD065s() =>
            (Animations?.Select(x => x.TargetOrInteger.Target?.TransformableD065)
            .Where(x => x != null).Distinct().ToList() ?? new List<TransformableD065>()).AsReadOnly();

        public ReadOnlyCollection<FlaggedNode> GetAltNFlaggedNodes() =>
            (AltN?.Select(x => x.FlaggedNode)
            .Where(x => x != null).Distinct().ToList() ?? new List<FlaggedNode>()).AsReadOnly();

        public ReadOnlyCollection<INode> GetAllNodes()
        {
            var rootNodes = new List<FlaggedNode>();
            rootNodes.AddRange(GetHeaderFlaggedNodes());
            rootNodes.AddRange(GetAnimationsTransformableD065s());
            rootNodes.AddRange(GetAltNFlaggedNodes());
            List<INode> allNodes = rootNodes.SelectMany(x => x.GetSelfAndDescendants()).ToList();
            return allNodes.AsReadOnly();
        }

        #endregion
    }
}
