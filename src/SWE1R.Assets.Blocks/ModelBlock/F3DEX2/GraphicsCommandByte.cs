// SPDX-License-Identifier: MIT

namespace SWE1R.Assets.Blocks.ModelBlock.F3DEX2
{
    //  

    /// <summary>
    /// See also:
    /// <list type="bullet">
    ///   <item>
    ///     <see href="https://ultra64.ca/files/documentation/online-manuals/man/header/gbi.htm#:~:text=ifdef">
    ///       ultra64.ca - 'Online Manuals (OS 2.0J)' - gbi.h</see></item>
    /// </list>
    /// </summary>
    public enum GraphicsCommandByte : byte
    {
        #region F3DEX_GBI_2

        G_SETOTHERMODE_L = 0xe2,
        G_ENDDL = 0xdf,

        G_VTX = 0x01,
        G_CULLDL = 0x03,
        G_TRI1 = 0x05,
        G_TRI2 = 0x06,
        
        #endregion

        #region RDP commands

        G_SETCOMBINE = 0xfc, // -4

        #endregion
    }
}
