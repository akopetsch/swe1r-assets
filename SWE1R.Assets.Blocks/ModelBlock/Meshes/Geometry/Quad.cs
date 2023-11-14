// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using System.Collections.Generic;

namespace SWE1R.Assets.Blocks.ModelBlock.Meshes.Geometry
{
    public class Quad
    {
        public int I0 { get; set; }
        public int I1 { get; set; }
        public int I2 { get; set; }
        public int I4 { get; set; }

        public IEnumerable<Triangle> Triangles
        {
            get
            {
                yield return new Triangle(I0, I1, I2);
                yield return new Triangle(I2, I4, I0);
            }
        }

        public Quad(int[] indices)
        {
            I0 = indices[0];
            I1 = indices[1];
            I2 = indices[2];
            I4 = indices[3];
        }

        public override string ToString() =>
            $"({I0}, {I1}, {I2}, {I4})";
    }
}
