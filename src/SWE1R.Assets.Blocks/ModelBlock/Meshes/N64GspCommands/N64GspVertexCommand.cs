// SPDX-License-Identifier: MIT

using ByteSerialization.Attributes;
using System;
using System.Collections.Generic;

namespace SWE1R.Assets.Blocks.ModelBlock.Meshes.N64GspCommands
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

        private const int NPadding = 4;

        #endregion

        #region Properties (serialized)

        [Order(0)]
        private short NPadded { get; set; } // TODO: implement BitFieldAttribute in BinarySerialization
        [Order(1)]
        private byte V0PlusNPadded { get; set; }
        [Order(2)]
        internal ReferenceByIndex<Vertex> V { get; set; }

        #endregion

        #region Properties (C struct)

        public byte N { get => Convert.ToByte(NPadded >> NPadding); set => NPadded = (short)(value << NPadding); }
        public byte V0 { get => Convert.ToByte(V.Index); set => V.Index = value; }
        public byte V0PlusN { get => (byte)(V0PlusNPadded >> 1); set => V0PlusNPadded = Convert.ToByte(value << 1); }

        #endregion

        #region Constructor

        public N64GspVertexCommand() : 
            base(N64GspCommandByte.G_VTX)
        { }

        public N64GspVertexCommand(int n, int v0PlusN, int v0, IList<Vertex> vertices) :
            this()
        {
            V = new ReferenceByIndex<Vertex>()
            {
                Collection = vertices,
            };

            N = Convert.ToByte(n);
            V0 = Convert.ToByte(v0);
            V0PlusN = Convert.ToByte(v0PlusN);
        }

        #endregion

        #region Methods (: object)

        public override string ToString() =>
            GetString(
                new PropertyNameAndValue(nameof(N), N),
                new PropertyNameAndValue(nameof(V0), V0));

        #endregion
    }
}
