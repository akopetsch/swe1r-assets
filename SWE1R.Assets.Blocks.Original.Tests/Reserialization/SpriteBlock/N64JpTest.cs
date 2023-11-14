// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using SWE1R.Assets.Blocks.Metadata.IdNames;
using SWE1R.Assets.Blocks.SpriteBlock;
using Xunit.Abstractions;

namespace SWE1R.Assets.Blocks.Original.Tests.Reserialization.SpriteBlock
{
    public class N64JpTest : BlockItemsReserializationXUnitTest<Sprite>
    {
        public N64JpTest(ITestOutputHelper output) :
            base(output, SpriteBlockIdNames.N64Jp) { }

        [Fact]
        public void Test_179() => CompareItem(179);
        [Fact]
        public void Test_180() => CompareItem(180);
        [Fact]
        public void Test_181() => CompareItem(181);
    }
}
