// SPDX-License-Identifier: MIT

using ByteSerialization.Attributes;
using System;

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
        public byte V0PlusN { get; set; }
        [Order(2)]
        public ReferenceByIndex<Vertex> V { get; set; }

        #endregion

        #region Properties (abstraction)

        public byte N
        {
            get => Convert.ToByte(NPadded >> NPadding);
            set => NPadded = (short)(value << NPadding);
        }

        #endregion

        #region Constructor

        public N64GspVertexCommand() : 
            base(N64GspCommandByte.G_VTX)
        { }

        #endregion

        #region Methods (: object)

        public override string ToString() =>
            GetString(
                new PropertyNameAndValue(nameof(N), N));

        #endregion
    }
}
