// SPDX-License-Identifier: MIT

using ByteSerialization.Nodes;
using SWE1R.Assets.Blocks.ModelBlock.Animations;
using SWE1R.Assets.Blocks.ModelBlock.Nodes;
using System.Linq;

namespace SWE1R.Assets.Blocks.ModelBlock.Types
{
    public class PartModel : Model
    {
        #region Properties (helper)

        public TransformedWithPivotNode Node0 => Nodes[0].FlaggedNode as TransformedWithPivotNode;
        public FlaggedNode Node0_Child => Node0.Children.First() as FlaggedNode;
        public FlaggedNode Node0_TransformedWithPivot => Node0.Children.First() as TransformedWithPivotNode;

        public PartModelKind Kind
        {
            get
            {
                if (Nodes.Count == 2)
                {
                    return PartModelKind.RacerLod1;
                }
                else
                {
                    if (Node0_TransformedWithPivot != null)
                        if (Node0_TransformedWithPivot.Children.Count == 8)
                            return PartModelKind.Unk_TransformedWithPivot_Shatter;
                        else
                            return PartModelKind.Unk_TransformedWithPivot;
                    else
                        return PartModelKind.Other;
                }
            }
        }

        #endregion

        #region Constructor

        public PartModel() : base() =>
            Type = ModelType.Part;

        #endregion

        #region Methods (serialization)

        public override bool HasExtraAlignment(FlaggedNode fn, ByteSerializerGraph g)
        {
            if (Kind == PartModelKind.Unk_TransformedWithPivot_Shatter)
                if (Node0_TransformedWithPivot.Children.Skip(1).Contains(fn))
                    return true;
            return false;
        }

        public override bool HasExtraAlignment(Animation n, ByteSerializerGraph g) =>
            Kind == PartModelKind.Unk_TransformedWithPivot_Shatter && n == Animations.First();

        #endregion
    }
}
