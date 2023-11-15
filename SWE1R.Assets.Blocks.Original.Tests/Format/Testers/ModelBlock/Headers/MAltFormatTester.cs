// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization.Extensions;
using ByteSerialization.Nodes;
using SWE1R.Assets.Blocks.ModelBlock.Nodes;
using SWE1R.Assets.Blocks.ModelBlock.Types;

namespace SWE1R.Assets.Blocks.Original.Tests.Format.Testers.ModelBlock.Headers
{
    public class MAltFormatTester : HeaderFormatTester<MAltHeader>
    {
        public MAltFormatTester(MAltHeader header) :
            base(header)
        { }

        public override void Test(Graph byteSerializerGraph)
        {
            Assert.True(Header.Nodes.Count == 75);
            Assert.True(Header.Data == null);
            Assert.True(Header.Animations == null);
            Assert.True(Header.AltN.Count == 2 || Header.AltN.Count == 4);

            var altn1 = Header.AltN[1].FlaggedNode;
            Assert.True(altn1 is Group5064 || altn1 is MeshGroup3064);
            if (altn1 is Group5064)
                Assert.True(altn1.Children.Are<MeshGroup3064, TransformableD065>());
            if (altn1 is MeshGroup3064)
                Assert.True(
                    Header.AltN[0].FlaggedNode ==
                    Header.AltN[1].FlaggedNode);
        }
    }
}
