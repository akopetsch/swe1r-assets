// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization.IO;
using System;
using System.IO;
using System.Linq;

namespace SWE1R.Assets.Blocks.Common.Compression
{
    public class Compressor
    {
        #region Singleton

        public static Compressor Instance { get; } = new Compressor();
        private Compressor() { }

        #endregion

        #region Methods

        public byte[] Compress(byte[] bytes)
        {
            using (var inputStream = new MemoryStream(bytes))
            using (var outputStream = new MemoryStream())
            using (var input = new EndianBinaryReader(inputStream, Endianness.BigEndian))
            using (var output = new EndianBinaryWriter(outputStream, Endianness.BigEndian))
            {
                var wnd = new Window();
                while (true)
                {
                    var flags = (Flags)0;
                    for (int i = 0; i < 8; i++)
                    {
                        byte[] match = input.PeekBytes(LengthDistancePair.MaxLength);

                        int length = match.Length;
                        int distance = -1;
                        while (length > 3 && distance == -1)
                        {
                            match = match.Take(length--).ToArray();
                            distance = wnd.IndexOf(match);
                        }

                        if (distance == -1)
                        {
                            flags[i] = Flag.Literal;

                            byte b = input.ReadByte();
                            wnd.Write(b);
                            output.Write(b);
                        }
                        else
                        {
                            flags[i] = Flag.LengthDistancePair;



                            // match
                        }
                    }
                    
                    throw new NotImplementedException();
                }
            }
        }

        public byte[] Decompress(byte[] bytes)
        {
            using (var inputStream = new MemoryStream(bytes))
            using (var outputStream = new MemoryStream())
            using (var input = new EndianBinaryReader(inputStream, Endianness.BigEndian))
            using (var output = new EndianBinaryWriter(outputStream, Endianness.BigEndian))
            {
                var wnd = new Window();
                while (true)
                {
                    var flags = (Flags)input.ReadByte();
                    foreach (Flag f in flags)
                    {
                        if (f == Flag.Literal)
                        {
                            byte b = input.ReadByte();
                            wnd.Write(b);
                            output.Write(b);
                        }
                        else
                        {
                            var pair = (LengthDistancePair)input.ReadUInt16();
                            if (pair.Distance == 0)
                                return outputStream.ToArray();

                            wnd.ReadPosition = pair.Distance;
                            foreach (byte b in wnd.ReadValues(pair.Length))
                            {
                                wnd.Write(b);
                                output.Write(b);
                            }
                        }
                    }
                }
            }
        }
        
        #endregion
    }
}
