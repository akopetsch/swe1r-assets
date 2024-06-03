// SPDX-License-Identifier: MIT

using ByteSerialization;
using ByteSerialization.Attributes;
using ByteSerialization.IO;
using SWE1R.Assets.Blocks.ModelBlock.Meshes.Geometry;
using System.Collections.Generic;

namespace SWE1R.Assets.Blocks.ModelBlock.Meshes.N64GspCommands
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
        #region Fields

        private static readonly byte PaddingByte = 0;

        #endregion

        #region Properties (serialized)

        [Order(0)] 
        public byte V00 { get; set; }
        [Order(1)]
        public byte V01 { get; set; }
        [Order(2)]
        public byte V02 { get; set; }
        [Order(3), Offset(5)]
        public byte V10 { get; set; }
        [Order(4)]
        public byte V11 { get; set; }
        [Order(5)]
        public byte V12 { get; set; }

        #endregion

        #region Properties (: IN64GspTrianglesCommand)

        public IEnumerable<int> Indices
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

        public N64Gsp2TrianglesCommand() : 
            base(N64GspCommandByte.G_TRI2)
        { }

        #endregion

        #region Methods (: ICustomSerializable)

        public override void Serialize(EndianBinaryWriter writer)
        {
            base.Serialize(writer);
            writer.Write(V00);
            writer.Write(V01);
            writer.Write(V02);
            writer.Write(PaddingByte);
            writer.Write(V10);
            writer.Write(V11);
            writer.Write(V12);
        }

        public override void Deserialize(EndianBinaryReader reader)
        {
            // TODO: not called
            base.Deserialize(reader);
            V00 = reader.ReadByte();
            V01 = reader.ReadByte();
            V02 = reader.ReadByte();
            reader.Read(PaddingByte.GetType());
            V10 = reader.ReadByte();
            V11 = reader.ReadByte();
            V12 = reader.ReadByte();
        }

        #endregion

        #region Methods (: object)

        public override string ToString() =>
            GetString(
                new PropertyNameAndValue(nameof(V00), V00),
                new PropertyNameAndValue(nameof(V01), V01),
                new PropertyNameAndValue(nameof(V02), V02),
                new PropertyNameAndValue(nameof(V10), V10),
                new PropertyNameAndValue(nameof(V11), V11),
                new PropertyNameAndValue(nameof(V12), V12));

        #endregion
    }
}
