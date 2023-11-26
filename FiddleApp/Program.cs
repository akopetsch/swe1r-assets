// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using SWE1R.Assets.Blocks.SpriteBlock;
using SWE1R.Assets.Blocks.Utils;
using System.Diagnostics;

namespace FiddleApp
{
    public class Program
    {
        public static int Main(string[] args)
        {
            int result = Fiddle();
            if (Debugger.IsAttached)
                ConsoleUtil.PromptExit();
            return result;

        }

        private static int Fiddle()
        {
            //new RoslynCodeGenerationExample();
            new ByteSerializerSqlFiddle(typeof(Sprite));
            return ExitCodes.Success;
        }
    }
}
