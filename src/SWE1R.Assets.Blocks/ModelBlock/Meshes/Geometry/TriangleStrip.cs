// Copyright 2024 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using System.Collections.Generic;
using System.Linq;

namespace SWE1R.Assets.Blocks.ModelBlock.Meshes.Geometry
{
    public class TriangleStrip : Primitive
    {
        #region Properties

        public List<int> Indices { get; set; }

        #endregion

        #region Constructor

        public TriangleStrip(IEnumerable<int> indices) =>
            Indices = indices.ToList();

        #endregion

        #region Methods

        public override IEnumerable<int> GetIndices() => 
            Indices;

        public override IEnumerable<Triangle> GetTriangles()
        {
            for (int i = 0; i < Indices.Count - 2; i++)
            {
                int[] triangle = i % 2 == 0 ?
                    new int[] { Indices[i + 0], Indices[i + 1], Indices[i + 2] } :
                    new int[] { Indices[i + 2], Indices[i + 1], Indices[i + 0] };
                yield return new Triangle(triangle);
            }
        }

        #endregion
    }
}
