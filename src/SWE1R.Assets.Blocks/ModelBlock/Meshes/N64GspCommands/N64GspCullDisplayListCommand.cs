﻿// SPDX-License-Identifier: MIT

using ByteSerialization;
using ByteSerialization.Attributes;
using ByteSerialization.IO;

namespace SWE1R.Assets.Blocks.ModelBlock.Meshes.N64GspCommands
{
    /// <summary>
    /// See also:
    /// <list type="bullet">
    ///   <item>
    ///     <see href="http://n64devkit.square7.ch/n64man/gsp/gSPCullDisplayList.htm">
    ///       n64devkit.square7.ch - 'gSPCullDisplayList'</see></item>
    /// </list>
    /// </summary>
    public class N64GspCullDisplayListCommand : N64GspCommand, ICustomSerializable
    {
        #region Fields

        private static readonly byte[] PaddingBytes = new byte[6];

        #endregion

        #region Properties (serialized)

        [Order(0), Offset(7)]
        private byte VNPadded { get; set; }

        #endregion

        #region Properties (C struct)

        public byte VN { get => (byte)(VNPadded >> 1); set => VNPadded = (byte)(value << 1); }

        #endregion

        #region Constructor

        public N64GspCullDisplayListCommand() : 
            base(N64GspCommandByte.G_CULLDL)
        { }

        #endregion

        #region Methods (: ICustomSerializable)

        public override void Serialize(EndianBinaryWriter writer)
        {
            base.Serialize(writer);
            writer.Write(PaddingBytes);
            writer.Write(VNPadded);
        }

        public override void Deserialize(EndianBinaryReader reader)
        {
            // TODO: not called
            base.Deserialize(reader);
            reader.ReadBytes(PaddingBytes.Length);
            VNPadded = reader.ReadByte();
        }

        #endregion

        #region Methods (: object)

        public override string ToString() =>
            GetString(
                new PropertyNameAndValue(nameof(VN), VN));

        #endregion
    }
}
