// SPDX-License-Identifier: MIT

using ByteSerialization.Nodes;
using SWE1R.Assets.Blocks.ModelBlock.Animations;
using SWE1R.Assets.Blocks.ModelBlock.Nodes;
using System.Linq;

namespace SWE1R.Assets.Blocks.ModelBlock.Types
{
    public class ModlModel : Model
    {
        #region Properties (helper)

        public TransformedWithPivotNode D065 => Nodes[0].FlaggedNode as TransformedWithPivotNode;
        public SelectorNode D065_5065 => D065?.Children[0] as SelectorNode;
        public BasicNode D065_5065_5064 => D065_5065?.Children.ElementAtOrDefault(1) as BasicNode;

        #endregion

        #region Constructor

        public ModlModel() : base() =>
            Type = ModelType.Modl;

        #endregion

        #region Methods (serialization)

        public override bool HasExtraAlignment(FlaggedNode fn, ByteSerializerGraph g) => 
            D065_5065_5064?.Children.Skip(1).Contains(fn) ?? false;

        public override bool HasExtraAlignment(Animation n, ByteSerializerGraph g) => 
            n == Animations?.First();

        #endregion
    }
}
