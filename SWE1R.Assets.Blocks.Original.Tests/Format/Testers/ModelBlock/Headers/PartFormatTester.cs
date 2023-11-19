// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization.Extensions;
using ByteSerialization.Nodes;
using SWE1R.Assets.Blocks.ModelBlock.Nodes;
using SWE1R.Assets.Blocks.ModelBlock.Types;

namespace SWE1R.Assets.Blocks.Original.Tests.Format.Testers.ModelBlock.Headers
{
    public class PartFormatTester : HeaderFormatTester<PartHeader>
    {
        public PartFormatTester(PartHeader header) : base(header)
        { }

        public override void Test(Graph byteSerializerGraph)
        {
            AssertHeader();

            Assert.True(Header.Node0 != null);
            Assert.True(Header.Node0.Children.Count == 1);

            AssertKind();
        }

        private void AssertHeader()
        {
            Assert.True(Header.Nodes.Count == 2 || Header.Nodes.Count == 5);
            Assert.True(Header.Data == null);
            Assert.True(Header.Animations == null || (Header.Animations.Count >= 1 && Header.Animations.Count <= 10));
            Assert.True(Header.AltN == null);
        }

        private void AssertKind()
        {
            switch (Header.GetKind())
            {
                case PartKind.RacerLod1:
                    Assert.True(Header.Nodes.Count == 2);
                    Assert.True(Header.Nodes[1].FlaggedNode is Group5065);
                    Assert.True(Header.Node0_Child is Group5065);
                    break;
                default:
                    Assert.True(Header.Nodes.Count == 5);
                    Assert.True(Header.Nodes[1].FlaggedNode == null);
                    Assert.True(Header.Nodes[2].FlaggedNode == null);
                    Assert.True(Header.Nodes[3].FlaggedNode == null);
                    Assert.True(Header.Nodes[4].FlaggedNode == null);
                    Assert.True(Header.Node0.Children.Are<MeshGroup3064, Group5064, TransformableD065>());
                    break;
            }
        }
    }
}
