// SPDX-License-Identifier: GPL-2.0-only

using System.Collections.Generic;

namespace SWE1R.Assets.Blocks.ModelBlock.Meshes.Geometry
{
    public abstract class Primitive
    {
        public abstract IEnumerable<int> GetIndices();

        public abstract IEnumerable<Triangle> GetTriangles();

        public override string ToString() =>
            $"({string.Join(", ", GetIndices())})";
    }
}
