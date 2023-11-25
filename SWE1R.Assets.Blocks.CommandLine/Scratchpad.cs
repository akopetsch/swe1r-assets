// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using SWE1R.Assets.Blocks.CommandLine.Mods;
using System.Diagnostics;

namespace SWE1R.Assets.Blocks.CommandLine
{
    public class Scratchpad
    {
        public Scratchpad()
        {
            RestoreBlockFileBackups();

            //var mod = new Model_115_ObjImport();
            var mod = new Model_170_ObjImport();
            mod.Run();
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
