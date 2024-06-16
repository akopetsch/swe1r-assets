// SPDX-License-Identifier: MIT

namespace SWE1R.Assets.Blocks.ModelBlock.F3DEX2
{
    /// <summary>
    /// See also:
    /// <list type="bullet">
    ///   <item>
    ///     <see href="http://n64devkit.square7.ch/n64man/gdp/gDPSetRenderMode.htm">
    ///       n64devkit.square7.ch - 'gDPSetRenderMode'</see></item>
    /// </list>
    /// </summary>
    public class GDpSetRenderModeCommand : GraphicsCommand
    {
        #region Properties (serialized)

        // TODO: implement and use command

        #endregion

        #region Constructor

        public GDpSetRenderModeCommand() :
            base(GraphicsCommandByte.G_SETOTHERMODE_L)
        { }

        #endregion
    }
}
