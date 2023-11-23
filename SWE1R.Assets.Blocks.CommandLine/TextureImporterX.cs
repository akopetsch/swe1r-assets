// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization;
using SWE1R.Assets.Blocks.CommandLine.Extensions;
using SWE1R.Assets.Blocks.CommandLine.MaterialExamples;
using SWE1R.Assets.Blocks.Common.Images;
using SWE1R.Assets.Blocks.ModelBlock;
using SWE1R.Assets.Blocks.ModelBlock.Meshes;
using SWE1R.Assets.Blocks.ModelBlock.Nodes;
using SWE1R.Assets.Blocks.ModelBlock.Types;
using SWE1R.Assets.Blocks.TextureBlock;
using SystemDrawingBitmap = System.Drawing.Bitmap;
using SystemDrawingImage = System.Drawing.Image;

namespace SWE1R.Assets.Blocks.CommandLine
{
    public class TextureImporterX
    {
        public TextureImporterX()
        {
            Change_142_ModelSkyboxTexture();
            Change_130_IcicleTexture();
        }

        private void Change_130_IcicleTexture()
        {
            // import material
            ImageRgba32 image = GetImageRgba32("TestTexture_1024x1024.png");
            var importer = new TextureImporter(image);
            importer.Import();

            // save texture
            var textureBlock = Block.Load<Texture>(BlockDefaultFilenames.TextureBlock);
            importer.Texture.Block = textureBlock;
            textureBlock.Add(importer.Texture);
            textureBlock.Save(BlockDefaultFilenames.TextureBlock);

            // create material
            Material material = Model_130_MaterialExample.CreateMaterial();
            MaterialTexture mt = material.Texture;
            mt.Width = (short)image.Width;
            mt.Height = (short)image.Height;
            mt.Width4 = (short)(image.Width * 4);
            mt.Height4 = (short)(image.Height * 4);
            mt.Width_Unk = 32768; // 32768 = 64 * 512
            mt.Width_Unk = 32768; // 32768 = 64 * 512
            mt.IdField.Id = importer.Texture.Index.Value;

            // load model
            var modelBlock = Block.Load<Model>(BlockDefaultFilenames.ModelBlock);
            Model model = modelBlock[130]; // 130 = BeedosWildRide
            model.Load(out ByteSerializerContext byteSerializerContext);

            // modify mesh
            var mesh = byteSerializerContext.Graph.GetValue<Mesh>(0x0603A8);
            mesh.Material = material;

            // save model
            model.Save();
            modelBlock.Save(BlockDefaultFilenames.ModelBlock);
        }

        private void Change_142_ModelSkyboxTexture()
        {
            // import material
            ImageRgba32 image = GetImageRgba32($"TestTexture_2048x2048_I8.png");
            var importer = new TextureImporter(image);
            importer.Import();

            // save texture
            var textureBlock = Block.Load<Texture>(BlockDefaultFilenames.TextureBlock);
            importer.Texture.Block = textureBlock;
            textureBlock.Add(importer.Texture);
            textureBlock.Save(BlockDefaultFilenames.TextureBlock);

            // create material
            Material material = Model_142_MaterialExample.CreateMaterial();
            MaterialTexture mt = material.Texture;
            mt.Width = (short)image.Width;
            mt.Height = (short)image.Height;
            mt.Width4 = (short)(image.Width * 4);
            mt.Height4 = (short)(image.Height * 4);
            mt.Width_Unk = 32768; // 32768 = 64 * 512
            mt.Width_Unk = 32768; // 32768 = 64 * 512
            mt.IdField.Id = importer.Texture.Index.Value;

            // load model
            var modelBlock = Block.Load<Model>(BlockDefaultFilenames.ModelBlock);
            Model model = modelBlock[142]; // 142 = MonGazza_Speedway
            model.Load();

            // modify meshes
            var header = (TrakHeader)model.Header;
            List<Mesh> meshes = header.Skybox.GetDescendants().OfType<MeshGroup3064>()
                .SelectMany(mg => mg.Meshes).ToList();
            meshes.ForEach(m => m.Material = material);

            // save model
            model.Save();
            modelBlock.Save(BlockDefaultFilenames.ModelBlock);
        }

        private ImageRgba32 GetImageRgba32(string imageFilename)
        {
            using var systemDrawingBitmap = 
                (SystemDrawingBitmap)SystemDrawingImage.FromFile(imageFilename);
            return systemDrawingBitmap.ToImageRgba32();
        }
    }
}
