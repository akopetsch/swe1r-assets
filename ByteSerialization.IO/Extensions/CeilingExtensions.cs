// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using System;

namespace ByteSerialization.IO.Extensions
{
    public static class CeilingExtensions
    {
        public static short Ceiling(this short value, short stepSize) => 
            (short)Ceiling((long)value, stepSize);

        public static int Ceiling(this int value, int stepSize) => 
            (int)Ceiling((long)value, stepSize);

        public static long Ceiling(this long value, long stepSize) => 
            (long)(Math.Ceiling((decimal)value / stepSize) * stepSize);
    }
}
