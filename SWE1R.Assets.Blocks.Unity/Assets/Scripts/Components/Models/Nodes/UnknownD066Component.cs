// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using SWE1R.Assets.Blocks.Unity.Extensions;
using Swe1rFlaggedNode = SWE1R.Assets.Blocks.ModelBlock.Nodes.FlaggedNode;
using Swe1rUnknownD066 = SWE1R.Assets.Blocks.ModelBlock.Nodes.UnknownD066;
using UnityVector3 = UnityEngine.Vector3;

namespace SWE1R.Assets.Blocks.Unity.Components.Models.Nodes
{
    public class UnknownD066Component : FlaggedNodeComponent<Swe1rUnknownD066>
    {
        public short word1;
        public short word2;
        public UnityVector3 vector;

        public override void Import(Swe1rUnknownD066 source)
        {
            base.Import(source);
            word1 = source.Word1;
            word2 = source.Word2;
            vector = source.Vector.ToUnityVector3();
        }

        public override Swe1rFlaggedNode Export(ModelExporter modelExporter)
        {
            var result = (Swe1rUnknownD066)base.Export(modelExporter);
            result.Word1 = word1;
            result.Word2 = word2;
            result.Vector = vector.ToSwe1rVector3Single();
            return result;
        }
    }
}
