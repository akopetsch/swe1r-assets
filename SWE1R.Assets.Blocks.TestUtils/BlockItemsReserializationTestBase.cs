// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization;

namespace SWE1R.Assets.Blocks.TestUtils
{
    public abstract class BlockItemsReserializationTestBase<TItem> :
        BlockItemsTestBase<TItem> where TItem : BlockItem, new()
    {
        protected BlockItemsReserializationTestBase() : base() { }

        protected override void CompareItemInternal(int i)
        {
            // deserialize
            TItem currentItem = DeserializeItem(i, out ByteSerializerContext _);
            var oldItem = (TItem)currentItem.Clone();

            // re-serialize
            SerializeItem(currentItem, i, out ByteSerializerContext _);

            // compare
            CompareItem(oldItem, currentItem);
        }

        private void CompareItem(TItem oldItem, TItem currentItem)
        {
            if (!AreBytesEqual(oldItem, currentItem))
            {
                var differentPartsTypes = new List<Type>();
                for (int partIndex = 0; partIndex < currentItem.Parts.Length; partIndex++)
                {
                    BlockItemPart oldPart = oldItem.Parts[partIndex];
                    BlockItemPart newPart = currentItem.Parts[partIndex];
                    if (!AreBytesEqual(oldPart, newPart))
                        differentPartsTypes.Add(newPart.GetType());
                }
                string errorMessage = string.Join(", ", differentPartsTypes.Select(x => x.Name));
                AssertFail(errorMessage);
            }
        }

        private static bool AreBytesEqual(BlockItem left, BlockItem right) =>
            left.Bytes.SequenceEqual(right.Bytes);

        private static bool AreBytesEqual(BlockItemPart left, BlockItemPart right) =>
            left.Bytes.SequenceEqual(right.Bytes);

        protected abstract void AssertFail(string userMessage);
    }
}
