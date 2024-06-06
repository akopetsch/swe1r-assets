// SPDX-License-Identifier: MIT

namespace SWE1R.Assets.Blocks.ModelBlock.N64Sdk.GraphicsCommands
{
    public enum GraphicsCommandByte : byte
    {
        #region F3DEX_GBI_2

        G_VTX = 0x01,
        G_CULLDL = 0x03,
        G_TRI1 = 0x05,
        G_TRI2 = 0x06,
        G_SETOTHERMODE_L = 0xe2,

        #endregion

        #region RDP commands

        G_SETCOMBINE = 0xfc,

        #endregion
    }
}
