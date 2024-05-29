// SPDX-License-Identifier: GPL-2.0-only

using SWE1R.Assets.Blocks.Original.MaterialTexturesCatalog;
using SWE1R.Assets.Blocks.Utils;
using System.Diagnostics;

namespace SWE1R.Assets.Blocks.Original.CommandLine
{
    public class Program
    {
        public static int Main(string[] args)
        {
            int result = GenerateOriginalMaterialTexturesCatalog();
            if (Debugger.IsAttached)
                ConsoleUtil.PromptExit();
            return result;

        }

        private static int GenerateOriginalMaterialTexturesCatalog()
        {
            var generator = new OriginalMaterialTexturesCatalogGenerator();
            generator.Generate();
            return ExitCodes.Success;
        }
    }
}
