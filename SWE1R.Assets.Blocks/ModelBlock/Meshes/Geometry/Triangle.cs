// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using System.Collections.Generic;

namespace SWE1R.Assets.Blocks.ModelBlock.Meshes.Geometry
{
    /// <summary>
    /// Represents a triangle defined by the indices of its vertices which are arranged counter-clockwise.
    /// </summary>
    public class Triangle
    {
        #region Properties

        public int I0 { get; set; }
        public int I1 { get; set; }
        public int I2 { get; set; }

        public IEnumerable<int> Indices
        {
            get
            {
                yield return I0;
                yield return I1;
                yield return I2;
            }
        }

        #endregion

        #region Constructor

        public Triangle(int i0, int i1, int i2)
        {
            I0 = i0;
            I1 = i1;
            I2 = i2;
        }

        public Triangle(int[] indices)
        {
            I0 = indices[0];
            I1 = indices[1];
            I2 = indices[2];
        }

        #endregion

        #region Methods

        public void AddToIndices(int summand)
        {
            I0 += summand;
            I1 += summand;
            I2 += summand;
        }

        public void DivideIndicesBy(int divisor)
        {
            I0 /= divisor;
            I1 /= divisor;
            I2 /= divisor;
        }

        #endregion

        #region Methods (: object)

        public override string ToString() =>
            $"({I0}, {I1}, {I2})";

        #endregion
    }
}
