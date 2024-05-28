// Copyright 2024 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using SWE1R.Assets.Blocks.Utils;

namespace SWE1R.Assets.Blocks.ModelBlock.Import.Resources.Resources.Leadphalanx
{
    public class LeadphalanxResourcesHelper : ResourceHelperBase
    {
        #region Methods

        public Stream OpenControlStabilizerWipMtlFile() =>
            ReadEmbeddedResource("control_stabilizer_WIP.mtl");

        public Stream OpenControlStabilizerWipObjFile() =>
            ReadEmbeddedResource("control_stabilizer_WIP.obj");

        public Stream OpenPodCleggHoldfastTestMtlFile() =>
            ReadEmbeddedResource("pod_clegg_holdfast_TEST.mtl");

        public Stream OpenPodCleggHoldfastTestObjFile() =>
            ReadEmbeddedResource("pod_clegg_holdfast_TEST.obj");

        public Stream OpenUpgradePlug2DViewPngFile() =>
            ReadEmbeddedResource("Upgrade_Plug_2D_View.png");

        public Stream OpenUpgradePlug3ExpMtlFile() =>
            ReadEmbeddedResource("Upgrade_Plug_3_exp.mtl");

        public Stream OpenUpgradePlug3ExpObjFile() =>
            ReadEmbeddedResource("Upgrade_Plug_3_exp.obj");

        #endregion
    }
}
