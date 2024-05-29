// SPDX-License-Identifier: MIT

using ByteSerialization.Extensions;
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
            var altn1 = (AltN[1].FlaggedNode as Group5064);
            if (altn1 != null)
            {
                TransformableD065 d0, d1;
                var mg = altn1.Children.OfType<MeshGroup3064>().First();
                if (mg == altn1.Children.Last())
                {
                    d0 = altn1.Children[0] as TransformableD065;
                    d1 = altn1.Children[1] as TransformableD065;
                }
                else
                {
                    int i = altn1.Children.IndexOf(mg);
                    d0 = altn1.Children.ElementAtOrDefault(i + 1) as TransformableD065;
                    d1 = altn1.Children.ElementAtOrDefault(i + 2) as TransformableD065;
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
