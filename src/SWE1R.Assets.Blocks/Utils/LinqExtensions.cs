// SPDX-License-Identifier: MIT

using System.Collections.Generic;
using System.Linq;

namespace SWE1R.Assets.Blocks.Utils
{
    public static class LinqExtensions
    {
        // https://stackoverflow.com/a/419063
        public static IEnumerable<TSource[]> Chunk<TSource>(this IEnumerable<TSource> source, int size) =>
            source.Select((x, i) => new { Index = i, Value = x })
            .GroupBy(x => x.Index / size)
            .Select(x => x.Select(v => v.Value).ToArray())
            .ToList();
    }
}
