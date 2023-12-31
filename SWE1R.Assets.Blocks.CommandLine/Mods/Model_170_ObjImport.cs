﻿// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization;
using SWE1R.Assets.Blocks.Images.SystemDrawing;
using SWE1R.Assets.Blocks.ModelBlock;
using SWE1R.Assets.Blocks.ModelBlock.Import;
using SWE1R.Assets.Blocks.ModelBlock.Meshes;
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

            //objFilename = "pod_clegg_holdfast_TEST.obj";
            //positionScale = 100;

            objFilename = "control_stabilizer_WIP.obj";
            positionScale = 100;

            //objFilename = "Upgrade_Plug_3_exp.groups.obj"; // OK
            //positionScale = 100;

            // v: 8
            //objFilename = "obj/box.obj"; // OK
            //positionScale = 400;

            // v: 1598
            //objFilename = "obj/teddy.obj"; // OK (with vt-limit = 1000)
            //positionScale = 15;

            // v: 64
            //objFilename = "obj/humanoid_quad.obj"; // OK
            //positionScale = 100;

            // v: 806
            //objFilename = "obj/magnolia.obj"; // OK
            //positionScale = 7;

            // v: 310
            //objFilename = "obj/shuttle.obj"; // OK
            //positionScale = 75;

            //objFilename = "capsule.obj"; // CRASH (even with vt-limit = 1000)
            //positionScale = 75;

            //objFilename = "obj/violin_case.obj"; // OK
            //positionScale = 150;

            // v: 326
            //objFilename = "obj/gourd.obj"; // OK
            //positionScale = 150;

            // v: 507
            //objFilename = "obj/monkey.obj"; // OK
            //positionScale = 250;

            //objFilename = "cube.obj"; // OK
            //positionScale = 400;

            // load
            var modelBlock = Block.Load<ModelBlockItem>(BlockDefaultFilenames.ModelBlock);
            var textureBlock = Block.Load<TextureBlockItem>(BlockDefaultFilenames.TextureBlock);
            ModelBlockItem modelBlockItem = modelBlock[170]; // 170 = Part_Upgrade_TopSpeed_Plug3ThrustCoil
            modelBlockItem.Load(out ByteSerializerContext byteSerializerContext);

            // import
            var configuration = new ModelObjImporterConfiguration() {
                PositionScale = positionScale,
                MaxVertexCountPerMesh = 1000,
            };
            var importer = new ModelObjImporter(
                objFilename, textureBlock, new SystemDrawingImageRgba32Loader(), configuration);
            importer.Import();
            PrintModelImporterDetails(importer);

            var parentNode = byteSerializerContext.Graph.GetValue<TransformableD065>();
            parentNode.Children.Clear();
            parentNode.Children.Add(importer.MeshGroup3064);
            parentNode.UpdateChildrenCount();

            // save
            modelBlockItem.Save();
            modelBlock.Save(BlockDefaultFilenames.ModelBlock);
            textureBlock.Save(BlockDefaultFilenames.TextureBlock);
        }

        private void PrintModelImporterDetails(ModelObjImporter modelObjImporter)
        {
            MeshGroup3064 meshGroup3064 = modelObjImporter.MeshGroup3064;
            for (int i = 0; i < meshGroup3064.Meshes.Count; i++)
                Console.WriteLine(GetMeshInfoString(i, meshGroup3064.Meshes[i]));
            Console.WriteLine(GetSumInfoString(meshGroup3064));
        }

        private string GetMeshInfoString(int i, Mesh mesh) =>
            $"[{i}] {GetInfoString(
                mesh.VisibleVertices.Count, 
                mesh.VisibleIndicesChunks.Count)}";

        private string GetSumInfoString(MeshGroup3064 meshGroup3064) =>
            $"total: {GetInfoString(
                meshGroup3064.Meshes.Sum(m => m.VisibleVertices.Count), 
                meshGroup3064.Meshes.Sum(m => m.VisibleIndicesChunks.Count))}";

        private string GetInfoString(int verticesCount, int chunksCount) =>
            $"{nameof(verticesCount)} = {verticesCount}, " +
            $"{nameof(chunksCount)} = {chunksCount}";
    }
}
