// SPDX-License-Identifier: MIT

using ByteSerialization.Nodes;
using SWE1R.Assets.Blocks.ModelBlock.Animations;
using SWE1R.Assets.Blocks.ModelBlock.Nodes;
using System.Linq;

namespace SWE1R.Assets.Blocks.ModelBlock.Types
{
    public class MAltModel : Model
    {
        #region Constructor

        public MAltModel() : base() =>
            Type = ModelType.MAlt;

        #endregion

        #region Methods (serialization)

        public override bool HasExtraAlignment(FlaggedNode fn, ByteSerializerGraph g)
        {
            var altn1 = (AltN[1].FlaggedNode as BasicNode);
            if (altn1 != null)
            {
                TransformedWithPivotNode d0, d1;
                var mg = altn1.Children.OfType<MeshGroupNode>().First();
                if (mg == altn1.Children.Last())
                {
                    d0 = altn1.Children[0] as TransformedWithPivotNode;
                    d1 = altn1.Children[1] as TransformedWithPivotNode;
                }
                else
                {
                    int i = altn1.Children.IndexOf(mg);
                    d0 = altn1.Children.ElementAtOrDefault(i + 1) as TransformedWithPivotNode;
                    d1 = altn1.Children.ElementAtOrDefault(i + 2) as TransformedWithPivotNode;
                }
                if (fn == d0 || fn == d1)
                    return true;
            }
            return false;
        }

        public override bool HasExtraAlignment(Animation anim, ByteSerializerGraph graph) => 
            false;

        #endregion
    }
}
