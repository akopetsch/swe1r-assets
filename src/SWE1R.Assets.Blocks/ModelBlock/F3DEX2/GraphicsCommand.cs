// SPDX-License-Identifier: MIT

using ByteSerialization.Attributes;
using ByteSerialization.IO;
using System.Linq;

namespace SWE1R.Assets.Blocks.ModelBlock.F3DEX2
{
    /// <summary>
    /// See also:
    /// <list type="bullet">
    ///   <item>
    ///     <see href="http://n64devkit.square7.ch/header/gbi.htm#:~:text=Graphics%20Commands">
    ///       n64devkit.square7.ch - gbi.h - 'Graphics Commands'</see></item>
    ///   <item>
    ///     <see href="http://n64devkit.square7.ch/header/gbi.htm#:~:text=Gfx%3B">
    ///       n64devkit.square7.ch - gbi.h - 'Gfx'</see></item>
    ///   <item>
    ///     <see href="https://github.com/akopetsch/SW_RACER_RE/blob/d2d15c27d81e51e91996563795643c91439147aa/src/types.h#L1365">
    ///       github.com - akopetsch/SW_RACER_RE - types.h - Gfx</see></item>
    ///   <item>
    ///     <see href="https://github.com/akopetsch/Sw_Racer/blob/76c8ad9cea549ea18457846a135a7f25d48b3813/include/Swr_Model.h#L515">
    ///       github.com - akopetsch/Sw_Racer - Swr_Model.h - SWR_MODEL_Section48</see></item>
    /// </list>
    /// </summary>
    [Sizeof(8)]
    public abstract class GraphicsCommand
    {
        #region Classes (helper)

        protected class PropertyNameAndValue // TODO: !!!!!!! refactor class PropertyNameAndValue
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

        [RecordTypeIdentifier(GraphicsCommandByte.G_VTX, typeof(GspVertexCommand))]
        [RecordTypeIdentifier(GraphicsCommandByte.G_CULLDL, typeof(GspCullDisplayListCommand))]
        [RecordTypeIdentifier(GraphicsCommandByte.G_TRI1, typeof(Gsp1TriangleCommand))]
        [RecordTypeIdentifier(GraphicsCommandByte.G_TRI2, typeof(Gsp2TrianglesCommand))]
        [RecordTypeIdentifier(GraphicsCommandByte.G_SETCOMBINE, typeof(GdpSetCombineLerpCommand))]
        [RecordTypeIdentifier(GraphicsCommandByte.G_SETOTHERMODE_L, typeof(GdpSetRenderModeCommand))]
        [Order(0)]
        public GraphicsCommandByte Byte { get; set; }

        #endregion

        #region Constructor

        protected GraphicsCommand(GraphicsCommandByte commandByte) =>
            Byte = commandByte;

        #endregion

        #region Methods (serialization)

        public virtual void Serialize(EndianBinaryWriter writer) =>
            writer.Write((byte)Byte);

        public virtual void Deserialize(EndianBinaryReader reader) =>
            Byte = (GraphicsCommandByte)reader.ReadByte();

        #endregion

        #region Methods (helper)

        protected string GetString(params PropertyNameAndValue[] propertyNamesAndValues) =>
            $"{GetType().Name}({string.Join(", ", propertyNamesAndValues.Select(x => x.ToString()))})";

        #endregion
    }
}
