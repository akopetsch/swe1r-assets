// SPDX-License-Identifier: MIT

using System;

namespace SWE1R.Assets.Blocks.ModelBlock.F3DEX2
{
    /// <summary>
    /// See also:
    /// <list type="bullet">
    ///   <item>
    ///     <see href="https://ultra64.ca/files/documentation/online-manuals/man/n64man/gdp/gDPSetCombineMode.html#:~:text=gDPSetCombineLERP">
    ///       ultra64.ca - 'Online Manuals (OS 2.0J)' - 'gDPSetCombineMode' - 'gDPSetCombineLERP'</see></item>
    ///   <item>
    ///     <see href="https://ultra64.ca/files/documentation/online-manuals/man/header/gbi.htm#:~:text=gDPSetCombineLERP">
    ///       ultra64.ca - 'Online Manuals (OS 2.0J)' - gbi.h - gDPSetCombineLERP</see></item>
    /// </list>
    /// </summary>
    public class GdpSetCombineLerpCommand : GraphicsCommand
    {
        #region Properties (serialized)

        // TODO: implement and use command

        #endregion

        #region Properties (: GraphicsCommand)

        protected override object[] MacroArguments =>
            throw new NotImplementedException();

        #endregion

        #region Constructor

        public GdpSetCombineLerpCommand() : 
            base(GraphicsCommandByte.G_SETCOMBINE, "gDPSetCombineLERP")
        { }

        #endregion
    }
}
