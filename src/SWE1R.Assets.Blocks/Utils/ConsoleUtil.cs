// SPDX-License-Identifier: GPL-2.0-only

using System;

namespace SWE1R.Assets.Blocks.Utils
{
    public static class ConsoleUtil
    {
        public static void PromptExit()
        {
            Console.Write("Press enter to exit.");
            while (Console.ReadKey().Key != ConsoleKey.Enter) ;
            Console.WriteLine();
        }
    }
}
