// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

namespace SWE1R.Assets.Blocks.CommandLine
{
    public class MaterialImporterException : Exception
    {
        public MaterialImporterException() : base() { }

        public MaterialImporterException(string message) : base(message) { }
    }
}
