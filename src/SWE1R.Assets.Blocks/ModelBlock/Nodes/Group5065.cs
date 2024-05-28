// Copyright 2024 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization.Attributes;

namespace SWE1R.Assets.Blocks.ModelBlock.Nodes
{
    /// <summary>
    /// <see href="https://github.com/akopetsch/Sw_Racer/blob/76c8ad9cea549ea18457846a135a7f25d48b3813/include/Swr_Model.h#L104">SWR_AltN_0x5065</see>
    /// </summary>
    public class Group5065 : FlaggedNode
    {
        /// <summary>
        /// Always 0 or -1.
        /// </summary>
        [Order(0)] public int Int { get; set; }

        public Group5065() : base() =>
            Flags = NodeFlags.Group5065;
    }
}
