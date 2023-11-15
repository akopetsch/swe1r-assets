// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization.Nodes;
using SWE1R.Assets.Blocks.ModelBlock.Types;

namespace SWE1R.Assets.Blocks.Original.Tests.Format.Testers.ModelBlock.Headers
{
    public class PuppFormatTester : HeaderFormatTester<PuppHeader>
    {
        public PuppFormatTester(PuppHeader header) : base(header)
        { }

        public override void Test(Graph byteSerializerGraph)
        {
            Assert.True(Header.Nodes.Count == 9);
            Assert.True(Header.Data == null);
            Assert.True(Header.Animations.Count >= 3 && Header.Animations.Count <= 33);
            Assert.True(Header.AltN == null);
        }
    }
}
