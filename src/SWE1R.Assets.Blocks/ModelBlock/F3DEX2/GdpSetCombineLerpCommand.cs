// SPDX-License-Identifier: MIT

namespace SWE1R.Assets.Blocks.ModelBlock.F3DEX2
{
    /// <summary>
    /// See also:
    /// <list type="bullet">
    ///   <item>
    ///     <see href="http://n64devkit.square7.ch/n64man/gdp/gDPSetCombineLERP.htm">
    ///       n64devkit.square7.ch - 'gDPSetCombineLERP'</see></item>
    /// </list>
    /// </summary>
    public class GdpSetCombineLerpCommand : GraphicsCommand
    {
        #region Properties (serialized)

        // TODO: implement and use command

        #endregion

        #region Constructor

        public GdpSetCombineLerpCommand() : 
            base(GraphicsCommandByte.G_SETCOMBINE)
        { }

        #endregion
    }
}
