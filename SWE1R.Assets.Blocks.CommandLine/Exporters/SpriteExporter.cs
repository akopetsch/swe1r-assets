// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization;
using SWE1R.Assets.Blocks.CommandLine.Extensions;
using SWE1R.Assets.Blocks.SpriteBlock;

namespace SWE1R.Assets.Blocks.CommandLine.Exporters
{
    public class SpriteExporter : BlockItemExporter<Sprite>
    {
        public SpriteExporter(string blockFilename, int[] indices) : 
            base(blockFilename, indices)
        { }

        protected override void ExportItem(int index, Sprite item, ByteSerializerContext byteSerializerContext)
        {
            using Image<Rgba32> image = item.Data.ExportImage().ToImageSharp();
            string exportFilename = Path.Combine(ExportFolderPath, $"{BlockItem.GetIndexString(index)}.png");
            image.SaveAsPng(exportFilename);
        }
    }
}
