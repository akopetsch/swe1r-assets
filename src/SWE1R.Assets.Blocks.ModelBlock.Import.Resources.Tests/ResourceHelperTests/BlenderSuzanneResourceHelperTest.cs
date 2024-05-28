// Copyright 2024 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using SWE1R.Assets.Blocks.ModelBlock.Import.Resources.Resources.BlenderSuzanne;

namespace SWE1R.Assets.Blocks.ModelBlock.Import.Tests.ResourcesTests
{
    public class BlenderSuzanneResourceHelperTest :
        ResourceHelperTestBase<BlenderSuzanneResourcesHelper>
    {
        [Fact] public void TestBlenderSuzanneMtlStream() => AssertStreamNotNull(ResourceHelper.OpenBlenderSuzanneMtlFile());
        [Fact] public void TestBlenderSuzanneObjStream() => AssertStreamNotNull(ResourceHelper.OpenBlenderSuzanneObjFile());
    }
}
