// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

namespace SWE1R.Assets.Blocks.TestUtils
{
    public class BlockItemReserializationConsoleTest<TItem> :
        BlockItemsReserializationTestBase<TItem> where TItem : BlockItem, new()
    {
        public BlockItemReserializationConsoleTest(Block<TItem> block) : 
            base(block) { }

        protected override void PrintItemIndex(int index) =>
            BlockItemsConsoleTestHelper.PrintItemIndex(index);

        protected override void PrintItemName(string name) =>
            BlockItemsConsoleTestHelper.PrintItemName(name);

        protected override void PrintItemDone() =>
            Console.WriteLine();

        protected override void AssertEquality(int p, bool areEqual)
        {
            Console.Write($"p{p}: ");
            BlockItemsConsoleTestHelper.PrintTestResult(areEqual);
            Console.Write(" ");
        }
    }
}
