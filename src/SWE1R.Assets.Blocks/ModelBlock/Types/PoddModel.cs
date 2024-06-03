// SPDX-License-Identifier: MIT

using ByteSerialization.Nodes;
using SWE1R.Assets.Blocks.ModelBlock.Animations;
using SWE1R.Assets.Blocks.ModelBlock.Nodes;
using System.Linq;

namespace SWE1R.Assets.Blocks.ModelBlock.Types
{
    public class PoddModel : Model
    {
        #region Properties (helper)

        public SelectorNode Root => (SelectorNode)Nodes[0].FlaggedNode;
        public BasicNode MainModel => (BasicNode)Root.Children[0];
        public TransformedWithPivotNode LowPolyModel => (TransformedWithPivotNode)Root.Children[1];
        
        public TransformedWithPivotNode Node02 => Nodes[02].FlaggedNode as TransformedWithPivotNode;
        public TransformedNode Node02_Transformed => Node02.Children.First() as TransformedNode;
        public BasicNode Node02_Transformed_Basic => Node02_Transformed.Children.First() as BasicNode;

        public TransformedWithPivotNode Node10 => Nodes[10].FlaggedNode as TransformedWithPivotNode;
        
        public TransformedWithPivotNode Node17 => Nodes[17].FlaggedNode as TransformedWithPivotNode;
        public LodSelectorNode Node17_LodSelector => Node17.Children.First() as LodSelectorNode;

        public TransformedWithPivotNode Node18 => Nodes[18].FlaggedNode as TransformedWithPivotNode;
        public TransformedWithPivotNode Node31 => Nodes[31].FlaggedNode as TransformedWithPivotNode;
        public TransformedWithPivotNode Node34 => Nodes[34].FlaggedNode as TransformedWithPivotNode;
        public TransformedWithPivotNode Node74 => Nodes[74].FlaggedNode as TransformedWithPivotNode;

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
                n == Node02 && Node02.GetDescendants().OfType<MeshGroupNode>().Any() ||
                n == Node02_Transformed_Basic?.Children.ElementAtOrDefault(2) ||

                // Node10
                n == Node10 ||

                // Node74
                n == Node74 ||

                // Node17
                (Node17.Children.Contains(n) && n is TransformedWithPivotNode) ||
                Node17_LodSelector.Children.ElementAtOrDefault(3) == n ||
                Node17_LodSelector.Children.OfType<BasicNode>().Any(c => c.Children?.ElementAtOrDefault(1) == n) ||
                Node17_LodSelector.Children.OfType<BasicNode>().Any(c => c.Children?.ElementAtOrDefault(2) == n) ||

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
                    Node17_LodSelector.Children.
                    OfType<BasicNode>().
                    SelectMany(x => x.GetDescendants().OfType<TransformedWithPivotNode>()).
                    Distinct().
                    Where(x => x.Children.Count == 2 && x.Children.OfType<BasicNode>().Count() == 2).
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
