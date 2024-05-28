// Copyright 2024 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using SWE1R.Assets.Blocks.ModelBlock.Import.Resources.Resources.PlotlyDashVtk;

namespace SWE1R.Assets.Blocks.ModelBlock.Import.Tests.ResourcesTests
{
    public class PlotlyDashVtkResourceHelperTest :
        ResourceHelperTestBase<PlotlyDashVtkResourcesHelper>
    {
        [Fact] public void TestCowNoNormalsObjStream() => AssertStreamNotNull(ResourceHelper.OpenCowNoNormalsObjFile());
        [Fact] public void TestPumpkinTall10kObjStream() => AssertStreamNotNull(ResourceHelper.OpenPumpkinTall10kObjFile());
        [Fact] public void TestTeapotObjStream() => AssertStreamNotNull(ResourceHelper.OpenTeapotObjFile());
        [Fact] public void TestTeddyObjStream() => AssertStreamNotNull(ResourceHelper.OpenTeddyObjFile());
    }
}
