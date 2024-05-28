// Copyright 2024 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using SWE1R.Assets.Blocks.TestUtils;
using Xunit.Abstractions;

namespace SWE1R.Assets.Blocks.Original.Tests.Reserialization
{
    public abstract class BlockItemsReserializationXUnitTestBase<TItem> : 
        BlockItemsReserializationTestBase<TItem>,  // TODO: !!! merge with base class (or not because of Unity)
        IClassFixture<OriginalBlocksProviderFixture>
        where TItem : BlockItem, new()
    {
        #region Properties

        protected OriginalBlocksProviderFixture OriginalBlocksProviderFixture { get; }
        protected ITestOutputHelper Output { get; }

        #endregion

        #region Constructor

        protected BlockItemsReserializationXUnitTestBase(
            OriginalBlocksProviderFixture originalBlocksProviderFixture, 
            ITestOutputHelper output) : 
            base()
        {
            OriginalBlocksProviderFixture = originalBlocksProviderFixture;
            Output = output;
        }

        #endregion

        #region Methods

        protected override TItem GetItem(int valueId) =>
            OriginalBlocksProviderFixture.Provider.GetFirstBlockItemByValueId<TItem>(valueId);

        protected override void PrintItemValueId(int valueId) { }

        protected override void PrintItemName(string nameString) =>
            Output.WriteLine(nameString);

        protected override void PrintItemDone() { }

        protected override void AssertFail(string userMessage) => 
            Assert.Fail(userMessage);

        #endregion
    }
}
