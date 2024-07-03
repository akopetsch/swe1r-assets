// SPDX-License-Identifier: MIT

using SWE1R.Assets.Blocks.Utils;
using System.Diagnostics;

namespace SWE1R.Assets.Blocks.Original.SQLite.CommandLine
{
    public class Program
    {
        public static int Main(string[] args)
        {
            int result = GenerateDatabase();
            if (Debugger.IsAttached)
                ConsoleUtil.PromptExit();
            return result;
        }

        private static int GenerateDatabase()
        {
            using AssetsDbContext assetsDbContext = new();
            var generator = new AssetsDbGenerator(assetsDbContext);
            generator.Generate();
            return ExitCodes.Success;
        }
    }
}
