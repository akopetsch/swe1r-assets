// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using System.Collections.Generic;
using System.Linq;

namespace ByteSerialization.Extensions
{
    public static class ListExtensions
    {
        public static void AddRangeIfAny<T>(this List<T> list, IEnumerable<T> collection)
        {
            if (collection.Any())
                list.AddRange(collection);
        }
    }
}
