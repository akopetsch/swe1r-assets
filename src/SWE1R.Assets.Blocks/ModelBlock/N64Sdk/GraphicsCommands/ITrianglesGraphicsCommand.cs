// SPDX-License-Identifier: MIT

using SWE1R.Assets.Blocks.ModelBlock.Meshes.Geometry;
using System.Collections.Generic;

namespace SWE1R.Assets.Blocks.ModelBlock.N64Sdk.GraphicsCommands
{
    public interface ITrianglesGraphicsCommand
    {
        #region Properties (abstraction)

        GraphicsCommandByte Byte { get; }

        IEnumerable<byte> Indices { get; }

        IEnumerable<Triangle> Triangles { get; }

        #endregion
    }
}
