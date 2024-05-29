// SPDX-License-Identifier: MIT

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
