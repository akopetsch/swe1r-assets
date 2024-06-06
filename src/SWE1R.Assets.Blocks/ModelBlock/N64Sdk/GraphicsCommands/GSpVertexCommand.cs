// SPDX-License-Identifier: MIT

using ByteSerialization.Attributes;
using System;
using System.Collections.Generic;

namespace SWE1R.Assets.Blocks.ModelBlock.N64Sdk.GraphicsCommands
{
    /// <summary>
    /// See also:
    /// <list type="bullet">
    ///   <item>
    ///     <see href="http://n64devkit.square7.ch/n64man/gsp/gSPVertex.htm">
    ///       n64devkit.square7.ch - 'gSPVertex'</see></item>
    /// </list>
    /// </summary>
    public class GSpVertexCommand : GraphicsCommand
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
        internal ReferenceByIndex<Vtx> V { get; set; }

        #endregion

        #region Properties (C struct)

        public byte N { get => Convert.ToByte(NPadded >> NPadding); set => NPadded = (short)(value << NPadding); }
        public int V0 { get => V.Index.Value; set => V.Index = value; }
        public byte V0PlusN { get => (byte)(V0PlusNPadded >> 1); set => V0PlusNPadded = Convert.ToByte(value << 1); }

        #endregion

        #region Constructor

        public GSpVertexCommand() :
            base(GraphicsCommandByte.G_VTX)
        { }

        public GSpVertexCommand(int n, int v0PlusN, int v0, IList<Vtx> vertices) :
            this()
        {
            V = new ReferenceByIndex<Vtx>()
            {
                Collection = vertices,
            };

            N = Convert.ToByte(n);
            V0 = v0;
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
