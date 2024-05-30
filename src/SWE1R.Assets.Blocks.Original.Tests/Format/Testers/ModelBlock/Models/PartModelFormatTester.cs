// SPDX-License-Identifier: MIT

using SWE1R.Assets.Blocks.ModelBlock.Nodes;
using SWE1R.Assets.Blocks.ModelBlock.Types;
using SWE1R.Assets.Blocks.Original.Tests.Extensions;

namespace SWE1R.Assets.Blocks.Original.Tests.Format.Testers.ModelBlock.Models
{
    public class PartModelFormatTester : ModelFormatTester<PartModel>
    {
        public override void Test()
        {
            base.Test();

            AssertHeader();
            AssertNode0();
            AssertKind();
        }

        private void AssertHeader()
        {
            Assert.True(Value.Nodes.Count == 2 || Value.Nodes.Count == 5);
            Assert.True(Value.Data == null);
            Assert.True(Value.Animations == null || Value.Animations.Count >= 1 && Value.Animations.Count <= 10);
            Assert.True(Value.AltN == null);
        }

        private void AssertNode0()
        {
            Assert.True(Value.Node0 != null);
            Assert.True(Value.Node0.Children.Count == 1);
        }

        private void AssertKind()
        {
            switch (Value.Kind)
            {
                case PartModelKind.RacerLod1:
                    Assert.True(Value.Nodes.Count == 2);
                    Assert.True(Value.Nodes[1].FlaggedNode is Group5065);
                    Assert.True(Value.Node0_Child is Group5065);
                    break;
                default:
                    Assert.True(Value.Nodes.Count == 5);
                    Assert.True(Value.Nodes[1].FlaggedNode == null);
                    Assert.True(Value.Nodes[2].FlaggedNode == null);
                    Assert.True(Value.Nodes[3].FlaggedNode == null);
                    Assert.True(Value.Nodes[4].FlaggedNode == null);
                    Assert.True(Value.Node0.Children.AreOfType<MeshGroup3064, Group5064, TransformableD065>());
                    break;
            }
        }
    }
}
