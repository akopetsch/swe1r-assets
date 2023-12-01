// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using System;
using System.Collections.Concurrent;
using System.Reflection;

namespace SWE1R.Assets.Blocks
{
    public static class BlockItemTypeAttributeHelper
    {
        private static readonly ConcurrentDictionary<Type, BlockItemType> _dictionary =
            new ConcurrentDictionary<Type, BlockItemType>();

        public static BlockItemType GetBlockItemType(Type blockItemClassType) =>
            _dictionary.GetOrAdd(blockItemClassType, 
                x => blockItemClassType.GetCustomAttribute<BlockItemTypeAttribute>().BlockItemType);
    }
}
