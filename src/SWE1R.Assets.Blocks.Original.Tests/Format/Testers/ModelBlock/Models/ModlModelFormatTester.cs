﻿// SPDX-License-Identifier: MIT

using SWE1R.Assets.Blocks.ModelBlock.Types;

namespace SWE1R.Assets.Blocks.Original.Tests.Format.Testers.ModelBlock.Models
{
    public class ModlModelFormatTester : ModelFormatTester<ModlModel>
    {
        public override void Test()
        {
            base.Test();

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