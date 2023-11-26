// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using SWE1R.Assets.Blocks.ModelBlock.Types;

namespace SWE1R.Assets.Blocks.Original.Tests.Format.ModelBlock.Testers.Headers
{
    public class PuppFormatTester : HeaderFormatTester<PuppModel>
    {
        public override void Test()
        {
            Assert.True(Value.Nodes.Count == 9);
            Assert.True(Value.Data == null);
            Assert.True(
                Value.Animations.Count >= 3 &&
                Value.Animations.Count <= 33);
            Assert.True(Value.AltN == null);
        }
    }
}
