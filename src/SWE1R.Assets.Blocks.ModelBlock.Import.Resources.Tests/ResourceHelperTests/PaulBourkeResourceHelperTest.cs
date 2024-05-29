// SPDX-License-Identifier: MIT

using SWE1R.Assets.Blocks.ModelBlock.Import.Resources.Resources.PaulBourke;

namespace SWE1R.Assets.Blocks.ModelBlock.Import.Tests.ResourcesTests
{
    public class PaulBourkeResourceHelperTest :
        ResourceHelperTestBase<PaulBourkeResourcesHelper>
    {
        [Fact] public void TestBoxObjStream() => AssertStreamNotNull(ResourceHelper.OpenBoxObjFile());
        [Fact] public void TestCapsule0JpgStream() => AssertStreamNotNull(ResourceHelper.OpenCapsule0JpgFile());
        [Fact] public void TestCapsuleMtlStream() => AssertStreamNotNull(ResourceHelper.OpenCapsuleMtlFile());
        [Fact] public void TestCapsuleObjStream() => AssertStreamNotNull(ResourceHelper.OpenCapsuleObjFile());
    }
}
