// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization;
using SWE1R.Assets.Blocks.Original.Tests.Format.Testers;
using SWE1R.Assets.Blocks.TestUtils;
using System.Diagnostics;
using Xunit.Abstractions;

namespace SWE1R.Assets.Blocks.Original.Tests.Format
{
    public abstract class BlockItemsFormatTestBase<TBlockItem> : 
        BlockItemsTestBase<TBlockItem>, 
        IClassFixture<AnalyticsFixture>,
        IClassFixture<OriginalBlocksProviderFixture>
        where TBlockItem : BlockItem, new()
    {
        #region Properties

        protected AnalyticsFixture AnalyticsFixture { get; }
        protected OriginalBlocksProviderFixture OriginalBlocksProviderFixture { get; }
        protected ITestOutputHelper Output { get; }

        #endregion

        #region Constructor

        public BlockItemsFormatTestBase(
            AnalyticsFixture analyticsFixture, 
            OriginalBlocksProviderFixture originalBlocksProviderFixture, 
            ITestOutputHelper output) :
            base()
        {
            AnalyticsFixture = analyticsFixture;
            OriginalBlocksProviderFixture = originalBlocksProviderFixture;
            Output = output;
        }

        #endregion

        #region Methods (: BlockItemsTestBase)

        protected override TBlockItem GetItem(int valueId) =>
            OriginalBlocksProviderFixture.Provider.GetFirstBlockItemByValueId<TBlockItem>(valueId);

        protected override void PrintItemValueId(int index) =>
            Debug.WriteLine(BlockItem.GetIndexString(index));

        protected override void PrintItemName(string nameString) =>
            Output.WriteLine(nameString);

        protected override void PrintItemDone()
        {
            Debug.WriteLine(string.Empty);
            Debug.WriteLine(string.Empty);
        }

        #endregion

        #region Methods

        protected void RunTesters<TValue, TTester>(ByteSerializerContext byteSerializerContext)
            where TTester : Tester<TValue>, new()
        {
            var values = byteSerializerContext.Graph.GetValues<TValue>().ToList();
            foreach (TValue value in values)
            {
                var tester = new TTester();
                tester.Init(value, byteSerializerContext.Graph, AnalyticsFixture);
                tester.Test();
            }
        }

        #endregion
    }
}
