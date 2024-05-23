// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

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
