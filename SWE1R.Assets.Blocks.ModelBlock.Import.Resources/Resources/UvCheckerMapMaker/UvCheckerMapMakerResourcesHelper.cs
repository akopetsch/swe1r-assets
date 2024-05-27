// Copyright 2024 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using SWE1R.Assets.Blocks.Utils;

namespace SWE1R.Assets.Blocks.ModelBlock.Import.Resources.Resources.UvCheckerMapMaker
{
    public class UvCheckerMapMakerResourcesHelper : ResourceHelperBase
    {
        #region Methods

        public Stream OpenTest1024PngFile() => ReadEmbeddedResource("Test_1024.png");
        public Stream OpenTest1024I8PngFile() => ReadEmbeddedResource("Test_1024_I8.png");
        public Stream OpenTest128PngFile() => ReadEmbeddedResource("Test_128.png");
        public Stream OpenTest128I8PngFile() => ReadEmbeddedResource("Test_128_I8.png");
        public Stream OpenTest2048PngFile() => ReadEmbeddedResource("Test_2048.png");
        public Stream OpenTest2048I8PngFile() => ReadEmbeddedResource("Test_2048_I8.png");
        public Stream OpenTest256PngFile() => ReadEmbeddedResource("Test_256.png");
        public Stream OpenTest256I8PngFile() => ReadEmbeddedResource("Test_256_I8.png");
        public Stream OpenTest4096PngFile() => ReadEmbeddedResource("Test_4096.png");
        public Stream OpenTest4096I8PngFile() => ReadEmbeddedResource("Test_4096_I8.png");
        public Stream OpenTest512PngFile() => ReadEmbeddedResource("Test_512.png");
        public Stream OpenTest512I8PngFile() => ReadEmbeddedResource("Test_512_I8.png");
        public Stream OpenTest64PngFile() => ReadEmbeddedResource("Test_64.png");
        public Stream OpenTest64I8PngFile() => ReadEmbeddedResource("Test_64_I8.png");

        #endregion
    }
}
