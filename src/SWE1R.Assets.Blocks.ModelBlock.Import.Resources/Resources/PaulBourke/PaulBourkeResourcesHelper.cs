// SPDX-License-Identifier: MIT

using SWE1R.Assets.Blocks.Utils;

namespace SWE1R.Assets.Blocks.ModelBlock.Import.Resources.Resources.PaulBourke
{
    public class PaulBourkeResourcesHelper : ResourceHelperBase
    {
        #region Methods

        public Stream OpenBoxObjFile() =>
            ReadEmbeddedResource("box.obj");

        public Stream OpenCapsuleMtlFile() =>
            ReadEmbeddedResource("capsule.mtl");

        public Stream OpenCapsuleObjFile() =>
            ReadEmbeddedResource("capsule.obj");

        public Stream OpenCapsule0JpgFile() =>
            ReadEmbeddedResource("capsule0.jpg");

        #endregion
    }
}
