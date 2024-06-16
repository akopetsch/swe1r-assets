﻿// SPDX-License-Identifier: MIT

using ByteSerialization;
using ByteSerialization.IO;
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

        public ModelTexturesBlockExporter(string blockPath, string textureBlockPath, Endianness endianness, int[] indices) : 
            base(blockPath, endianness, indices)
        {
            TextureBlock = new Block<TextureBlockItem>(endianness);
            TextureBlock.Load(textureBlockPath);
        }

        protected override void ExportItem(int index, ModelBlockItem item, ByteSerializerContext byteSerializerContext)
        {
            // get modelFolderPath
            string itemFolderName = BlockItem.GetIndexString(index);
            string itemFolderPath = Path.Combine(ExportFolderPath, itemFolderName);
            Directory.CreateDirectory(itemFolderPath);

            var meshMaterials = byteSerializerContext.Graph.GetValues<MeshMaterial>().ToList();
            foreach (MeshMaterial meshMaterial in meshMaterials)
            {
                Debug.Write($"{meshMaterial.MaterialTexture?.TextureIndex} ");
                Console.Write('.');

                var materialExporter = new MeshMaterialExporter(meshMaterial, TextureBlock);
                materialExporter.Export();
                ImageRgba32 image = materialExporter.EffectiveImage;
                if (image != null)
                {
                    // save as png
                    string exportFilename = $"{BlockItem.GetIndexString(meshMaterial.MaterialTexture.TextureIndex.Value)}.png";
                    string exportPath = Path.Combine(itemFolderPath, exportFilename);
                    image.ToImageSharp().SaveAsPng(exportPath);
                }
            }

            item.Unload(); // reduces memory usage
        }
    }
}
