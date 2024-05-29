// SPDX-License-Identifier: GPL-2.0-only

using System.Collections.Generic;
using System.Linq;

namespace SWE1R.Assets.Blocks.ModelBlock.Meshes.Geometry
{
    public class TriangleFan : Primitive
    {
        #region Properties

        public List<int> Indices { get; set; }

        #endregion

        #region Constructor

        public TriangleFan(IEnumerable<int> indices) =>
            Indices = indices.ToList();

        #endregion

        #region Methods

        public override IEnumerable<int> GetIndices() =>
            Indices;

        public override IEnumerable<Triangle> GetTriangles()
        {
            int i0 = Indices[0];
            for (int i = 0; i < Indices.Count - 2; i++)
            {
                int i1 = Indices[i + 1];
                int i2 = Indices[i + 2];
                yield return new Triangle(i0, i1, i2);
            }
        }

        #endregion
    }
}
