// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

namespace SWE1R.Assets.Blocks.TestUtils
{
    public class BlockItemsConsoleTestHelper
    {
        public static void PrintItemIndex(int index) =>
            Console.Write($"{BlockItem.GetIndexString(index)} ");

        public static void PrintItemName(string name) =>
            Console.Write($"{name} ");

        public static void PrintStep() =>
            Console.Write(".");

        public static void PrintTestResult(bool result) =>
            Console.Write(result ? "OK" : "FAIL");
    }
}
