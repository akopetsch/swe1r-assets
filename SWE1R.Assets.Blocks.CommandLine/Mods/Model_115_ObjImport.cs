// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization;
using SWE1R.Assets.Blocks.ModelBlock;
using SWE1R.Assets.Blocks.ModelBlock.Nodes;
using SWE1R.Assets.Blocks.TextureBlock;
using System.Numerics;

namespace SWE1R.Assets.Blocks.CommandLine.Mods
{
    public class Model_115_ObjImport
    {
        public Model_115_ObjImport()
        {

        }

        public void Run()
        {
            string objFilename = "cube.obj";
            float positionScale = 40;

            // load
            var modelBlock = Block.Load<Model>(BlockDefaultFilenames.ModelBlock);
            var textureBlock = Block.Load<Texture>(BlockDefaultFilenames.TextureBlock);
            Model model = modelBlock[115]; // 115 = tatooine training
            model.Load(out ByteSerializerContext byteSerializerContext);

            // import
            var configuration = new ObjImporterConfiguration() {
                PositionScale = 40,
                PositionOffset = new Vector3(1620, 4440, -60),
                OverrideNormals = true,
            };
            var importer = new ObjImporter(objFilename, textureBlock, configuration);
            importer.Import();

            var parentNode = (Group5064)model.Header.Nodes[0].FlaggedNode;
            parentNode.Children.Add(importer.MeshGroup3064);
            parentNode.UpdateChildrenCount();

            // save
            model.Save();
            modelBlock.Save(BlockDefaultFilenames.ModelBlock);
            textureBlock.Save(BlockDefaultFilenames.TextureBlock);
        }
    }
}
