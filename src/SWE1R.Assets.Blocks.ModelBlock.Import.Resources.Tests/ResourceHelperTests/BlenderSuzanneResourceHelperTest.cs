// SPDX-License-Identifier: MIT

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
