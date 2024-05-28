// Copyright 2024 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using System.Linq;

namespace SWE1R.Assets.Blocks.Common.Compression
{
    public class Window : RingBuffer<byte>
    {
        public Window() : 
            base(LengthDistancePair.MaxDistance)
        {
            ReadPosition = 0;
            WritePosition = 1;
        }

        public int IndexOf(byte[] bytes)
        {
            byte[] unwinded = Values.Concat(Values.Take(LengthDistancePair.MaxLength - 1)).ToArray();
            return unwinded.IndexOf(0, bytes);
        }
    }
}
