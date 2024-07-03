// SPDX-License-Identifier: MIT

using ByteSerialization.Attributes;
using ByteSerialization.IO;

namespace SWE1R.Assets.Blocks.ModelBlock.F3DEX2
{
    /// <summary>
    /// See also:
    /// <list type="bullet">
    ///   <item>
    ///     <see href="https://ultra64.ca/files/documentation/online-manuals/man/header/gbi.htm#:~:text=Gfx%3B">
    ///       ultra64.ca - 'Online Manuals (OS 2.0J)' - gbi.h - Gfx</see></item>
    ///   <item>
    ///     <see href="https://ultra64.ca/files/documentation/online-manuals/man/header/gbi.htm#:~:text=Graphics%20Commands">
    ///       ultra64.ca - 'Online Manuals (OS 2.0J)' - gbi.h - 'Graphics Commands'</see></item>
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
        #region Properties (serialized)

        [RecordTypeIdentifier(GraphicsCommandByte.G_VTX, typeof(GspVertexCommand))]
        [RecordTypeIdentifier(GraphicsCommandByte.G_CULLDL, typeof(GspCullDisplayListCommand))]
        [RecordTypeIdentifier(GraphicsCommandByte.G_TRI1, typeof(Gsp1TriangleCommand))]
        [RecordTypeIdentifier(GraphicsCommandByte.G_TRI2, typeof(Gsp2TrianglesCommand))]
        [RecordTypeIdentifier(GraphicsCommandByte.G_SETCOMBINE, typeof(GdpSetCombineLerpCommand))]
        [RecordTypeIdentifier(GraphicsCommandByte.G_SETOTHERMODE_L, typeof(GdpSetRenderModeCommand))]
        [Order(0)]
        public GraphicsCommandByte Byte { get; private set; }

        public string MacroName { get; private set; }

        protected abstract object[] MacroArguments { get; }

        #endregion

        #region Constructor

        protected GraphicsCommand(GraphicsCommandByte commandByte, string macroName)
        {
            Byte = commandByte;
            MacroName = macroName;
        }

        #endregion

        #region Methods (serialization)

        public virtual void Serialize(EndianBinaryWriter writer) =>
            writer.Write((byte)Byte);

        public virtual void Deserialize(EndianBinaryReader reader) =>
            Byte = (GraphicsCommandByte)reader.ReadByte();

        #endregion

        #region Methods (helper)

        public override string ToString() =>
            $"{MacroName}({string.Join(", ", MacroArguments)})";

        #endregion
    }
}
