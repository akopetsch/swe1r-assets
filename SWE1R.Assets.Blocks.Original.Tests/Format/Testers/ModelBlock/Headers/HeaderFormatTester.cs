// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization.Nodes;
using SWE1R.Assets.Blocks.ModelBlock;

namespace SWE1R.Assets.Blocks.Original.Tests.Format.Testers.ModelBlock.Headers
{
    public abstract class HeaderFormatTester<THeader> : Tester<THeader> where THeader : Model
    {
        public HeaderFormatTester(THeader value, Graph byteSerializationGraph, AnalyticsFixture analyticsFixture) : 
            base(value, byteSerializationGraph, analyticsFixture)
        { }
    }
}
