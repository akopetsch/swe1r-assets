// SPDX-License-Identifier: GPL-2.0-only

using SWE1R.Assets.Blocks.ModelBlock.Import.Resources.Resources.JBurkardt;

namespace SWE1R.Assets.Blocks.ModelBlock.Import.Tests.ResourcesTests
{
    public class JBurkardtResourceHelperTest :
        ResourceHelperTestBase<JBurkardtResourcesHelper>
    {
        [Fact] public void TestAirboatObjStream() => AssertStreamNotNull(ResourceHelper.OpenAirboatObjFile());
        [Fact] public void TestAlObjStream() => AssertStreamNotNull(ResourceHelper.OpenAlObjFile());
        [Fact] public void TestAlfa147ObjStream() => AssertStreamNotNull(ResourceHelper.OpenAlfa147ObjFile());
        [Fact] public void TestCessnaObjStream() => AssertStreamNotNull(ResourceHelper.OpenCessnaObjFile());
        [Fact] public void TestCubeObjStream() => AssertStreamNotNull(ResourceHelper.OpenCubeObjFile());
        [Fact] public void TestDiamondObjStream() => AssertStreamNotNull(ResourceHelper.OpenDiamondObjFile());
        [Fact] public void TestExampleMtlStream() => AssertStreamNotNull(ResourceHelper.OpenExampleMtlFile());
        [Fact] public void TestDodecahedronObjStream() => AssertStreamNotNull(ResourceHelper.OpenDodecahedronObjFile());
        [Fact] public void TestGourdObjStream() => AssertStreamNotNull(ResourceHelper.OpenGourdObjFile());
        [Fact] public void TestHumanoidQuadObjStream() => AssertStreamNotNull(ResourceHelper.OpenHumanoidQuadObjFile());
        [Fact] public void TestHumanoidTriObjStream() => AssertStreamNotNull(ResourceHelper.OpenHumanoidTriObjFile());
        [Fact] public void TestIcosahedronObjStream() => AssertStreamNotNull(ResourceHelper.OpenIcosahedronObjFile());
        [Fact] public void TestLampObStream() => AssertStreamNotNull(ResourceHelper.OpenLampObjFile());
        [Fact] public void TestMagnoliaObjsStream() => AssertStreamNotNull(ResourceHelper.OpenMagnoliaObjFile());
        [Fact] public void TestMiniCooperObjStream() => AssertStreamNotNull(ResourceHelper.OpenMinicooperObjFile());
        [Fact] public void TestOctahedronObjStream() => AssertStreamNotNull(ResourceHelper.OpenOctahedronObjFile());
        [Fact] public void TestPowerLinesObjStream() => AssertStreamNotNull(ResourceHelper.OpenPowerLinesObjFile());
        [Fact] public void TestPyramidObjStream() => AssertStreamNotNull(ResourceHelper.OpenPyramidObjFile());
        [Fact] public void TestRoiObjStream() => AssertStreamNotNull(ResourceHelper.OpenRoiObjFile());
        [Fact] public void TestSandalObjStream() => AssertStreamNotNull(ResourceHelper.OpenSandalObjFile());
        [Fact] public void TestShuttleObjStream() => AssertStreamNotNull(ResourceHelper.OpenShuttleObjFile());
        [Fact] public void TestSkyscraperObjStream() => AssertStreamNotNull(ResourceHelper.OpenSkyscraperObjFile());
        [Fact] public void TestSlotMachineObjStream() => AssertStreamNotNull(ResourceHelper.OpenSlotMachineObjFile());
        [Fact] public void TestSymphysisObjStream() => AssertStreamNotNull(ResourceHelper.OpenSymphysisObjFile());
        [Fact] public void TestTeaportObjStream() => AssertStreamNotNull(ResourceHelper.OpenTeapotObjFile());
        [Fact] public void TestTetrahedronObjStream() => AssertStreamNotNull(ResourceHelper.OpenTetrahedronObjFile());
        [Fact] public void TestTrumpetObjStream() => AssertStreamNotNull(ResourceHelper.OpenTrumpetObjFile());
        [Fact] public void TestViolinCaseObjStream() => AssertStreamNotNull(ResourceHelper.OpenViolinCaseObjFile());
        [Fact] public void TestVpMtlStream() => AssertStreamNotNull(ResourceHelper.OpenVpMtlFile());
    }
}
