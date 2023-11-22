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
        public MAltFormatTester(MAltHeader value, Graph byteSerializationGraph, AnalyticsFixture analyticsFixture) :
            base(value, byteSerializationGraph, analyticsFixture)
        { }

        public override void Test()
        {
            Assert.True(Value.Nodes.Count == 75);
            Assert.True(Value.Data == null);
            Assert.True(Value.Animations == null);
            Assert.True(Value.AltN.Count == 2 || Value.AltN.Count == 4);

            var altn1 = Value.AltN[1].FlaggedNode;
            Assert.True(altn1 is Group5064 || altn1 is MeshGroup3064);
            if (altn1 is Group5064)
                Assert.True(altn1.Children.Are<MeshGroup3064, TransformableD065>());
            if (altn1 is MeshGroup3064)
                Assert.True(
                    Value.AltN[0].FlaggedNode ==
                    Value.AltN[1].FlaggedNode);
        }
    }
}
