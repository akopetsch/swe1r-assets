// SPDX-License-Identifier: MIT

using ByteSerialization;
using ByteSerialization.Attributes;
using ByteSerialization.IO;
using SWE1R.Assets.Blocks.ModelBlock.Meshes.Geometry;
using SWE1R.Assets.Blocks.ModelBlock.Meshes.N64GspCommands;
using System.Collections.Generic;

namespace SWE1R.Assets.Blocks.ModelBlock.Meshes.VertexIndices
{
    /// <summary>
    /// See also:
    /// <list type="bullet">
    ///   <item>
    ///     <see href="http://n64devkit.square7.ch/n64man/gsp/gSP2Triangles.htm">
    ///       n64devkit.square7.ch - 'gSP2Triangles'</see></item>
    /// </list>
    /// </summary>
    public class N64Gsp2TrianglesCommand : N64GspCommand, IN64GspTrianglesCommand, ICustomSerializable
    {
        #region Properties (serialized)

        [Order(0)] 
        public byte Index0 { get; set; }
        [Order(1)]
        public byte Index1 { get; set; }
        [Order(2)]
        public byte Index2 { get; set; }
        [Order(3), Offset(5)]
        public byte Index3 { get; set; }
        [Order(4)]
        public byte Index4 { get; set; }
        [Order(5)]
        public byte Index5 { get; set; }

        #endregion

        #region Properties (: IN64GspTrianglesCommand)

        public IEnumerable<int> Indices
        {
            get
            {
                yield return Index0;
                yield return Index1;
                yield return Index2;

                yield return Index3;
                yield return Index4;
                yield return Index5;
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

        public Triangle Triangle0 => new Triangle(Index0, Index1, Index2);

        public Triangle Triangle1 => new Triangle(Index3, Index4, Index5);

        #endregion

        #region Constructor

        public N64Gsp2TrianglesCommand() => 
            Byte = 6;

        #endregion

        #region Methods (: ICustomSerializable)

        public override void Serialize(EndianBinaryWriter writer)
        {
            base.Serialize(writer);
            writer.Write(Byte);
            writer.Write(Index0);
            writer.Write(Index1);
            writer.Write(Index2);
            writer.Write((byte)0);
            writer.Write(Index3);
            writer.Write(Index4);
            writer.Write(Index5);
        }

        public override void Deserialize(EndianBinaryReader reader)
        {
            // TODO: not called
            base.Deserialize(reader);
            Byte = reader.ReadByte();
            Index0 = reader.ReadByte();
            Index1 = reader.ReadByte();
            Index2 = reader.ReadByte();
            reader.ReadByte();
            Index3 = reader.ReadByte();
            Index4 = reader.ReadByte();
            Index5 = reader.ReadByte();
        }

        #endregion

        #region Methods (: object)

        public override string ToString() =>
            $"({Byte} {Triangle0}, {Triangle1})";

        #endregion
    }
}
