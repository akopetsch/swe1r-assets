﻿// SPDX-License-Identifier: MIT

using SWE1R.Assets.Blocks.ModelBlock.Types;

namespace SWE1R.Assets.Blocks.Original.Tests.Format.Testers.ModelBlock.Models
{
    public class PuppModelFormatTester : ModelFormatTester<PuppModel>
    {
        public override void Test()
        {
            base.Test();

            Assert.True(Value.Nodes.Count == 9);
            Assert.True(Value.Data == null);
            Assert.True(
                Value.Animations.Count >= 3 &&
                Value.Animations.Count <= 33);
            Assert.True(Value.AltN == null);
        }
    }
}