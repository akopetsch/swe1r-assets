// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file

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
