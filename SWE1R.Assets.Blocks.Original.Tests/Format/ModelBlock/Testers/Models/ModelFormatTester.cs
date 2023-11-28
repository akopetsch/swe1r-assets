// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization.Extensions;
using SWE1R.Assets.Blocks.ModelBlock;
using SWE1R.Assets.Blocks.Original.Tests.Format.Testers;

namespace SWE1R.Assets.Blocks.Original.Tests.Format.ModelBlock.Testers.Models
{
    public abstract class ModelFormatTester<TModel> : Tester<TModel> where TModel : Model
    {
        public override void Test()
        {
            if (Value.Animations != null)
            {
                // Anims does not contain null
                Assert.True(!Value.Animations.Contains(null));

                // Anims only contains distinct values
                Assert.True(Value.Animations.AllUnique());
            }
            if (Value.AltN != null)
                // AltN does not contain null
                Assert.True(!Value.AltN.Contains(null));
        }
    }
}
