// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization;
using SWE1R.Assets.Blocks.Common.Images;
using SWE1R.Assets.Blocks.ModelBlock;
using SWE1R.Assets.Blocks.ModelBlock.Meshes;
using SWE1R.Assets.Blocks.ModelBlock.Nodes;
using SWE1R.Assets.Blocks.ModelBlock.Types;
using SWE1R.Assets.Blocks.TextureBlock;

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
            var textureBlock = Block.Load<Texture>(BlockDefaultFilenames.TextureBlock);
            ImageRgba32 image = SystemDrawingImageRgba32Loader.LoadImageRgba32("TestTexture_1024x1024.png");
            var importer = new MaterialImporter(image, textureBlock);
            importer.Import();
            textureBlock.Save(BlockDefaultFilenames.TextureBlock);

            // load model
            var modelBlock = Block.Load<Model>(BlockDefaultFilenames.ModelBlock);
            Model model = modelBlock[130]; // 130 = BeedosWildRide
            model.Load(out ByteSerializerContext byteSerializerContext);

            // modify mesh
            var mesh = byteSerializerContext.Graph.GetValue<Mesh>(0x0603A8);
            mesh.Material = importer.Material;

            // save model
            model.Save();
            modelBlock.Save(BlockDefaultFilenames.ModelBlock);
        }

        private void Change_142_ModelSkyboxTexture()
        {
            // import material
            var textureBlock = Block.Load<Texture>(BlockDefaultFilenames.TextureBlock);
            ImageRgba32 image = SystemDrawingImageRgba32Loader.LoadImageRgba32("TestTexture_2048x2048_I8.png");
            var importer = new MaterialImporter(image, textureBlock);
            importer.Import();
            textureBlock.Save(BlockDefaultFilenames.TextureBlock);

            // load model
            var modelBlock = Block.Load<Model>(BlockDefaultFilenames.ModelBlock);
            Model model = modelBlock[142]; // 142 = MonGazza_Speedway
            model.Load();

            // modify meshes
            var header = (TrakHeader)model.Header;
            List<Mesh> meshes = header.Skybox.GetDescendants().OfType<MeshGroup3064>()
                .SelectMany(mg => mg.Meshes).ToList();
            meshes.ForEach(m => m.Material = importer.Material);

            // save model
            model.Save();
            modelBlock.Save(BlockDefaultFilenames.ModelBlock);
        }
    }
}
