// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using SWE1R.Assets.Blocks.Metadata.IdNames;
using Xunit.Abstractions;

namespace SWE1R.Assets.Blocks.Original.Tests.Format.SpriteBlock
{
    public class N64JpTest : SpriteFormatTestBase
    {
        public N64JpTest(
            AnalyticsFixture analyticsFixture, ITestOutputHelper output) :
            base(analyticsFixture, output, SpriteBlockIdNames.N64Jp)
        { }

        [Fact]
        public void Test_179() => CompareItem(179);
        [Fact]
        public void Test_180() => CompareItem(180);
        [Fact]
        public void Test_181() => CompareItem(181);
    }
}
