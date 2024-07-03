// SPDX-License-Identifier: MIT

using ByteSerialization;
using ByteSerialization.Attributes;
using ByteSerialization.IO;
using System;

namespace SWE1R.Assets.Blocks.ModelBlock.F3DEX2
{
    /// <summary>
    /// See also:
    /// <list type="bullet">
    ///   <item>
    ///     <see href="https://ultra64.ca/files/documentation/online-manuals/man/n64man/gsp/gSPCullDisplayList.html">
    ///       ultra64.ca - 'Online Manuals (OS 2.0J)' - 'gSPCullDisplayList'</see></item>
    ///   <item>
    ///     <see href="https://ultra64.ca/files/documentation/online-manuals/man/header/gbi.htm#:~:text=gSPCullDisplayList">
    ///       ultra64.ca - 'Online Manuals (OS 2.0J)' - gbi.h - gSPCullDisplayList</see></item>
    /// </list>
    /// </summary>
    [MacroName("gSPCullDisplayList")]
    public class GspCullDisplayListCommand : GraphicsCommand, ICustomSerializable
    {
        #region Fields

        private static readonly byte[] PaddingBytes1 = new byte[2];
        private static readonly byte[] PaddingBytes2 = new byte[3];

        #endregion

        #region Properties (serialized)

        [Order(0), Offset(3)]
        private byte V0Padded { get; set; }
        [Order(1), Offset(7)]
        private byte VNPadded { get; set; }

        #endregion

        #region Properties (C macro)

        public byte V0 { get => (byte)(V0Padded >> 1); set => V0Padded = Convert.ToByte(value << 1); }
        public byte VN { get => (byte)(VNPadded >> 1); set => VNPadded = Convert.ToByte(value << 1); }

        #endregion

        #region Properties (: GraphicsCommand)

        protected override object[] MacroArguments => 
            new object[] { V0, VN };

        #endregion

        #region Constructor

        public GspCullDisplayListCommand() :
            base(GraphicsCommandByte.G_CULLDL)
        { }

        public GspCullDisplayListCommand(byte v0, byte vn) :
            this()
        {
            V0 = v0;
            VN = vn;
        }

        #endregion

        #region Methods (: ICustomSerializable)

        public override void Serialize(EndianBinaryWriter writer)
        {
            base.Serialize(writer);
            writer.Write(PaddingBytes1);
            writer.Write(V0Padded);
            writer.Write(PaddingBytes2);
            writer.Write(VNPadded);
        }

        public override void Deserialize(EndianBinaryReader reader)
        {
            // TODO: not called
            base.Deserialize(reader);
            reader.Read<byte>(PaddingBytes1.Length);
            V0Padded = reader.ReadByte();
            reader.Read<byte>(PaddingBytes2.Length);
            VNPadded = reader.ReadByte();
        }

        #endregion
    }
}
