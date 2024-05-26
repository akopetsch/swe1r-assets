// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using System;
using System.Collections.Generic;

namespace ByteSerialization.IO.Utils
{
    public class StructArrayComparer
    {
        public class Difference<TItem>
            where TItem : struct
        {
            public int Index { get; set; }
            public TItem? Left { get; set; }
            public TItem? Right { get; set; }

            public Difference(int offset, TItem? leftByte, TItem? rightByte)
            {
                Index = offset;
                Left = leftByte;
                Right = rightByte;
            }
        }

        public static List<Difference<TItem>> Compare<TItem>(TItem[] left, TItem[] right)
            where TItem : struct
        {
            var differences = new List<Difference<TItem>>();

            int length = Math.Min(left.Length, right.Length);
            for (int i = 0; i < length; i++)
                if (!EqualityComparer<TItem?>.Default.Equals(left[i], right[i]))
                    differences.Add(new Difference<TItem>(i, left[i], right[i]));

            if (left.Length != right.Length)
                if (left.Length > right.Length)
                    for (int i = right.Length; i < left.Length; i++)
                        differences.Add(new Difference<TItem>(i, left[i], null));
                else
                    for (int i = left.Length; i < right.Length; i++)
                        differences.Add(new Difference<TItem>(i, null, right[i]));

            return differences;
        }
    }
}
