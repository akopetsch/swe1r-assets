// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization;
using SWE1R.Assets.Blocks.CommandLine.MaterialExamples;
using SWE1R.Assets.Blocks.CommandLine.Mods;
using SWE1R.Assets.Blocks.ModelBlock;
using SWE1R.Assets.Blocks.ModelBlock.Meshes;
using SWE1R.Assets.Blocks.TextureBlock;
using System.Diagnostics;

namespace SWE1R.Assets.Blocks.CommandLine
{
    public class Scratchpad
    {
        public Scratchpad()
        {
            RestoreBlockFileBackups();

            Foo2();
            //Foo();
            //new TextureImporterX();
        }

        private void Foo2()
        {
            //var mod = new Model_115_ObjImport();
            var mod = new Model_170_ObjImport();
            mod.Run();
        }

        private void Foo()
        {
            // load
            var modelBlock = Block.Load<Model>(BlockDefaultFilenames.ModelBlock);
            var textureBlock = Block.Load<Texture>(BlockDefaultFilenames.TextureBlock);
            Model model = modelBlock[115]; // 115 = tatooine training
            model.Load(out ByteSerializerContext byteSerializerContext);

            // modify
            var mesh = (Mesh)byteSerializerContext.Graph.GetValueComponent(typeof(Mesh), 0x017A2C).Value;
            mesh.Material = Model_115_MaterialExample.CreateMaterial();
            // to test if the example (input by hand) is correct

            // save
            model.Save();
            modelBlock.Save(BlockDefaultFilenames.ModelBlock);
            textureBlock.Save(BlockDefaultFilenames.TextureBlock);
        }

        private static void RestoreBlockFileBackups()
        {
            string batchFilename = "restore.bat";
            var process = new Process() {
                StartInfo = new ProcessStartInfo(batchFilename) {
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                },
            };
            process.Start();
            process.WaitForExit();
        }
    }
}
