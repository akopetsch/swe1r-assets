// SPDX-License-Identifier: MIT

using ByteSerialization;
using SixLabors.ImageSharp;
using SWE1R.Assets.Blocks.Images;
using SWE1R.Assets.Blocks.Images.ImageSharp;
using SWE1R.Assets.Blocks.ModelBlock;
using SWE1R.Assets.Blocks.ModelBlock.Materials;
using SWE1R.Assets.Blocks.ModelBlock.Materials.Export;
using SWE1R.Assets.Blocks.TextureBlock;
using System.Diagnostics;

namespace SWE1R.Assets.Blocks.CommandLine.Exporters
{
    public class ModelTexturesBlockExporter : BlockExporter<ModelBlockItem>
    {
        public Block<TextureBlockItem> TextureBlock { get; }

        public ModelTexturesBlockExporter(string blockPath, string textureBlockPath, int[] indices) : 
            base(blockPath, indices)
        {
            TextureBlock = new Block<TextureBlockItem>();
            TextureBlock.Load(textureBlockPath);
        }

        protected override void ExportItem(int index, ModelBlockItem item, ByteSerializerContext byteSerializerContext)
        {
            // get modelFolderPath
            string itemFolderName = BlockItem.GetIndexString(index);
            string itemFolderPath = Path.Combine(ExportFolderPath, itemFolderName);
            Directory.CreateDirectory(itemFolderPath);

            var materials = byteSerializerContext.Graph.GetValues<Material>().ToList();
            foreach (Material material in materials)
            {
                Debug.Write($"{material.Texture?.TextureIndex} ");
                Console.Write('.');

                var materialExporter = new MaterialExporter(material, TextureBlock);
                materialExporter.Export();
                ImageRgba32 image = materialExporter.EffectiveImage;
                if (image != null)
                {
                    // save as png
                    string exportFilename = $"{BlockItem.GetIndexString(material.Texture.TextureIndex.Value)}.png";
                    string exportPath = Path.Combine(itemFolderPath, exportFilename);
                    image.ToImageSharp().SaveAsPng(exportPath);
                }
            }

            item.Unload(); // reduces memory usage
        }
    }
}
