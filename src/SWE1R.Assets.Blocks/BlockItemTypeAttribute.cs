// SPDX-License-Identifier: MIT

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
