// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization.Nodes;

namespace SWE1R.Assets.Blocks.Original.Tests.Format.Testers
{
    public interface IFormatTester
    {
        void Test(Graph byteSerializerGraph);
    }
}
