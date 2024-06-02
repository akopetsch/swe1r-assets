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
    ///     <see href="http://n64devkit.square7.ch/n64man/gsp/gSP1Triangle.htm">
    ///       n64devkit.square7.ch - 'gSP1Triangle'</see></item>
    /// </list>
    /// </summary>
    public class N64Gsp1TriangleCommand : N64GspCommand, IN64GspTrianglesCommand, ICustomSerializable
    {
        #region Fields

        private static readonly byte[] padding = new byte[4];

        #endregion

        #region Properties

        [Order(0)]
        public byte Index0 { get; set; }
        [Order(1)]
        public byte Index1 { get; set; }
        [Order(2)]
        public byte Index2 { get; set; }

        #endregion

        #region Properties (: IN64GspTrianglesCommand)

        public IEnumerable<int> Indices
        {
            get
            {
                yield return Index0;
                yield return Index1;
                yield return Index2;
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
            new Triangle(Index0, Index1, Index2);

        #endregion

        #region Constructor

        public N64Gsp1TriangleCommand() => 
            Byte = 5;

        #endregion

        #region Methods (: ICustomSerializable)

        public override void Serialize(EndianBinaryWriter writer)
        {
            base.Serialize(writer);
            writer.Write(Index0);
            writer.Write(Index1);
            writer.Write(Index2);
            writer.Write(padding);
        }

        public override void Deserialize(EndianBinaryReader reader)
        {
            // TODO: not called
            base.Deserialize(reader);
            Byte = reader.ReadByte();
            Index0 = reader.ReadByte();
            Index1 = reader.ReadByte();
            Index2 = reader.ReadByte();
            reader.ReadBytes(padding.Length);
        }

        #endregion

        #region Methods (: object)

        public override string ToString() =>
            $"({Byte} {Triangle})";

        #endregion
    }
}
