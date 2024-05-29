// SPDX-License-Identifier: GPL-2.0-only

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
