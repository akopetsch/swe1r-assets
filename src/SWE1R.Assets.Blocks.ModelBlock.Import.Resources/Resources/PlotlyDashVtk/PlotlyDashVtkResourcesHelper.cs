// SPDX-License-Identifier: MIT

using SWE1R.Assets.Blocks.Utils;

namespace SWE1R.Assets.Blocks.ModelBlock.Import.Resources.Resources.PlotlyDashVtk
{
    public class PlotlyDashVtkResourcesHelper : ResourceHelperBase
    {
        #region Methods

        public Stream OpenCowNoNormalsObjFile() =>
            ReadEmbeddedResource("cow-nonormals.obj");

        public Stream OpenPumpkinTall10kObjFile() =>
            ReadEmbeddedResource("pumpkin_tall_10k.obj");

        public Stream OpenTeapotObjFile() =>
            ReadEmbeddedResource("teapot.obj");

        public Stream OpenTeddyObjFile() =>
            ReadEmbeddedResource("teddy.obj");

        #endregion
    }
}
