// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using SWE1R.Assets.Blocks.ModelBlock.Import.Resources.Resources.UvCheckerMapMaker;
using SWE1R.Assets.Blocks.ModelBlock.Import.Tests.ResourcesTests;

namespace SWE1R.Assets.Blocks.ModelBlock.Import.Resources.Tests.ResourceHelperTests
{
    public class UvCheckerMapMakerResourceHelperTest :
        ResourceHelperTestBase<UvCheckerMapMakerResourcesHelper>
    {
        #region Methods

        [Fact] public void Test1024PngStream() => AssertStreamNotNull(ResourceHelper.OpenTest1024PngFile());
        [Fact] public void Test1024I8PngStream() => AssertStreamNotNull(ResourceHelper.OpenTest1024I8PngFile());
        [Fact] public void Test128PngStream() => AssertStreamNotNull(ResourceHelper.OpenTest128PngFile());
        [Fact] public void Test128I8PngStream() => AssertStreamNotNull(ResourceHelper.OpenTest128I8PngFile());
        [Fact] public void Test2048PngStream() => AssertStreamNotNull(ResourceHelper.OpenTest2048PngFile());
        [Fact] public void Test2048I8PngStream() => AssertStreamNotNull(ResourceHelper.OpenTest2048I8PngFile());
        [Fact] public void Test256PngStream() => AssertStreamNotNull(ResourceHelper.OpenTest256PngFile());
        [Fact] public void Test256I8PngStream() => AssertStreamNotNull(ResourceHelper.OpenTest256I8PngFile());
        [Fact] public void Test4096PngStream() => AssertStreamNotNull(ResourceHelper.OpenTest4096PngFile());
        [Fact] public void Test4096I8PngStream() => AssertStreamNotNull(ResourceHelper.OpenTest4096I8PngFile());
        [Fact] public void Test512PngStream() => AssertStreamNotNull(ResourceHelper.OpenTest512PngFile());
        [Fact] public void Test512I8PngStream() => AssertStreamNotNull(ResourceHelper.OpenTest512I8PngFile());
        [Fact] public void Test64Stream() => AssertStreamNotNull(ResourceHelper.OpenTest64PngFile());
        [Fact] public void Test64I8Stream() => AssertStreamNotNull(ResourceHelper.OpenTest64I8PngFile());

        #endregion
    }
}
