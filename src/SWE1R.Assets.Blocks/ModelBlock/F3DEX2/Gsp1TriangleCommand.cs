﻿// SPDX-License-Identifier: MIT

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
    ///     <see href="https://ultra64.ca/files/documentation/online-manuals/man/n64man/gsp/gSP1Triangle.html">
    ///       ultra64.ca - 'Online Manuals (OS 2.0J)' - 'gSP1Triangle'</see></item>
    ///   <item>
    ///     <see href="https://ultra64.ca/files/documentation/online-manuals/man/header/gbi.htm#:~:text=gSP1Triangle%28pkt">
    ///       ultra64.ca - 'Online Manuals (OS 2.0J)' - gbi.h - gSP1Triangle</see></item>
    /// </list>
    /// </summary>
    [MacroName("gSP1Triangle")]
    public class Gsp1TriangleCommand : GraphicsCommand, ITrianglesGraphicsCommand, ICustomSerializable
    {
        #region Fields

        private static readonly byte[] PaddingBytes = new byte[4];

        #endregion

        #region Properties (serialized)

        [Order(0)]
        private byte V0Padded { get; set; }
        [Order(1)]
        private byte V1Padded { get; set; }
        [Order(2)]
        private byte V2Padded { get; set; }

        #endregion

        #region Properties (C macro)

        public byte V0 { get => (byte)(V0Padded >> 1); set => V0Padded = Convert.ToByte(value << 1); }
        public byte V1 { get => (byte)(V1Padded >> 1); set => V1Padded = Convert.ToByte(value << 1); }
        public byte V2 { get => (byte)(V2Padded >> 1); set => V2Padded = Convert.ToByte(value << 1); }

        #endregion

        #region Properties (: GraphicsCommand)

        protected override object[] MacroArguments => 
            new object[] { V0, V1, V2 };

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

        public Gsp1TriangleCommand() :
            base(GraphicsCommandByte.G_TRI1)
        { }

        public Gsp1TriangleCommand(byte v0, byte v1, byte v2) :
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
            reader.Read<byte>(PaddingBytes.Length);
        }

        #endregion
    }
}
