// Copyright 2024 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

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
