// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization.Nodes;
using SWE1R.Assets.Blocks.ModelBlock.Types;

namespace SWE1R.Assets.Blocks.Original.Tests.Format.Testers.ModelBlock.Headers
{
    public class ScenFormatTester : HeaderFormatTester<ScenHeader>
    {
        public ScenFormatTester(ScenHeader header) : base(header)
        { }

        public override void Test(Graph byteSerializerGraph)
        {
            Assert.True(Header.Nodes.Count == 83 || Header.Nodes.Count == 89);
            Assert.True(Header.Nodes.Count == 83 ?
                    Header.Data.List.Select(d => d.Integer.Value).Count() == 6 :
                    Header.Data == null);
            Assert.True(Header.Animations.Count >= 2 && Header.Animations.Count <= 126);
            Assert.True(Header.AltN == null);
        }
    }
}
