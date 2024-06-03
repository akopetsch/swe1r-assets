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

        private static readonly byte[] PaddingBytes = new byte[4];

        #endregion

        #region Properties

        [Order(0)]
        public byte V0 { get; set; }
        [Order(1)]
        public byte V1 { get; set; }
        [Order(2)]
        public byte V2 { get; set; }

        #endregion

        #region Properties (: IN64GspTrianglesCommand)

        public IEnumerable<int> Indices
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

        public N64Gsp1TriangleCommand() => 
            Byte = 5;

        #endregion

        #region Methods (: ICustomSerializable)

        public override void Serialize(EndianBinaryWriter writer)
        {
            base.Serialize(writer);
            writer.Write(V0);
            writer.Write(V1);
            writer.Write(V2);
            writer.Write(PaddingBytes);
        }

        public override void Deserialize(EndianBinaryReader reader)
        {
            // TODO: not called
            base.Deserialize(reader);
            V0 = reader.ReadByte();
            V1 = reader.ReadByte();
            V2 = reader.ReadByte();
            reader.ReadBytes(PaddingBytes.Length);
        }

        #endregion

        #region Methods (: object)

        public override string ToString() =>
            $"({Byte} {Triangle})";

        #endregion
    }
}
