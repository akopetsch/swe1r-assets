// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization;

namespace SWE1R.Assets.Blocks.TestUtils
{
    public abstract class BlockItemsReserializationTestBase<TItem> :
        BlockItemsTestBase<TItem> where TItem : BlockItem, new()
    {
        protected BlockItemsReserializationTestBase(Block<TItem> block) : base(block) { }

        protected override void CompareItemInternal(int i)
        {
            // deserialize
            TItem model = DeserializeItem(i, out ByteSerializerContext inContext);
            byte[][] inputPartsBytes = model.Parts.Select(p => p.Bytes.ToArray()).ToArray();

            // re-serialize
            SerializeItem(model, i, out ByteSerializerContext outContext);
            byte[][] outputPartsBytes = model.Parts.Select(p => p.Bytes.ToArray()).ToArray();

            // compare
            for (int p = 0; p < model.Parts.Length; p++)
            {
                byte[] inputBytes = inputPartsBytes[p];
                byte[] outputBytes = outputPartsBytes[p];
                bool areEqual = inputBytes.SequenceEqual(outputBytes);
                AssertEquality(p, areEqual);
            }
        }

        protected abstract void AssertEquality(int p, bool areEqual);
    }
}
