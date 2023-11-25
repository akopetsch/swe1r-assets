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
        public ITester Get(Model value, Graph byteSerializationGraph, AnalyticsFixture analyticsFixture)
        {
            if (value is MAltModel mAltHeader)
                return new MAltFormatTester(mAltHeader, byteSerializationGraph, analyticsFixture);
            if (value is ModlModel modlHeader)
                return new ModlFormatTester(modlHeader, byteSerializationGraph, analyticsFixture);
            if (value is PartModel partHeader)
                return new PartFormatTester(partHeader, byteSerializationGraph, analyticsFixture);
            if (value is PoddModel poddHeader)
                return new PoddFormatTester(poddHeader, byteSerializationGraph, analyticsFixture);
            if (value is PuppModel puppHeader)
                return new PuppFormatTester(puppHeader, byteSerializationGraph, analyticsFixture);
            if (value is ScenModel scenHeader)
                return new ScenFormatTester(scenHeader, byteSerializationGraph, analyticsFixture);
            if (value is TrakModel trakHeader)
                return new TrakFormatTester(trakHeader, byteSerializationGraph, analyticsFixture);
            return null;
        }
    }
}
