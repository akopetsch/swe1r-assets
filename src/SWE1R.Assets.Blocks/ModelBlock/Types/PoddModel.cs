// SPDX-License-Identifier: MIT

using ByteSerialization.Extensions;
using ByteSerialization.Nodes;
using SWE1R.Assets.Blocks.ModelBlock.Animations;
using SWE1R.Assets.Blocks.ModelBlock.Nodes;
using System.Linq;

namespace SWE1R.Assets.Blocks.ModelBlock.Types
{
    public class PoddModel : Model
    {
        #region Properties (helper)

        public Group5065 Root => (Group5065)Nodes[0].FlaggedNode;
        public Group5064 MainModel => (Group5064)Root.Children[0];
        public TransformableD065 LowPolyModel => (TransformableD065)Root.Children[1];
        
        public TransformableD065 Node02 => Nodes[02].FlaggedNode as TransformableD065;
        public TransformableD064 Node02_D064 => Node02.Children.First() as TransformableD064;
        public Group5064 Node02_D064_5064 => Node02_D064.Children.First() as Group5064;

        public TransformableD065 Node10 => Nodes[10].FlaggedNode as TransformableD065;
        
        public TransformableD065 Node17 => Nodes[17].FlaggedNode as TransformableD065;
        public Group5066 Node17_5066 => Node17.Children.First() as Group5066;

        public TransformableD065 Node18 => Nodes[18].FlaggedNode as TransformableD065;
        public TransformableD065 Node31 => Nodes[31].FlaggedNode as TransformableD065;
        public TransformableD065 Node34 => Nodes[34].FlaggedNode as TransformableD065;
        public TransformableD065 Node74 => Nodes[74].FlaggedNode as TransformableD065;

        #endregion

        #region Constructor

        public PoddModel() : base() =>
            Type = ModelType.Podd;

        #endregion

        #region Methods (serialization)

        public override bool HasExtraAlignment(FlaggedNode n, ByteSerializerGraph g)
        {
            if (
                // Node02
                n == Node02 && Node02.GetDescendants().OfType<MeshGroup3064>().Any() ||
                n == Node02_D064_5064?.Children.ElementAtOrDefault(2) ||

                // Node10
                n == Node10 ||

                // Node74
                n == Node74 ||

                // Node17
                (Node17.Children.Contains(n) && n is TransformableD065) ||
                Node17_5066.Children.ElementAtOrDefault(3) == n ||
                Node17_5066.Children.OfType<Group5064>().Any(c => c.Children?.ElementAtOrDefault(1) == n) ||
                Node17_5066.Children.OfType<Group5064>().Any(c => c.Children?.ElementAtOrDefault(2) == n) ||

                // Node18
                (Node18 != null && Node18.Children.ElementAtOrDefault(1) == n) ||

                // Header.Nodes[i]
                n == Nodes[3].FlaggedNode || 
                n == Nodes[4].FlaggedNode || 
                n == Nodes[11].FlaggedNode || 
                n == Nodes[65].FlaggedNode
                )
            {
                return true;
            }
            else
            {
                // TODO: clean-up mess
                var foo =
                    Node17_5066.Children.
                    OfType<Group5064>().
                    SelectMany(x => x.GetDescendants().OfType<TransformableD065>()).
                    Distinct().
                    Where(x => x.Children.Count == 2 && x.Children.OfType<Group5064>().Count() == 2).
                    ToList();
                if (foo.Any(x => n == x.Children.ElementAtOrDefault(1)))
                    return true;
            }
            return false;
        }

        public override bool HasExtraAlignment(Animation anim, ByteSerializerGraph graph) => false;

        #endregion
    }
}
