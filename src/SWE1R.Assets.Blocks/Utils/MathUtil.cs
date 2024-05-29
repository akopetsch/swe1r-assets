// SPDX-License-Identifier: GPL-2.0-only

namespace SWE1R.Assets.Blocks.Utils
{
    public static class MathUtil
    {
        public static bool IsPowerOfTwo(int n)
        {
            if (n <= 0)
                return false;
            return (n & (n - 1)) == 0;
        }
    }
}
