// SPDX-License-Identifier: MIT

using ByteSerialization.Attributes;
using System;
using System.Collections.Generic;

namespace SWE1R.Assets.Blocks.ModelBlock.F3DEX2
{
    /// <summary>
    /// See also:
    /// <list type="bullet">
    ///   <item>
    ///     <see href="https://ultra64.ca/files/documentation/online-manuals/man/n64man/gsp/gSPVertex.html">
    ///       ultra64.ca - 'Online Manuals (OS 2.0J)' - 'gSPVertex'</see></item>
    ///   <item>
    ///     <see href="https://ultra64.ca/files/documentation/online-manuals/man/header/gbi.htm#:~:text=gSPVertex">
    ///       ultra64.ca - 'Online Manuals (OS 2.0J)' - gbi.h - gSPVertex</see></item>
    /// </list>
    /// </summary>
    public class GspVertexCommand : GraphicsCommand
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

        public GspVertexCommand() :
            base(GraphicsCommandByte.G_VTX)
        { }

        public GspVertexCommand(int n, int v0PlusN, int v0, IList<Vtx> vertices) :
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
