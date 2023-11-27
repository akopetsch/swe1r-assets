// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using System.IO;

namespace SWE1R.Assets.Blocks.Unity
{
    public class UnityBlockItemDumper : BlockItemDumper
    {
        public UnityBlockItemDumper(string suffix) : 
            base(Path.Combine("Temp", "dump"), suffix)
        { }
    }
}
