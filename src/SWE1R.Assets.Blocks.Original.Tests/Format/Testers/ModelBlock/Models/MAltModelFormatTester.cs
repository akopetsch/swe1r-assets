// SPDX-License-Identifier: MIT

using SWE1R.Assets.Blocks.ModelBlock.Nodes;
using SWE1R.Assets.Blocks.ModelBlock.Types;
using SWE1R.Assets.Blocks.Original.Tests.Extensions;

namespace SWE1R.Assets.Blocks.Original.Tests.Format.Testers.ModelBlock.Models
{
    public class MAltModelFormatTester : ModelFormatTester<MAltModel>
    {
        public override void Test()
        {
            base.Test();

            Assert.True(Value.Nodes.Count == 75);
            Assert.True(Value.Data == null);
            Assert.True(Value.Animations == null);
            Assert.True(Value.AltN.Count == 2 || Value.AltN.Count == 4);

            var altn1 = Value.AltN[1].FlaggedNode;
            Assert.True(altn1 is BasicNode || altn1 is MeshGroupNode);
            if (altn1 is BasicNode)
                Assert.True(altn1.Children.AreOfType<MeshGroupNode, TransformedWithPivotNode>());
            if (altn1 is MeshGroupNode)
                Assert.True(
                    Value.AltN[0].FlaggedNode ==
                    Value.AltN[1].FlaggedNode);
        }
    }
}
