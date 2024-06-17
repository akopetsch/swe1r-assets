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
    ///     <see href="http://n64devkit.square7.ch/n64man/gsp/gSPCullDisplayList.htm">
    ///       n64devkit.square7.ch - 'gSPCullDisplayList'</see></item>
    /// </list>
    /// </summary>
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

        #region Properties (C struct)

        public byte V0 { get => (byte)(V0Padded >> 1); set => V0Padded = Convert.ToByte(value << 1); }
        public byte VN { get => (byte)(VNPadded >> 1); set => VNPadded = Convert.ToByte(value << 1); }

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

        #region Methods (: object)

        public override string ToString() =>
            GetString(
                new PropertyNameAndValue(nameof(V0), V0),
                new PropertyNameAndValue(nameof(VN), VN));

        #endregion
    }
}
