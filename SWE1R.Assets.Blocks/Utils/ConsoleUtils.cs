// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using System;

namespace SWE1R.Assets.Blocks.Utils
{
    public static class ConsoleUtils
    {
        public static void PromptExit()
        {
            Console.Write("Press enter to exit.");
            while (Console.ReadKey().Key != ConsoleKey.Enter) ;
            Console.WriteLine();
        }
    }
}
