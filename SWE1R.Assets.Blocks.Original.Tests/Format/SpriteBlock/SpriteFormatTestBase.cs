// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization;
using SWE1R.Assets.Blocks.SpriteBlock;
using Xunit.Abstractions;

namespace SWE1R.Assets.Blocks.Original.Tests.Format.SpriteBlock
{
    public class SpriteFormatTestBase : BlockItemsFormatTestBase<SpriteBlockItem>
    {
        #region Constructor

        public SpriteFormatTestBase(AnalyticsFixture analyticsFixture, ITestOutputHelper output, string blockIdName) :
            base(analyticsFixture, output, blockIdName)
        { }

        #endregion

        #region Methods (: BlockItemsTestBase)

        protected override void CompareItemInternal(int index)
        {
            SpriteBlockItem modelBlockItem = DeserializeItem(index, out ByteSerializerContext context);

            
        }

        #endregion
    }
}
