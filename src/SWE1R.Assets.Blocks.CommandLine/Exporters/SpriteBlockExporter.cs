// SPDX-License-Identifier: MIT

using ByteSerialization;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SWE1R.Assets.Blocks.Images.ImageSharp;
using SWE1R.Assets.Blocks.SpriteBlock;
using SWE1R.Assets.Blocks.SpriteBlock.Export;

namespace SWE1R.Assets.Blocks.CommandLine.Exporters
{
    public class SpriteBlockExporter : BlockExporter<SpriteBlockItem>
    {
        public SpriteBlockExporter(string blockFilename, int[] indices) : 
            base(blockFilename, indices)
        { }

        protected override void ExportItem(int index, SpriteBlockItem item, ByteSerializerContext byteSerializerContext)
        {
            var spriteExporter = new SpriteExporter(item.Sprite);
            spriteExporter.Export();
            using Image<Rgba32> image = spriteExporter.Image.ToImageSharp();
            string exportFilename = Path.Combine(ExportFolderPath, $"{BlockItem.GetIndexString(index)}.png");
            image.SaveAsPng(exportFilename);
        }
    }
}
