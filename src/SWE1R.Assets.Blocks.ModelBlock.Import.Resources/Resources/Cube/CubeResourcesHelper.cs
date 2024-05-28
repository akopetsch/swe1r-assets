// Copyright 2024 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using SWE1R.Assets.Blocks.Utils;

namespace SWE1R.Assets.Blocks.ModelBlock.Import.Resources.Resources.Cube
{
    public class CubeResourcesHelper : ResourceHelperBase
    {
        #region Methods

        public Stream OpenCubeMtlFile() =>
            ReadEmbeddedResource("cube.mtl");

        public Stream OpenCubeObjFile() =>
            ReadEmbeddedResource("cube.obj");

        public Stream OpenCubePngFile() =>
            ReadEmbeddedResource("cube.png");

        #endregion
    }
}
