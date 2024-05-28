// Copyright 2024 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

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
