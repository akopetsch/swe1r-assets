// SPDX-License-Identifier: GPL-2.0-only

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
