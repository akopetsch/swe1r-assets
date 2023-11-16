// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization.Nodes;
using SWE1R.Assets.Blocks.ModelBlock.Meshes;

namespace SWE1R.Assets.Blocks.Original.Tests.Format.Testers.ModelBlock.Meshes
{
    public class MaterialTextureTester : Tester<MaterialTexture>
    {
        public MaterialTextureTester(
            MaterialTexture value, Graph byteSerializationGraph, AnalyticsFixture analyticsFixture) : 
            base(value, byteSerializationGraph, analyticsFixture)
        { }

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
