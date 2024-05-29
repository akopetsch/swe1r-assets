// SPDX-License-Identifier: MIT

using SWE1R.Assets.Blocks.ModelBlock.Import.Resources.Resources.Cube;

namespace SWE1R.Assets.Blocks.ModelBlock.Import.Tests.ResourcesTests
{
    public class CubeResourceHelperTest :
        ResourceHelperTestBase<CubeResourcesHelper>
    {
        [Fact] public void TestCubeMtlStream() => AssertStreamNotNull(ResourceHelper.OpenCubeMtlFile());
        [Fact] public void TestCubeObjStream() => AssertStreamNotNull(ResourceHelper.OpenCubeObjFile());
        [Fact] public void TestCubePngStream() => AssertStreamNotNull(ResourceHelper.OpenCubePngFile());
    }
}
