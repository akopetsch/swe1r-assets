// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization.Attributes;
using System;

namespace SWE1R.Assets.Blocks.Common.Colors
{
    public class ColorRgb<T> where T :
        struct, IComparable, IFormattable, IConvertible, IComparable<T>, IEquatable<T>
    {
        [Order(0)] public T R { get; set; }
        [Order(1)] public T G { get; set; }
        [Order(2)] public T B { get; set; }

        public ColorRgb() { }

        public ColorRgb(T r, T g, T b)
        {
            R = r;
            G = g;
            B = b;
        }
    }
}
