// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using SWE1R.Assets.Blocks.ModelBlock.Import.Resources.Resources.Leadphalanx;

namespace SWE1R.Assets.Blocks.ModelBlock.Import.Tests.ResourcesTests
{
    public class LeadphalanxResourceHelperTest :
        ResourceHelperTestBase<LeadphalanxResourcesHelper>
    {
        [Fact] public void TestControlStabilizerWipMtlStream() => AssertStreamNotNull(ResourceHelper.OpenControlStabilizerWipMtlFile());
        [Fact] public void TestControlStabilizerWipObjStream() => AssertStreamNotNull(ResourceHelper.OpenControlStabilizerWipObjFile());
        [Fact] public void TestPodCleggHoldfastTestMtlStream() => AssertStreamNotNull(ResourceHelper.OpenPodCleggHoldfastTestMtlFile());
        [Fact] public void TestPodCleggHoldfastTestObjtream() => AssertStreamNotNull(ResourceHelper.OpenPodCleggHoldfastTestObjFile());
        [Fact] public void TestUpgradePlug3MtlStream() => AssertStreamNotNull(ResourceHelper.OpenUpgradePlug3ExpMtlFile());
        [Fact] public void TestUpgradePlug3ObjStream() => AssertStreamNotNull(ResourceHelper.OpenUpgradePlug3ExpObjFile());
    }
}
