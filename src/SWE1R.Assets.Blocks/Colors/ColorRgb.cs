// SPDX-License-Identifier: MIT

using ByteSerialization.Attributes;
using System;

namespace SWE1R.Assets.Blocks.Colors
{
    public class ColorRgb<T> where T : // TODO: unused
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
