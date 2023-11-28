// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using SWE1R.Assets.Blocks.TestUtils;
using Xunit.Abstractions;

namespace SWE1R.Assets.Blocks.Original.Tests.Reserialization
{
    public abstract class BlockItemsReserializationXUnitTest<TItem> : 
        BlockItemsReserializationTestBase<TItem> where TItem : BlockItem, new() // TODO: !!! merge with base class (or not because of Unity)
    {
        #region Fields

        private readonly ITestOutputHelper _output;

        #endregion

        #region Constructor

        protected BlockItemsReserializationXUnitTest(ITestOutputHelper output, string blockIdName) : 
            base(new OriginalBlockProvider().LoadBlock<TItem>(blockIdName))
        {
            _output = output;
        }

        #endregion

        #region Methods

        protected override void PrintItemIndex(int index) { }

        protected override void PrintItemName(string nameString) =>
            _output.WriteLine(nameString);

        protected override void PrintItemDone() { }

        protected override void AssertEquality(int p, bool areEqual) =>
            Assert.True(areEqual, $"{nameof(p)} = {p}");

        #endregion
    }
}
