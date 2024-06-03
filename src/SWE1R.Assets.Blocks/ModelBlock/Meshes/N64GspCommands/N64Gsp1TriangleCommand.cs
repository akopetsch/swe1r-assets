// SPDX-License-Identifier: MIT

using ByteSerialization;
using ByteSerialization.Attributes;
using ByteSerialization.IO;
using SWE1R.Assets.Blocks.ModelBlock.Meshes.Geometry;
using System;
using System.Collections.Generic;

namespace SWE1R.Assets.Blocks.ModelBlock.Meshes.N64GspCommands
{
    /// <summary>
    /// See also:
    /// <list type="bullet">
    ///   <item>
    ///     <see href="http://n64devkit.square7.ch/n64man/gsp/gSP1Triangle.htm">
    ///       n64devkit.square7.ch - 'gSP1Triangle'</see></item>
    /// </list>
    /// </summary>
    public class N64Gsp1TriangleCommand : N64GspCommand, IN64GspTrianglesCommand, ICustomSerializable
    {
        #region Fields

        private static readonly byte[] PaddingBytes = new byte[4];

        #endregion

        #region Properties

        [Order(0)]
        private byte V0Padded { get; set; }
        [Order(1)]
        private byte V1Padded { get; set; }
        [Order(2)]
        private byte V2Padded { get; set; }

        #endregion

        #region Properties (C struct)

        public byte V0 { get => (byte)(V0Padded >> 1); set => V0Padded = Convert.ToByte(value << 1); }
        public byte V1 { get => (byte)(V1Padded >> 1); set => V1Padded = Convert.ToByte(value << 1); }
        public byte V2 { get => (byte)(V2Padded >> 1); set => V2Padded = Convert.ToByte(value << 1); }

        #endregion

        #region Properties (: IN64GspTrianglesCommand)

        public IEnumerable<byte> Indices
        {
            get
            {
                yield return V0;
                yield return V1;
                yield return V2;
            }
        }

        public IEnumerable<Triangle> Triangles
        {
            get
            {
                yield return Triangle;
            }
        }

        public Triangle Triangle => 
            new Triangle(V0, V1, V2);

        #endregion

        #region Constructor

        public N64Gsp1TriangleCommand() : 
            base(N64GspCommandByte.G_TRI1)
        { }

        public N64Gsp1TriangleCommand(byte v0, byte v1, byte v2) :
            this()
        {
            V0 = v0;
            V1 = v1;
            V2 = v2;
        }

        #endregion

        #region Methods (: ICustomSerializable)

        public override void Serialize(EndianBinaryWriter writer)
        {
            base.Serialize(writer);
            writer.Write(V0Padded);
            writer.Write(V1Padded);
            writer.Write(V2Padded);
            writer.Write(PaddingBytes);
        }

        public override void Deserialize(EndianBinaryReader reader)
        {
            // TODO: not called
            base.Deserialize(reader);
            V0Padded = reader.ReadByte();
            V1Padded = reader.ReadByte();
            V2Padded = reader.ReadByte();
            reader.ReadBytes(PaddingBytes.Length);
        }

        #endregion

        #region Methods (: object)

        public override string ToString() =>
            GetString(
                new PropertyNameAndValue(nameof(V0), V0),
                new PropertyNameAndValue(nameof(V1), V1),
                new PropertyNameAndValue(nameof(V2), V2));

        #endregion
    }
}
