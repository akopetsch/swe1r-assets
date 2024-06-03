// SPDX-License-Identifier: MIT

using ByteSerialization;
using ByteSerialization.Attributes;
using ByteSerialization.IO;

namespace SWE1R.Assets.Blocks.ModelBlock.Meshes.VertexIndices
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

        #region Properties

        [Order(0), Offset(7)]
        public byte VN { get; set; }

        #endregion

        #region Constructor

        public N64GspCullDisplayListCommand() => 
            Byte = 3;

        #endregion

        #region Methods (: ICustomSerializable)

        public override void Serialize(EndianBinaryWriter writer)
        {
            base.Serialize(writer);
            writer.Write(PaddingBytes);
            writer.Write(VN);
        }

        public override void Deserialize(EndianBinaryReader reader)
        {
            // TODO: not called
            base.Deserialize(reader);
            reader.ReadBytes(PaddingBytes.Length);
            VN = reader.ReadByte();
        }

        #endregion

        #region Methods (: object)

        public override string ToString() =>
            $"({Byte} {nameof(VN)} = {VN})";

        #endregion
    }
}
