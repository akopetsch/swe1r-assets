// SPDX-License-Identifier: MIT

using SWE1R.Assets.Blocks.ModelBlock.Nodes;
using SWE1R.Assets.Blocks.Vectors;

namespace SWE1R.Assets.Blocks.Original.Tests.Format.Testers.ModelBlock.Nodes
{
    public class MeshGroupNodeTester : Tester<MeshGroupNode>
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
