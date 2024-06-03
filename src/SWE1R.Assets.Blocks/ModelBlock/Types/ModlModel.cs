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

        public TransformedWithPivotNode TransformedWithPivot => Nodes[0].FlaggedNode as TransformedWithPivotNode;
        public SelectorNode TransformedWithPivot_Selector => TransformedWithPivot?.Children[0] as SelectorNode;
        public BasicNode TransformedWithPivot_Selector_Basic => TransformedWithPivot_Selector?.Children.ElementAtOrDefault(1) as BasicNode;

        #endregion

        #region Constructor

        public ModlModel() : base() =>
            Type = ModelType.Modl;

        #endregion

        #region Methods (serialization)

        public override bool HasExtraAlignment(FlaggedNode fn, ByteSerializerGraph g) => 
            TransformedWithPivot_Selector_Basic?.Children.Skip(1).Contains(fn) ?? false;

        public override bool HasExtraAlignment(Animation n, ByteSerializerGraph g) => 
            n == Animations?.First();

        #endregion
    }
}
