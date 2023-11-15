// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization.Nodes;

namespace SWE1R.Assets.Blocks.Original.Tests.Format.Testers
{
    public abstract class Tester<TValue>
    {
        public TValue Value { get; }
        public Graph ByteSerializationGraph { get; }

        public Tester(TValue value, Graph byteSerializationGraph)
        {
            Value = value;
            ByteSerializationGraph = byteSerializationGraph;
        }

        public abstract void Test();
    }
}
