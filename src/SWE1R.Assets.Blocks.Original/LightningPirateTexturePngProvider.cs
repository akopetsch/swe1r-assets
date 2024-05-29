// SPDX-License-Identifier: GPL-2.0-only

using SWE1R.Assets.Blocks.Original.Resources;
using System.IO.Compression;
using ImageSharpImage = SixLabors.ImageSharp.Image;

namespace SWE1R.Assets.Blocks.Original
{
    public class LightningPirateTexturePngProvider
    {
        public static int[] ScrambledTextureIds =>
            new int[] { 49, 58, 99, 924, 966, 972, 991, 992, 1000, 1048, 1064 };

        public ImageSharpImage LoadTexturePng(int index)
        {
            string resourcePath = "LightningPirate.zip";
            using Stream resourceStream = new ResourceHelper().ReadEmbeddedResource(resourcePath);
            using var zipArchive = new ZipArchive(resourceStream);
            ZipArchiveEntry zipArchiveEntry = zipArchive.GetEntry($"{index:d4}.png");
            using Stream stream = zipArchiveEntry.Open();
            return ImageSharpImage.Load(stream);
        }
    }
}
