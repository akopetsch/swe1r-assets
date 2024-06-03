// SPDX-License-Identifier: MIT

using SWE1R.Assets.Blocks.ModelBlock.Meshes.Geometry;
using System.Collections.Generic;

namespace SWE1R.Assets.Blocks.ModelBlock.Meshes.N64GspCommands
{
    public interface IN64GspTrianglesCommand
    {
        #region Properties (abstraction)

        N64GspCommandByte Byte { get; }

        IEnumerable<int> Indices { get; }

        IEnumerable<Triangle> Triangles { get; }

        #endregion
    }
}
