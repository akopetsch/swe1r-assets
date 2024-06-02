// SPDX-License-Identifier: MIT

using ByteSerialization;
using ByteSerialization.Attributes;
using ByteSerialization.IO;
using SWE1R.Assets.Blocks.ModelBlock.Meshes.Geometry;
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
    public class N64Gsp1TriangleCommand : N64GspCommand, ICustomSerializable
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

        #region Properties (abstraction)

        public override IEnumerable<int> Indices
        {
            get
            {
                yield return Index0;
                yield return Index1;
                yield return Index2;
            }
        }

        public override IEnumerable<Triangle> Triangles
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
            Tag = 5;

        #endregion

        #region Methods (: ICustomSerializable)

        public void Serialize(EndianBinaryWriter writer)
        {
            writer.Write(Tag);
            writer.Write(Index0);
            writer.Write(Index1);
            writer.Write(Index2);
            writer.Write(padding);
        }

        public void Deserialize(EndianBinaryReader reader)
        {
            // TODO: not called

            Tag = reader.ReadByte();
            Index0 = reader.ReadByte();
            Index1 = reader.ReadByte();
            Index2 = reader.ReadByte();
            reader.ReadBytes(padding.Length);
        }

        #endregion

        #region Methods (: object)

        public override string ToString() =>
            $"({Tag} {Triangle})";

        #endregion
    }
}
