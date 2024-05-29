// SPDX-License-Identifier: GPL-2.0-only

using SWE1R.Assets.Blocks.CommandLine.ModelInjectors;
using SWE1R.Assets.Blocks.ModelBlock;
using SWE1R.Assets.Blocks.ModelBlock.Import;
using SWE1R.Assets.Blocks.ModelBlock.Import.Resources.ResourceHelpers.Obj;
using SWE1R.Assets.Blocks.TextureBlock;
using System.Diagnostics;

namespace SWE1R.Assets.Blocks.CommandLine
{
    public class Scratchpad
    {
        public Scratchpad()
        {
            RestoreBlockFileBackups();

            // block filenames
            string modelBlockFilename = BlockDefaultFilenames.ModelBlock;
            string textureBlockFilename = BlockDefaultFilenames.TextureBlock;

            // load blocks
            var modelBlock = Block.Load<ModelBlockItem>(modelBlockFilename);
            var textureBlock = Block.Load<TextureBlockItem>(textureBlockFilename);

            // import
            var resourceImporter = new LeadphalanxObjResourceImporter(textureBlock);
            ModelObjImporter modelObjImporter = resourceImporter.ImportUpgradePlug3Obj();
            // TODO: fallback material 'Test_1024.png'

            // inject
            var modelInjector = new UpgradePartModelInjector(
                modelObjImporter.MeshGroup3064, modelBlock);
            modelInjector.Inject();

            // save blocks
            modelBlock.Save(modelBlockFilename);
            textureBlock.Save(textureBlockFilename);
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
