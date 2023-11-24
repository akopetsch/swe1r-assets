// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using SWE1R.Assets.Blocks.Metadata.IdNames;
using SWE1R.Assets.Blocks.SplineBlock;
using Xunit.Abstractions;

namespace SWE1R.Assets.Blocks.Original.Tests.Reserialization.SplineBlock
{
    public class N64Test : BlockItemsReserializationXUnitTest<SplineBlockItem>
    {
        public N64Test(ITestOutputHelper output) :
            base(output, SplineBlockIdNames.N64) { }

        [Fact]
        public void Test_006() => CompareItem(6);
        [Fact]
        public void Test_010() => CompareItem(10);
        [Fact]
        public void Test_022() => CompareItem(22);
        [Fact]
        public void Test_024() => CompareItem(24);
        [Fact]
        public void Test_025() => CompareItem(25);
        [Fact]
        public void Test_026() => CompareItem(26);
        [Fact]
        public void Test_028() => CompareItem(28);
        [Fact]
        public void Test_029() => CompareItem(29);
        [Fact]
        public void Test_032() => CompareItem(32);
        [Fact]
        public void Test_033() => CompareItem(33);
        [Fact]
        public void Test_034() => CompareItem(34);
        [Fact]
        public void Test_044() => CompareItem(44);
        [Fact]
        public void Test_045() => CompareItem(45);
        [Fact]
        public void Test_046() => CompareItem(46);
        [Fact]
        public void Test_048() => CompareItem(48);
        [Fact]
        public void Test_049() => CompareItem(49);
        [Fact]
        public void Test_051() => CompareItem(51);
        [Fact]
        public void Test_052() => CompareItem(52);
        [Fact]
        public void Test_054() => CompareItem(54);
        [Fact]
        public void Test_056() => CompareItem(56);
        [Fact]
        public void Test_065() => CompareItem(65);
        [Fact]
        public void Test_066() => CompareItem(66);
        [Fact]
        public void Test_067() => CompareItem(67);
        [Fact]
        public void Test_070() => CompareItem(70);
        [Fact]
        public void Test_071() => CompareItem(71);
        [Fact]
        public void Test_072() => CompareItem(72);
        [Fact]
        public void Test_073() => CompareItem(73);
        [Fact]
        public void Test_074() => CompareItem(74);
        [Fact]
        public void Test_076() => CompareItem(76);
        [Fact]
        public void Test_081() => CompareItem(81);
        [Fact]
        public void Test_082() => CompareItem(82);
        [Fact]
        public void Test_083() => CompareItem(83);
        [Fact]
        public void Test_084() => CompareItem(84);
        [Fact]
        public void Test_088() => CompareItem(88);
        [Fact]
        public void Test_089() => CompareItem(89);
        [Fact]
        public void Test_090() => CompareItem(90);
    }
}
