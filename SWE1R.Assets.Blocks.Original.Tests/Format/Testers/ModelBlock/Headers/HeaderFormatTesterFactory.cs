// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization.Nodes;
using SWE1R.Assets.Blocks.ModelBlock;
using SWE1R.Assets.Blocks.ModelBlock.Types;

namespace SWE1R.Assets.Blocks.Original.Tests.Format.Testers.ModelBlock.Headers
{
    public class HeaderFormatTesterFactory
    {
        public ITester Get(Header value, Graph byteSerializationGraph, AnalyticsFixture analyticsFixture)
        {
            if (value is MAltHeader mAltHeader)
                return new MAltFormatTester(mAltHeader, byteSerializationGraph, analyticsFixture);
            if (value is ModlHeader modlHeader)
                return new ModlFormatTester(modlHeader, byteSerializationGraph, analyticsFixture);
            if (value is PartHeader partHeader)
                return new PartFormatTester(partHeader, byteSerializationGraph, analyticsFixture);
            if (value is PoddHeader poddHeader)
                return new PoddFormatTester(poddHeader, byteSerializationGraph, analyticsFixture);
            if (value is PuppHeader puppHeader)
                return new PuppFormatTester(puppHeader, byteSerializationGraph, analyticsFixture);
            if (value is ScenHeader scenHeader)
                return new ScenFormatTester(scenHeader, byteSerializationGraph, analyticsFixture);
            if (value is TrakHeader trakHeader)
                return new TrakFormatTester(trakHeader, byteSerializationGraph, analyticsFixture);
            return null;
        }
    }
}
