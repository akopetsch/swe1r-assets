﻿// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization.Nodes;
using SWE1R.Assets.Blocks.ModelBlock.Types;

namespace SWE1R.Assets.Blocks.Original.Tests.Format.Testers.ModelBlock.Headers
{
    public class ModlFormatTester : HeaderFormatTester<ModlHeader>
    {
        public ModlFormatTester(ModlHeader value, Graph byterSerializationGraph, AnalyticsFixture analyticsFixture) : 
            base(value, byterSerializationGraph, analyticsFixture)
        { }

        public override void Test()
        {
            Assert.True(Value.Nodes.Count == 1);
            Assert.True(Value.Data == null);
            Assert.True(
                Value.Animations == null ||
                Value.Animations.Count == 1 ||
                Value.Animations.Count == 2 ||
                Value.Animations.Count == 3);
            Assert.True(Value.AltN == null);
        }
    }
}
