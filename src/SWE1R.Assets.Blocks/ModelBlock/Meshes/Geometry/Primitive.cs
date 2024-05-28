// Copyright 2024 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

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
