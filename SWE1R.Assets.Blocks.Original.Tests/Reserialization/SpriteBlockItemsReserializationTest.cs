// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using SWE1R.Assets.Blocks.Original.TestUtils;
using SWE1R.Assets.Blocks.SpriteBlock;
using Xunit.Abstractions;

namespace SWE1R.Assets.Blocks.Original.Tests.Reserialization
{
    public class SpriteBlockItemsReserializationTest :
        BlockItemsReserializationXUnitTestBase<SpriteBlockItem>
    {
        public SpriteBlockItemsReserializationTest(
            OriginalBlocksProviderFixture originalBlocksProviderFixture,
            ITestOutputHelper output) :
            base(originalBlocksProviderFixture, output)
        { }
    }
}
