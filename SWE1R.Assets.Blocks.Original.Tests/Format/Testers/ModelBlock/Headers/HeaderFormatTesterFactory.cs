// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using SWE1R.Assets.Blocks.ModelBlock;
using SWE1R.Assets.Blocks.ModelBlock.Types;

namespace SWE1R.Assets.Blocks.Original.Tests.Format.Testers.ModelBlock.Headers
{
    public class HeaderFormatTesterFactory
    {
        public IHeaderFormatTester Get(Header header)
        {
            if (header is MAltHeader mAltHeader)
                return new MAltFormatTester(mAltHeader);
            if (header is ModlHeader modlHeader)
                return new ModlFormatTester(modlHeader);
            if (header is PartHeader partHeader)
                return new PartFormatTester(partHeader);
            if (header is PoddHeader poddHeader)
                return new PoddFormatTester(poddHeader);
            if (header is PuppHeader puppHeader)
                return new PuppFormatTester(puppHeader);
            if (header is ScenHeader scenHeader)
                return new ScenFormatTester(scenHeader);
            if (header is TrakHeader trakHeader)
                return new TrakFormatTester(trakHeader);
            return null;
        }
    }
}
