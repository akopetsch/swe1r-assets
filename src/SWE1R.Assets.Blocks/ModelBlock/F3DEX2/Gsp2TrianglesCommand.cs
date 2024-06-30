// SPDX-License-Identifier: MIT

using ByteSerialization;
using ByteSerialization.Attributes;
using ByteSerialization.IO;
using SWE1R.Assets.Blocks.ModelBlock.Meshes.Geometry;
using System;
using System.Collections.Generic;

namespace SWE1R.Assets.Blocks.ModelBlock.F3DEX2
{
    /// <summary>
    /// See also:
    /// <list type="bullet">
    ///   <item>
    ///     <see href="https://ultra64.ca/files/documentation/online-manuals/man/n64man/gsp/gSP2Triangles.html">
    ///       ultra64.ca - 'Online Manuals (OS 2.0J)' - 'gSP2Triangles'</see></item>
    ///   <item>
    ///     <see href="https://ultra64.ca/files/documentation/online-manuals/man/header/gbi.htm#:~:text=gSP2Triangles">
    ///       ultra64.ca - 'Online Manuals (OS 2.0J)' - gbi.h - gSP2Triangles</see></item>
    /// </list>
    /// </summary>
    public class Gsp2TrianglesCommand : GraphicsCommand, ITrianglesGraphicsCommand, ICustomSerializable
    {
        #region Fields

        private static readonly byte PaddingByte = 0;

        #endregion

        #region Properties (serialized)

        [Order(0)]
        private byte V00Padded { get; set; }
        [Order(1)]
        private byte V01Padded { get; set; }
        [Order(2)]
        private byte V02Padded { get; set; }
        [Order(3), Offset(5)]
        private byte V10Padded { get; set; }
        [Order(4)]
        private byte V11Padded { get; set; }
        [Order(5)]
        private byte V12Padded { get; set; }

        #endregion

        #region Properties (C macro)

        public byte V00 { get => (byte)(V00Padded >> 1); set => V00Padded = Convert.ToByte(value << 1); }
        public byte V01 { get => (byte)(V01Padded >> 1); set => V01Padded = Convert.ToByte(value << 1); }
        public byte V02 { get => (byte)(V02Padded >> 1); set => V02Padded = Convert.ToByte(value << 1); }
        public byte V10 { get => (byte)(V10Padded >> 1); set => V10Padded = Convert.ToByte(value << 1); }
        public byte V11 { get => (byte)(V11Padded >> 1); set => V11Padded = Convert.ToByte(value << 1); }
        public byte V12 { get => (byte)(V12Padded >> 1); set => V12Padded = Convert.ToByte(value << 1); }

        #endregion

        #region Properties (: GraphicsCommand)

        protected override object[] MacroArguments => 
            new object[] { V00, V01, V02, V10, V11, V12 };

        #endregion

        #region Properties (: IN64GspTrianglesCommand)

        public IEnumerable<byte> Indices
        {
            get
            {
                yield return V00;
                yield return V01;
                yield return V02;

                yield return V10;
                yield return V11;
                yield return V12;
            }
        }

        public IEnumerable<Triangle> Triangles
        {
            get
            {
                yield return Triangle0;
                yield return Triangle1;
            }
        }

        public Triangle Triangle0 => new Triangle(V00, V01, V02);

        public Triangle Triangle1 => new Triangle(V10, V11, V12);

        #endregion

        #region Constructor

        public Gsp2TrianglesCommand() :
            base(GraphicsCommandByte.G_TRI2, "gSP2Triangles")
        { }

        public Gsp2TrianglesCommand(
            byte v00, byte v01, byte v02,
            byte v10, byte v11, byte v12) :
            this()
        {
            V00 = v00;
            V01 = v01;
            V02 = v02;

            V10 = v10;
            V11 = v11;
            V12 = v12;
        }

        #endregion

        #region Methods (: ICustomSerializable)

        public override void Serialize(EndianBinaryWriter writer)
        {
            base.Serialize(writer);
            writer.Write(V00Padded);
            writer.Write(V01Padded);
            writer.Write(V02Padded);
            writer.Write(PaddingByte);
            writer.Write(V10Padded);
            writer.Write(V11Padded);
            writer.Write(V12Padded);
        }

        public override void Deserialize(EndianBinaryReader reader)
        {
            // TODO: not called
            base.Deserialize(reader);
            V00Padded = reader.ReadByte();
            V01Padded = reader.ReadByte();
            V02Padded = reader.ReadByte();
            reader.Read(PaddingByte.GetType());
            V10Padded = reader.ReadByte();
            V11Padded = reader.ReadByte();
            V12Padded = reader.ReadByte();
        }

        #endregion
    }
}
