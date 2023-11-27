// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using SWE1R.Assets.Blocks.ModelBlock.Types;

namespace SWE1R.Assets.Blocks.Original.Tests.Format.ModelBlock.Testers.Headers
{
    public class ModlFormatTester : ModelKindFormatTester<ModlModel>
    {
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
