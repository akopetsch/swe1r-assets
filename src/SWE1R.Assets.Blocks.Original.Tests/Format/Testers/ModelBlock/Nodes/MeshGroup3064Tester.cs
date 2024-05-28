// Copyright 2024 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using SWE1R.Assets.Blocks.ModelBlock.Nodes;
using SWE1R.Assets.Blocks.Vectors;

namespace SWE1R.Assets.Blocks.Original.Tests.Format.Testers.ModelBlock.Nodes
{
    public class MeshGroup3064Tester : Tester<MeshGroup3064>
    {
        public override void Test()
        {
            AssertBounds();
        }

        private void AssertBounds()
        {
            if (Value.Meshes == null)
            {
                var nullBounds = new Bounds3Single(-1, -1, -1, -1, -1, -1);
                Assert.True(Value.Bounds.Equals(nullBounds));
            }
            else
            {
                Assert.True(Value.Bounds.IsValid);
                var computedBounds = new Bounds3Single(Value.Meshes.Select(x => x.FixedBounds).ToArray());
                Assert.True(Value.Bounds.Equals(computedBounds));
            }
        }
    }
}
