// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ByteSerialization.Extensions
{
    public static class LinqExtensions
    {
        public static IEnumerable<T> Except<T>(this IEnumerable<T> first, T second) =>
            first.Except(new T[] { second });

        public static IEnumerable<T> OfType<T>(this IEnumerable<T> source, Type type) =>
            source.Where(x => x?.GetType().Is(type) ?? false);

        public static bool AnyDuplicate<T>(this IEnumerable<T> source)
        {
            var knownKeys = new HashSet<T>();
            return source.Any(item => !knownKeys.Add(item));
        }

        public static bool AllUnique<T>(this IEnumerable<T> source) =>
            !source.AnyDuplicate();

        public static bool Are<T1, T2>(this IEnumerable values) =>
            values.Cast<object>().Are(new Type[] { typeof(T1), typeof(T2) });

        public static bool Are<T1, T2, T3>(this IEnumerable values) =>
            values.Cast<object>().Are(new Type[] { typeof(T1), typeof(T2), typeof(T3) });

        private static bool Are(this IEnumerable values, IEnumerable<Type> types) =>
            types.Contains(values.Cast<object>().Select(n => n?.GetType()).Distinct());

        private static bool Are(this IEnumerable values, params Type[] types) =>
            values.Are(types as IEnumerable<Type>);

        private static bool Contains<T>(this IEnumerable<T> a, IEnumerable<T> b) =>
            !b.Any(x => !a.Contains(x));
    }
}
