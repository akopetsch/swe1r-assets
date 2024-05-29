// SPDX-License-Identifier: GPL-2.0-only

using SWE1R.Assets.Blocks.Utils;

namespace SWE1R.Assets.Blocks.ModelBlock.Import.Resources.Resources.BlenderSuzanne
{
    public class BlenderSuzanneResourcesHelper : ResourceHelperBase
    {
        #region Methods

        public Stream OpenBlenderSuzanneMtlFile() =>
            ReadEmbeddedResource("blender-suzanne.mtl");

        public Stream OpenBlenderSuzanneObjFile() =>
            ReadEmbeddedResource("blender-suzanne.obj");

        #endregion
    }
}
