﻿// SPDX-License-Identifier: MIT

using SWE1R.Assets.Blocks.ModelBlock.Materials;

namespace SWE1R.Assets.Blocks.Original.Tests.Format.Testers.ModelBlock.Materials
{
    public class MaterialTextureTester : Tester<MaterialTexture>
    {
        public override void Test()
        {
            // TODO: Mask_Unk
            Assert.Equal(Value.Width * 4, Value.Width4);
            Assert.Equal(Value.Height * 4, Value.Height4);
            Assert.Equal(0, Value.Always0_08);
            Assert.Equal(0, Value.Always0_0a);
            // TODO: ...
        }
    }
}