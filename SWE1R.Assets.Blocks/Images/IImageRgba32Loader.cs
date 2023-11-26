// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

namespace SWE1R.Assets.Blocks.Images
{
    public interface IImageRgba32Loader
    {
        ImageRgba32 Load(string imageFilename);
    }
}
