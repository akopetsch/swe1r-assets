// SPDX-License-Identifier: MIT

using ByteSerialization.IO;
using CommandLine;

namespace SWE1R.Assets.Blocks.CommandLine.Options
{
    public abstract class FilenameOptions
    {
        [Value(0)]
        public string BlockPath { get; set; }

        [Option("little-endian", Required = false, Default = false)]
        public bool LittleEndian { get; set; }

        public Endianness Endianness =>
            LittleEndian ? Endianness.LittleEndian : Endianness.BigEndian;
    }
}
