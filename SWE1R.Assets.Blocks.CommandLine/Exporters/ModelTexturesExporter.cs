// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization;
using SWE1R.Assets.Blocks.CommandLine.Extensions;
using SWE1R.Assets.Blocks.Common.Images;
using SWE1R.Assets.Blocks.ModelBlock;
using SWE1R.Assets.Blocks.ModelBlock.Meshes;
using SWE1R.Assets.Blocks.TextureBlock;
using System.Diagnostics;

namespace SWE1R.Assets.Blocks.CommandLine.Exporters
{
    public class ModelTexturesExporter : BlockItemExporter<Model>
    {
        public Block<Texture> TextureBlock { get; }

        public ModelTexturesExporter(string blockPath, string textureBlockPath, int[] indices) : 
            base(blockPath, indices)
        {
            TextureBlock = new Block<Texture>();
            TextureBlock.Load(textureBlockPath);
        }

        protected override void ExportItem(int index, Model item, ByteSerializerContext byteSerializerContext)
        {
            // get modelFolderPath
            string itemFolderName = BlockItem.GetIndexString(index);
            string itemFolderPath = Path.Combine(ExportFolderPath, itemFolderName);
            Directory.CreateDirectory(itemFolderPath);

            var materials = byteSerializerContext.Graph.GetValues<Material>().ToList();
            foreach (Material material in materials)
            {
                int? textureIndex = material.Texture?.IdField.Id;
                Debug.Write($"{textureIndex} ");
                Console.Write('.');

                ImageRgba32 image = material.Hack_ExportEffectiveImage(TextureBlock);
                if (image != null)
                {
                    // save as png
                    string exportFilename = $"{BlockItem.GetIndexString(textureIndex.Value)}.png";
                    string exportPath = Path.Combine(itemFolderPath, exportFilename);
                    image.ToImageSharp().SaveAsPng(exportPath);
                }
            }

            item.Unload(); // reduces memory usage
        }
    }
}
