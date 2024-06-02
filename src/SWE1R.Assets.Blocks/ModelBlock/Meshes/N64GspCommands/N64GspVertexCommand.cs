// SPDX-License-Identifier: MIT

using ByteSerialization.Attributes;
using SWE1R.Assets.Blocks.ModelBlock.Meshes.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SWE1R.Assets.Blocks.ModelBlock.Meshes.VertexIndices
{
    /// <summary>
    /// See also:
    /// <list type="bullet">
    ///   <item>
    ///     <see href="http://n64devkit.square7.ch/n64man/gsp/gSPVertex.htm">
    ///       n64devkit.square7.ch - 'gSPVertex'</see></item>
    /// </list>
    /// </summary>
    public class N64GspVertexCommand : N64GspCommand
    {
        #region Fields (const)

        internal const int VerticesCountPadding = 4;

        #endregion

        #region Properties (serialized)

        [Order(0)]
        internal short VerticesCountPadded { get; set; } // TODO: implement BitFieldAttribute in BinarySerialization
        [Order(1)]
        public byte NextIndicesBase { get; set; } // v0_plus_n
        [Order(2)]
        public ReferenceByIndex<Vertex> StartVertex { get; set; }

        #endregion

        #region Properties (abstraction)

        public byte VerticesCount
        {
            get => Convert.ToByte(VerticesCountPadded >> VerticesCountPadding);
            set => VerticesCountPadded = (short)(value << VerticesCountPadding);
        }

        public override IEnumerable<int> Indices => 
            Enumerable.Empty<int>();

        public override IEnumerable<Triangle> Triangles => 
            Enumerable.Empty<Triangle>();

        #endregion

        #region Constructor

        public N64GspVertexCommand() => 
            Byte = 1;

        #endregion

        #region Methods (: object)

        public override string ToString() =>
            $"({Byte} " +
            $"{nameof(VerticesCount)} = {VerticesCount}, " +
            $"{nameof(NextIndicesBase)} = {NextIndicesBase}, " +
            $"{nameof(StartVertex)} = {StartVertex})";

        #endregion
    }
}
