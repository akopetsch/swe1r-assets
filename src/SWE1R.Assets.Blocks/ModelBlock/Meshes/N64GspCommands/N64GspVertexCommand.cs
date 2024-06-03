// SPDX-License-Identifier: MIT

using ByteSerialization.Attributes;
using System;

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

        private const int VerticesCountPadding = 4;

        #endregion

        #region Properties (serialized)

        [Order(0)]
        private short VerticesCountPadded { get; set; } // TODO: implement BitFieldAttribute in BinarySerialization
        [Order(1)]
        public byte V0PlusN { get; set; }
        [Order(2)]
        public ReferenceByIndex<Vertex> V { get; set; }

        #endregion

        #region Properties (abstraction)

        public byte VerticesCount
        {
            get => Convert.ToByte(VerticesCountPadded >> VerticesCountPadding);
            set => VerticesCountPadded = (short)(value << VerticesCountPadding);
        }

        #endregion

        #region Constructor

        public N64GspVertexCommand() => 
            Byte = 1;

        #endregion

        #region Methods (: object)

        public override string ToString() =>
            $"({Byte} " +
            $"{nameof(VerticesCount)} = {VerticesCount}, " +
            $"{nameof(V0PlusN)} = {V0PlusN}, " +
            $"{nameof(V)} = {V})";

        #endregion
    }
}
