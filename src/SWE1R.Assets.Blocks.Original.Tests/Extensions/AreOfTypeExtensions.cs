// SPDX-License-Identifier: MIT

using System.Collections;

namespace SWE1R.Assets.Blocks.Original.Tests.Extensions
{
    internal static class AreOfTypeExtensions
    {
        internal static bool Are<T1, T2>(this IEnumerable values) =>
            values.Cast<object>().Are([typeof(T1), typeof(T2)]);

        internal static bool Are<T1, T2, T3>(this IEnumerable values) =>
            values.Cast<object>().Are([typeof(T1), typeof(T2), typeof(T3)]);

        private static bool Are(this IEnumerable values, params Type[] types) =>
            values.Are(types as IEnumerable<Type>);

        private static bool Are(this IEnumerable values, IEnumerable<Type> types) =>
            types.Contains(values.Cast<object>().Select(n => n?.GetType()).Distinct());

        private static bool Contains<T>(this IEnumerable<T> a, IEnumerable<T> b) =>
            !b.Any(x => !a.Contains(x));
    }
}
