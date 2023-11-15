// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization.Nodes;
using SWE1R.Assets.Blocks.ModelBlock.Types;

namespace SWE1R.Assets.Blocks.Original.Tests.Format.Testers.ModelBlock.Headers
{
    public class TrakFormatTester : HeaderFormatTester<TrakHeader>
    {
        public TrakFormatTester(TrakHeader header) :
            base(header)
        { }

        public override void Test(Graph byteSerializerGraph)
        {
            Assert.True(Header.Nodes.Count == 6);
            Assert.True(
                Header.Animations.Count >= 3 &&
                Header.Animations.Count <= 75);
            Assert.True(Header.AltN == null);
        }
    }
}
