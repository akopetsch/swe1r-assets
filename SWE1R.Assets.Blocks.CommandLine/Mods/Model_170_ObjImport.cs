// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization;
using SWE1R.Assets.Blocks.ModelBlock;
using SWE1R.Assets.Blocks.ModelBlock.Nodes;
using SWE1R.Assets.Blocks.TextureBlock;

namespace SWE1R.Assets.Blocks.CommandLine.Mods
{
    public class Model_170_ObjImport
    {
        public Model_170_ObjImport()
        {
            
        }

        public void Run()
        {
            string objFilename;
            float positionScale = 1;

            objFilename = "Upgrade_Plug_3_exp.groups.obj"; // FAIL (/2) // FAIL (/4, Tr)
            positionScale = 100;

            // v: 8 f: 8
            //objFilename = "obj/box.obj"; // OK (/2)
            //positionScale = 400;

            // v: 1598 f: 1598
            //objFilename = "obj/teddy.obj"; // FAIL (/2) // FAIL (/4, Tr)
            //positionScale = 15;

            // v: 64 f: 64
            //objFilename = "obj/humanoid_quad.obj"; // OK (/2) // OK (/4, Tr)
            //positionScale = 100;

            // v: 806 f: 806
            //objFilename = "obj/magnolia.obj"; // FAIL (/2) // OK (/4, Tr)
            //positionScale = 7;

            // v: 310 f: 310
            //objFilename = "obj/shuttle.obj"; // OK (/2) (but degenerate in-game) // OK (/4, Tr)
            //positionScale = 75;

            // v: 5252 f: 5252
            //objFilename = "obj/capsule.obj"; // FAIL (/2) // FAIL (/4, Tr)
            //positionScale = 75;

            // v: 1080 f: 1080
            //objFilename = "obj/violin_case.obj"; // FAIL (/2) // FAIL (/4, Tr)
            //positionScale = 150;

            // v: 326 f: 326
            //objFilename = "obj/gourd.obj"; // OK (/2) (but degenerate in-game)  // OK (/4, Tr)
            //positionScale = 150;

            // v: 507 f: 507
            //objFilename = "obj/monkey.obj"; // OK (/2) (but degenerate in-game, left-ear, around eyes) // OK (/4, Tr)
            //positionScale = 250;

            //objFilename = "cube.obj";
            //positionScale = 400;

            // load
            var modelBlock = Block.Load<Model>(BlockDefaultFilenames.ModelBlock);
            var textureBlock = Block.Load<TextureBlockItem>(BlockDefaultFilenames.TextureBlock);
            Model model = modelBlock[170]; // 170 = Part_Upgrade_TopSpeed_Plug3ThrustCoil
            model.Load(out ByteSerializerContext byteSerializerContext);

            // import
            var configuration = new ObjImporterConfiguration() {
                PositionScale = positionScale
            };
            var importer = new ObjImporter(
                objFilename, textureBlock, SystemDrawingImageRgba32Loader.LoadImageRgba32, configuration);
            importer.Import();

            var parentNode = byteSerializerContext.Graph.GetValue<TransformableD065>();
            parentNode.Children.Clear();
            parentNode.Children.Add(importer.MeshGroup3064);
            parentNode.UpdateChildrenCount();

            // save
            model.Save();
            modelBlock.Save(BlockDefaultFilenames.ModelBlock);
            textureBlock.Save(BlockDefaultFilenames.TextureBlock);
        }
    }
}
