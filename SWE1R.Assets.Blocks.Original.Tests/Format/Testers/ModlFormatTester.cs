// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization.Nodes;
using SWE1R.Assets.Blocks.ModelBlock.Types;

namespace SWE1R.Assets.Blocks.Original.Tests.Format.Testers
{
    public class ModlFormatTester : FormatTester<ModlHeader>
    {
        public ModlFormatTester(ModlHeader header) : base(header)
        { }

        public override void Test(Graph byteSerializerGraph)
        {
            Assert.True(Header.Nodes.Count == 1);
            Assert.True(Header.Data == null);
            Assert.True(
                Header.Animations == null ||
                Header.Animations.Count == 1 ||
                Header.Animations.Count == 2 ||
                Header.Animations.Count == 3);
            Assert.True(Header.AltN == null);
        }
    }
}
