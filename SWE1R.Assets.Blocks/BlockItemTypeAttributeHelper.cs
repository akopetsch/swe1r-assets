// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using System;
using System.Reflection;

namespace SWE1R.Assets.Blocks
{
    public static class BlockItemTypeAttributeHelper
    {
        public static BlockItemType GetBlockItemClassType(Type blockItemClassType) =>
            blockItemClassType.GetCustomAttribute<BlockItemTypeAttribute>().BlockItemType;
    }
}
