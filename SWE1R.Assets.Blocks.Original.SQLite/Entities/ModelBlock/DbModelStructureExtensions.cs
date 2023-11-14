// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

namespace SWE1R.Assets.Blocks.Original.SQLite.Entities.ModelBlock
{
    public static class DbModelStructureExtensions
    {
        public static IEnumerable<T> OfModel<T>(this IEnumerable<T> source, int model) 
            where T : DbModelStructure =>
            source.Where(x => x.Model == model);

        public static IEnumerable<T> OrderByOffset<T>(this IEnumerable<T> source)
            where T : DbModelStructure =>
            source.OrderBy(x => x.Offset);
    }
}
