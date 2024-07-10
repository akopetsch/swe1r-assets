// SPDX-License-Identifier: MIT

namespace SWE1R.Assets.Blocks.ModelBlock.F3DEX2
{
    /// <summary>
    /// See also:
    /// <list type="bullet">
    ///   <item>
    ///     <see href="https://ultra64.ca/files/documentation/online-manuals/man/n64man/gsp/gSPEndDisplayList.html">
    ///       ultra64.ca - 'Online Manuals (OS 2.0J)' - 'gSPEndDisplayList'</see></item>
    ///   <item>
    ///     <see href="https://ultra64.ca/files/documentation/online-manuals/man/header/gbi.htm#:~:text=gSPEndDisplayList">
    ///       ultra64.ca - 'Online Manuals (OS 2.0J)' - gbi.h - gSPEndDisplayList</see></item>
    /// </list>
    /// </summary>
    [MacroName("gSPEndDisplayList")]
    public class GspEndDisplayListCommand : GraphicsCommand
    {
        #region Properties

        protected override object[] MacroArguments =>
            new object[] { };

        #endregion

        #region Constructor

        public GspEndDisplayListCommand() :
            base(GraphicsCommandByte.G_ENDDL)
        { }

        #endregion
    }
}
