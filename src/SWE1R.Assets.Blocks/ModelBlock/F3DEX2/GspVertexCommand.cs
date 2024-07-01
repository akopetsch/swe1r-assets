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
        internal ReferenceByIndex<Vtx> V { get; set; } = new ReferenceByIndex<Vtx>();

        #endregion

        #region Properties (C macro)

        public byte N
        {
            get => Convert.ToByte(NPadded >> NPadding);
            set => NPadded = (short)(value << NPadding);
        }

        public byte V0
        {
            get => Convert.ToByte(V0PlusN - N);
            set => V0PlusN = Convert.ToByte(value + N);
        }

        #endregion

        #region Properties (helper)

        public byte V0PlusN
        {
            get => (byte)(V0PlusNPadded >> 1);
            set => V0PlusNPadded = Convert.ToByte(value << 1);
        }

        public IList<Vtx> Vertices
        {
            get => V.Collection;
            set => V.Collection = value;
        }

        public int VerticesStartIndex
        {
            get => V.Index.Value;
            set => V.Index = value;
        }

        #endregion

        #region Properties (: GraphicsCommand)

        protected override object[] MacroArguments => 
            new object[] { $"allVertices + {V.Index}", N, V0 }; // TODO: string literal

        #endregion

        #region Constructor

        public GspVertexCommand() :
            base(GraphicsCommandByte.G_VTX, "gSPVertex")
        { }

        public GspVertexCommand(IList<Vtx> vertices, int verticesStartIndex, int n, int v0PlusN) :
            this() // TODO: mimick C macro
        {
            N = Convert.ToByte(n);
            V0PlusN = Convert.ToByte(v0PlusN);
            V = new ReferenceByIndex<Vtx>()
            {
                Collection = vertices,
                Index = verticesStartIndex,
            };
        }

        #endregion
    }
}
