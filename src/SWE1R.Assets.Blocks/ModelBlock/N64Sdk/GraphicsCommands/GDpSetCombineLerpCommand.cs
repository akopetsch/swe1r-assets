// SPDX-License-Identifier: MIT

namespace SWE1R.Assets.Blocks.ModelBlock.N64Sdk.GraphicsCommands
{
    /// <summary>
    /// See also:
    /// <list type="bullet">
    ///   <item>
    ///     <see href="http://n64devkit.square7.ch/n64man/gdp/gDPSetCombineLERP.htm">
    ///       n64devkit.square7.ch - 'gDPSetCombineLERP'</see></item>
    /// </list>
    /// </summary>
    public class GDpSetCombineLerpCommand : GraphicsCommand
    {
        #region Properties (serialized)

        // TODO: implement and use command

        #endregion

        #region Constructor

        public GDpSetCombineLerpCommand() : 
            base(GraphicsCommandByte.G_SETCOMBINE)
        { }

        #endregion
    }
}
