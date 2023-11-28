// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization.Nodes;
using SWE1R.Assets.Blocks.ModelBlock;
using SWE1R.Assets.Blocks.ModelBlock.Types;
using SWE1R.Assets.Blocks.Original.Tests.Format.Testers;

namespace SWE1R.Assets.Blocks.Original.Tests.Format.ModelBlock.Testers.Models
{
    public class ModelFormatTesterFactory
    {
        public ITester Get(Model value, ByteSerializerGraph byteSerializationGraph, AnalyticsFixture analyticsFixture)
        {
            if (value is MAltModel mAltHeader)
            {
                var tester = new MAltModelFormatTester();
                tester.Init(mAltHeader, byteSerializationGraph, analyticsFixture);
                return tester;
            }
            if (value is ModlModel modlHeader)
            {
                var tester = new ModlModelFormatTester();
                tester.Init(modlHeader, byteSerializationGraph, analyticsFixture);
                return tester;
            }
            if (value is PartModel partHeader)
            {
                var tester = new PartModelFormatTester();
                tester.Init(partHeader, byteSerializationGraph, analyticsFixture);
                return tester;
            }
            if (value is PoddModel poddHeader)
            {
                var tester = new PoddModelFormatTester();
                tester.Init(poddHeader, byteSerializationGraph, analyticsFixture);
                return tester;
            }
            if (value is PuppModel puppHeader)
            {
                var tester = new PuppModelFormatTester();
                tester.Init(puppHeader, byteSerializationGraph, analyticsFixture);
                return tester;
            }
            if (value is ScenModel scenHeader)
            {
                var tester = new ScenModelFormatTester();
                tester.Init(scenHeader, byteSerializationGraph, analyticsFixture);
                return tester;
            }
            if (value is TrakModel trakHeader)
            {
                var tester = new TrakModelFormatTester();
                tester.Init(trakHeader, byteSerializationGraph, analyticsFixture);
                return tester;
            }
            return null;
        }
    }
}
