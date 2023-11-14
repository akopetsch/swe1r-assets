// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using System.Collections.Generic;

namespace SWE1R.Assets.Blocks.Common.Compression
{
    public class RingBuffer<T>
    {
        public int Size { get; }
        public T[] Values { get; }
        public int ReadPosition { get; set; } = 0;
        public int WritePosition { get; set; } = 0;

        public RingBuffer(int size)
        {
            Size = size;
            Values = new T[size];
        }

        public T this[int i]
        {
            get => Values[i];
            set => Values[i] = value;
        }

        public IEnumerable<T> ReadValues(int length)
        {
            for (int i = 0; i < length; i++)
                yield return Read();
        }

        public T Read()
        {
            T value = Values[ReadPosition++];
            ReadPosition %= Size;
            return value;
        }

        public void Write(T value)
        {
            Values[WritePosition++] = value;
            WritePosition %= Size;
        }
    }
}
