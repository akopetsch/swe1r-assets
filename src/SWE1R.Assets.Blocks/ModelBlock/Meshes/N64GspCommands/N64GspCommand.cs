// SPDX-License-Identifier: MIT

using ByteSerialization.Attributes;
using SWE1R.Assets.Blocks.ModelBlock.Meshes.Geometry;
using System.Collections.Generic;

namespace SWE1R.Assets.Blocks.ModelBlock.Meshes.VertexIndices
{
    /// <summary>
    /// See also:
    /// <list type="bullet">
    ///   <item>
    ///     <see href="http://n64devkit.square7.ch/header/gbi.htm#:~:text=Graphics%20Commands">
    ///       n64devkit.square7.ch - 'gbi.h' - 'Graphics Commands'</see></item>
    ///   <item>
    ///     <see href="https://github.com/akopetsch/Sw_Racer/blob/76c8ad9cea549ea18457846a135a7f25d48b3813/include/Swr_Model.h#L515">
    ///       github.com - akopetsch/Sw_Racer - 'SWR_MODEL_Section48'</see></item>
    /// </list>
    /// </summary>
    [Sizeof(8)]
    public abstract class N64GspCommand
    {
        #region Properties (serialized)

        [RecordTypeIdentifier((byte)01, typeof(N64GspVertexCommand))]
        [RecordTypeIdentifier((byte)03, typeof(N64GspCullDisplayListCommand))]
        [RecordTypeIdentifier((byte)05, typeof(N64Gsp1TriangleCommand))]
        [RecordTypeIdentifier((byte)06, typeof(N64Gsp2TrianglesCommand))]
        [Order(0)]
        public byte Tag { get; set; }

        #endregion

        #region Properties (abstraction)

        public abstract IEnumerable<int> Indices { get; }

        public abstract IEnumerable<Triangle> Triangles { get; }

        #endregion
    }
}
