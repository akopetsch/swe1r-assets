// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using System.Collections.Generic;

namespace SWE1R.Assets.Blocks.ModelBlock.Meshes.Geometry
{
    public class Quad : Primitive
    {
        #region Properties

        public int I0 { get; set; }
        public int I1 { get; set; }
        public int I2 { get; set; }
        public int I3 { get; set; }

        #endregion

        #region Constructor

        public Quad(int[] indices)
        {
            I0 = indices[0];
            I1 = indices[1];
            I2 = indices[2];
            I3 = indices[3];
        }

        #endregion

        #region Methods

        public override IEnumerable<int> GetIndices()
        {
            yield return I0;
            yield return I1;
            yield return I2;
            yield return I3;
        }

        public override IEnumerable<Triangle> GetTriangles()
        {
            yield return new Triangle(I0, I1, I2);
            yield return new Triangle(I2, I3, I0);
        }

        #endregion
    }
}
