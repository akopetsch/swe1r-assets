// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

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
