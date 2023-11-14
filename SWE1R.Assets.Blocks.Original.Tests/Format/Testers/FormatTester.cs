﻿// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization.Nodes;
using SWE1R.Assets.Blocks.ModelBlock;

namespace SWE1R.Assets.Blocks.Original.Tests.Format.Testers
{
    public abstract class FormatTester<THeader> : IFormatTester where THeader : Header
    {
        public THeader Header { get; }

        public FormatTester(THeader header) =>
            Header = header;

        public abstract void Test(Graph byteSerializerGraph);
    }
}
