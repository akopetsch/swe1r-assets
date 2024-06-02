// SPDX-License-Identifier: MIT

using ByteSerialization;
using ByteSerialization.Attributes;
using ByteSerialization.IO;
using SWE1R.Assets.Blocks.ModelBlock.Meshes.Geometry;
using System.Collections.Generic;
using System.Linq;

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

        private static readonly byte[] padding = new byte[6];

        #endregion

        #region Properties

        [Order(0), Offset(7)]
        public byte MaxIndex { get; set; }

        #endregion

        #region Properties (abstraction)

        public override IEnumerable<int> Indices => Enumerable.Empty<int>();

        public override IEnumerable<Triangle> Triangles => Enumerable.Empty<Triangle>();

        #endregion

        #region Constructor

        public N64GspCullDisplayListCommand() => 
            Tag = 3;

        #endregion

        #region Methods (: ICustomSerializable)

        public void Serialize(EndianBinaryWriter writer)
        {
            writer.Write(Tag);
            writer.Write(padding);
            writer.Write(MaxIndex);
        }

        public void Deserialize(EndianBinaryReader reader)
        {
            // TODO: not called

            Tag = reader.ReadByte();
            reader.ReadBytes(padding.Length);
            MaxIndex = reader.ReadByte();
        }

        #endregion

        #region Methods (: object)

        public override string ToString() =>
            $"({Tag} {nameof(MaxIndex)} = {MaxIndex})";

        #endregion
    }
}
