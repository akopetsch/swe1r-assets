// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization.IO.Utils;
using Xunit;

namespace ByteSerialization.IO.Tests
{
    public class BitsHelperTest
    {
        #region Methods ([Fact])

        [Fact]
        public void Test_GetBits_0x01_LsbFirst()
        {
            bool[] expected = GetAllZeroesBitsArray();
            expected[0] = true;
            bool[] actual = BitsHelper.GetBits(0x01, BitOrder.LsbFirst);
            Assert.True(expected.SequenceEqual(actual));
        }

        [Fact]
        public void Test_GetBits_0x01_MsbFirst()
        {
            bool[] expected = GetAllZeroesBitsArray();
            expected[7] = true;
            bool[] actual = BitsHelper.GetBits(0x01, BitOrder.MsbFirst);
            Assert.True(expected.SequenceEqual(actual));
        }

        [Fact]
        public void Test_GetBits_0x80_MsbFirst()
        {
            bool[] expected = GetAllZeroesBitsArray();
            expected[0] = true;
            bool[] actual = BitsHelper.GetBits(0x80, BitOrder.MsbFirst);
            Assert.True(expected.SequenceEqual(actual));
        }

        [Fact]
        public void Test_GetBits_0xFF()
        {
            bool[] expected = GetAllOnesBitsArray();
            bool[] actual = BitsHelper.GetBits(0xFF, BitOrder.MsbFirst);
            Assert.True(expected.SequenceEqual(actual));
        }

        [Fact]
        public void Test_GetBitMask_1_MsbFirst() =>
            Assert.Equal(
                expected: 0x80, 
                actual: BitsHelper.GetBitMask(0, BitOrder.MsbFirst));

        [Fact]
        public void Test_GetBitMask_1_LsbFirst() =>
            Assert.Equal(
                expected: 0x01, 
                actual: BitsHelper.GetBitMask(0, BitOrder.LsbFirst));

        #endregion

        #region Methods (helper)

        private static bool[] GetBitsArray(
            bool value0, bool value1, bool value2, bool value3, 
            bool value4, bool value5, bool value6, bool value7) =>
            new bool[] { 
                value0, value1, value2, value3, 
                value4, value5, value6, value7 };

        private static bool[] GetAllZeroesBitsArray() =>
            Enumerable.Repeat(false, BitsHelper.BitsPerByte).ToArray();

        private static bool[] GetAllOnesBitsArray() =>
            Enumerable.Repeat(true, BitsHelper.BitsPerByte).ToArray();

        #endregion
    }
}
