// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization.IO.Extensions;
using Xunit;

namespace ByteSerialization.IO.Tests
{
    public class SwapBytesTest
    {
        [Fact]
        public void Test1()
        {
            Assert.Equal(-573785174, (-1430532899).SwapBytes());
            Assert.Equal(0xAABBCCDD, 0xDDCCBBAA.SwapBytes());
            Assert.Equal(-16224, ((short)-24384).SwapBytes());
            Assert.Equal(0xC0A0, ((ushort)0xA0C0).SwapBytes());
            Assert.Equal((ulong)0x2211ffeeddccbbaa, 0xaabbccddeeff1122.SwapBytes());
        }
    }
}
