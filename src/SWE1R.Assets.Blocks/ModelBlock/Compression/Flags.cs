// SPDX-License-Identifier: GPL-2.0-only

using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace SWE1R.Assets.Blocks.Common.Compression
{
    internal struct Flags : IReadOnlyCollection<Flag>
    {
        internal Flag[] flags;
        public int Count => flags.Length;
        
        public Flag this[int i] { get => flags[i]; set => flags[i] = value; }
        public IEnumerator<Flag> GetEnumerator() => flags.Cast<Flag>().GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => flags.GetEnumerator();
        public override string ToString() => string.Join(string.Empty, flags.Select(f => (int)f));

        public static explicit operator byte(Flags flags)
        {
            byte b = 0;
            for (int i = 0; i < flags.Count; i++)
                b |= (byte)((byte)flags[i] << i);
            return b;
        }

        public static explicit operator Flags(byte bits)
        {
            var f = new Flags();
            f.flags = new Flag[8];
            for (int i = 0; i < 8; i++)
                f[i] = (Flag)((bits & (1 << i)) >> i);
            return f;
        }
    }
}
