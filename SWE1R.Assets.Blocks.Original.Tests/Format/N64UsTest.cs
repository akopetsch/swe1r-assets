// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using SWE1R.Assets.Blocks.Metadata.IdNames;
using Xunit.Abstractions;

namespace SWE1R.Assets.Blocks.Original.Tests.Format
{
    public class N64UsTest : TestBase
    {
        public N64UsTest(ITestOutputHelper output) :
            base(output, ModelBlockIdNames.Default)
        { }

        [Fact]
        public void Test_001() => CompareItem(1);

        [Fact]
        public void Test_115() => CompareItem(115);

        [Fact]
        public void Test_128() => CompareItem(128);

        [Fact]
        public void Test_132() => CompareItem(132);
        [Fact]
        public void Test_133() => CompareItem(133);
        [Fact]
        public void Test_134() => CompareItem(134);
        [Fact]
        public void Test_135() => CompareItem(135);
        [Fact]
        public void Test_136() => CompareItem(136);
        [Fact]
        public void Test_137() => CompareItem(137);
        [Fact]
        public void Test_138() => CompareItem(138);
        [Fact]
        public void Test_139() => CompareItem(139);
        [Fact]
        public void Test_140() => CompareItem(140);
        [Fact]
        public void Test_141() => CompareItem(141);
        [Fact]
        public void Test_142() => CompareItem(142);

        [Fact]
        public void Test_144() => CompareItem(144);
        [Fact]
        public void Test_145() => CompareItem(145);

        [Fact]
        public void Test_226() => CompareItem(226);
        [Fact]
        public void Test_227() => CompareItem(227);
        [Fact]
        public void Test_228() => CompareItem(228);

        [Fact]
        public void Test_243() => CompareItem(243);

        [Fact]
        public void Test_281() => CompareItem(281);
        [Fact]
        public void Test_282() => CompareItem(282);
        [Fact]
        public void Test_283() => CompareItem(283);
        [Fact]
        public void Test_284() => CompareItem(284);
        [Fact]
        public void Test_285() => CompareItem(285);
        [Fact]
        public void Test_286() => CompareItem(286);
        [Fact]
        public void Test_287() => CompareItem(287);
        [Fact]
        public void Test_288() => CompareItem(288);
        [Fact]
        public void Test_289() => CompareItem(289);

        [Fact]
        public void Test_294() => CompareItem(294);

        [Fact]
        public void Test_299() => CompareItem(299);

        [Fact]
        public void Test_301() => CompareItem(301);

        [Fact]
        public void Test_303() => CompareItem(303);
        [Fact]
        public void Test_304() => CompareItem(304);
        [Fact]
        public void Test_305() => CompareItem(305);
        [Fact]
        public void Test_306() => CompareItem(306);
    }
}
