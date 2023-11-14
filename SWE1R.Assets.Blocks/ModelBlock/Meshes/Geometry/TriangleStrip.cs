// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using System.Collections.Generic;

namespace SWE1R.Assets.Blocks.ModelBlock.Meshes.Geometry
{
    public class TriangleStrip
    {
        public int[] Indices { get; set; }

        public IEnumerable<Triangle> Triangles
        {
            get
            {
                for (int i = 0; i < Indices.Length - 2; i++)
                {
                    int[] triangle = i % 2 == 0 ?
                        new int[] { Indices[i + 0], Indices[i + 1], Indices[i + 2] } :
                        new int[] { Indices[i + 2], Indices[i + 1], Indices[i + 0] };
                    yield return new Triangle(triangle);
                }
            }
        }

        public TriangleStrip(int[] indices) =>
            Indices = indices;

        public override string ToString() =>
            $"({string.Join(", ", Indices)})";
    }
}
