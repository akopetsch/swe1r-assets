﻿// SPDX-License-Identifier: MIT

using SWE1R.Assets.Blocks.ModelBlock.Types;

namespace SWE1R.Assets.Blocks.Original.Tests.Format.Testers.ModelBlock.Models
{
    public class ScenModelFormatTester : ModelFormatTester<ScenModel>
    {
        public override void Test()
        {
            base.Test();

            Assert.True(Value.Nodes.Count == 83 || Value.Nodes.Count == 89);
            Assert.True(Value.Nodes.Count == 83 ?
                    Value.Data.List.Select(d => d.Integer.Value).Count() == 6 :
                    Value.Data == null);
            Assert.True(Value.Animations.Count >= 2 && Value.Animations.Count <= 126);
            Assert.True(Value.AltN == null);
        }
    }
}