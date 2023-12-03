// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using SWE1R.Assets.Blocks.Utils;
using System.Diagnostics;

namespace SWE1R.Assets.Blocks.Original.SQLite.CommandLine
{
    public class Program
    {
        public static int Main(string[] args)
        {
            int result = ImportSprites();
            if (Debugger.IsAttached)
                ConsoleUtil.PromptExit();
            return result;
        }

        private static int ImportSprites()
        {
            using AssetsDbContext assetsDbContext = new();
            var generator = new AssetsDbGenerator(assetsDbContext);
            generator.Generate();
            return ExitCodes.Success;
        }
    }
}
