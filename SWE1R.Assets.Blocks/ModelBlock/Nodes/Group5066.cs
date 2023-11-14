// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization.Attributes;

namespace SWE1R.Assets.Blocks.ModelBlock.Nodes
{
    /// <summary>
    /// <see href="https://github.com/Olganix/Sw_Racer/blob/master/include/Swr_Model.h#L111">SWR_AltN_0x5066</see>
    /// </summary>
    public class Group5066 : FlaggedNode
    {
        [Order(0), Length(8)] public float[] Floats { get; set; }
        /// <summary>
        /// Always <c>{0, 0x80000000, 0}</c>.
        /// <para>
        /// Not sure if these are float32 values composing a vector, because 0x80000000 would be an unusual value (-0).
        /// </para>
        /// </summary>
        [Order(1), Length(3)] public int[] Ints { get; set; }

        public Group5066() : base() =>
            Flags = NodeFlags.Group5066;
    }
}
