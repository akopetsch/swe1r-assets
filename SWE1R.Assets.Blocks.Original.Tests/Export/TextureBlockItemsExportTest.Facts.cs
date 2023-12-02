// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using SixLabors.ImageSharp;

namespace SWE1R.Assets.Blocks.Original.Tests.Export
{
    public partial class TextureBlockItemsExportTest
    {
        [Fact]
        public void Foo()
        {
            var foo = new LightningPirateTexturePngProvider().LoadTexturePng(0);
        }
    }
}
