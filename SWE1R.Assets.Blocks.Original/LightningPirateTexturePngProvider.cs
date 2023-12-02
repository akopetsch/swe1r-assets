// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using SWE1R.Assets.Blocks.Original.Resources;
using System.IO.Compression;
using ImageSharpImage = SixLabors.ImageSharp.Image;

namespace SWE1R.Assets.Blocks.Original
{
    public class LightningPirateTexturePngProvider
    {
        public ImageSharpImage LoadTexturePng(int index)
        {
            string resourcePath = "LightningPirate.zip";
            using Stream resourceStream = new OriginalBlocksResourceHelper().ReadEmbeddedResource(resourcePath);
            using var zipArchive = new ZipArchive(resourceStream);
            ZipArchiveEntry zipArchiveEntry = zipArchive.GetEntry($"{index:d4}.png");
            using Stream stream = zipArchiveEntry.Open();
            return ImageSharpImage.Load(stream);
        }
    }
}
