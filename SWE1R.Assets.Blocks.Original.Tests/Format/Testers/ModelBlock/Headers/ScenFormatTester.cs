// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization.Nodes;
using SWE1R.Assets.Blocks.ModelBlock.Types;

namespace SWE1R.Assets.Blocks.Original.Tests.Format.Testers.ModelBlock.Headers
{
    public class ScenFormatTester : HeaderFormatTester<ScenModel>
    {
        public ScenFormatTester(ScenModel value, Graph byterSerializationGraph, AnalyticsFixture analyticsFixture) :
            base(value, byterSerializationGraph, analyticsFixture)
        { }

        public override void Test()
        {
            Assert.True(Value.Nodes.Count == 83 || Value.Nodes.Count == 89);
            Assert.True(Value.Nodes.Count == 83 ?
                    Value.Data.List.Select(d => d.Integer.Value).Count() == 6 :
                    Value.Data == null);
            Assert.True(Value.Animations.Count >= 2 && Value.Animations.Count <= 126);
            Assert.True(Value.AltN == null);
        }
    }
}
