﻿// SPDX-License-Identifier: MIT

using ByteSerialization.Attributes;
using ByteSerialization.IO;
using System.Linq;

namespace SWE1R.Assets.Blocks.ModelBlock.Meshes.N64GspCommands
{
    /// <summary>
    /// See also:
    /// <list type="bullet">
    ///   <item>
    ///     <see href="http://n64devkit.square7.ch/header/gbi.htm#:~:text=Graphics%20Commands">
    ///       n64devkit.square7.ch - 'gbi.h' - 'Graphics Commands'</see></item>
    ///   <item>
    ///     <see href="https://github.com/akopetsch/Sw_Racer/blob/76c8ad9cea549ea18457846a135a7f25d48b3813/include/Swr_Model.h#L515">
    ///       github.com - akopetsch/Sw_Racer - 'SWR_MODEL_Section48'</see></item>
    /// </list>
    /// </summary>
    [Sizeof(8)]
    public abstract class N64GspCommand
    {
        #region Classes (helper)

        protected class PropertyNameAndValue
        {
            public string Name { get; set; }
            public object Value { get; set; }

            public PropertyNameAndValue(string name, object value)
            {
                Name = name;
                Value = value;
            }

            public override string ToString() =>
                $"{Name} = {Value}";
        }

        #endregion

        #region Properties (serialized)

        [RecordTypeIdentifier(N64GspCommandByte.G_VTX, typeof(N64GspVertexCommand))]
        [RecordTypeIdentifier(N64GspCommandByte.G_CULLDL, typeof(N64GspCullDisplayListCommand))]
        [RecordTypeIdentifier(N64GspCommandByte.G_TRI1, typeof(N64Gsp1TriangleCommand))]
        [RecordTypeIdentifier(N64GspCommandByte.G_TRI2, typeof(N64Gsp2TrianglesCommand))]
        [Order(0)]
        public N64GspCommandByte Byte { get; set; }

        #endregion

        #region Constructor

        protected N64GspCommand(N64GspCommandByte commandByte) =>
            Byte = commandByte;

        #endregion

        #region Methods (serialization)

        public virtual void Serialize(EndianBinaryWriter writer) =>
            writer.Write((byte)Byte);

        public virtual void Deserialize(EndianBinaryReader reader) =>
            Byte = (N64GspCommandByte)reader.ReadByte();

        #endregion

        #region Methods (helper)

        protected string GetString(params PropertyNameAndValue[] propertyNamesAndValues) =>
            $"{GetType().Name}({string.Join(", ", propertyNamesAndValues.Select(x => x.ToString()))})";

        #endregion
    }
}
