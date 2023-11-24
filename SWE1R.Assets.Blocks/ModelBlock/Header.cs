// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization.Attributes;
using ByteSerialization.Nodes;
using SWE1R.Assets.Blocks.ModelBlock.Animations;
using SWE1R.Assets.Blocks.ModelBlock.Nodes;
using SWE1R.Assets.Blocks.ModelBlock.Types;
using System.Collections.Generic;

namespace SWE1R.Assets.Blocks.ModelBlock
{
    public abstract class Header
    {
        #region Properties (serialized)

        [RecordTypeIdentifier(ModelType.MAlt, typeof(MAltHeader))]
        [RecordTypeIdentifier(ModelType.Modl, typeof(ModlHeader))]
        [RecordTypeIdentifier(ModelType.Part, typeof(PartHeader))]
        [RecordTypeIdentifier(ModelType.Podd, typeof(PoddHeader))]
        [RecordTypeIdentifier(ModelType.Pupp, typeof(PuppHeader))]
        [RecordTypeIdentifier(ModelType.Scen, typeof(ScenHeader))]
        [RecordTypeIdentifier(ModelType.Trak, typeof(TrakHeader))]
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

        #region Properties (logical)

        public ModelBlockItem BlockItem { get; set; }

        #endregion

        #region Methods (helper)

        public abstract bool HasExtraAlignment(FlaggedNode flaggedNode, Graph graph);
        public abstract bool HasExtraAlignment(Animation anim, Graph graph);
        
        #endregion
    }
}
