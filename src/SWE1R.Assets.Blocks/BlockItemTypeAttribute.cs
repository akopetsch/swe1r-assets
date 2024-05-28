// Copyright 2024 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using System;

namespace SWE1R.Assets.Blocks
{
    [AttributeUsage(AttributeTargets.Class)]
    public class BlockItemTypeAttribute : Attribute
    {
        public BlockItemType BlockItemType { get; }

        public BlockItemTypeAttribute(BlockItemType blockItemType) =>
            BlockItemType = blockItemType;
    }
}
