// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

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
