// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

namespace SWE1R.Assets.Blocks.ModelBlock
{
    public enum ModelType : int
    {
        MAlt = ('M' << 24) + ('A' << 16) + ('l' << 8) + ('t' << 0),
        Modl = ('M' << 24) + ('o' << 16) + ('d' << 8) + ('l' << 0),
        Part = ('P' << 24) + ('a' << 16) + ('r' << 8) + ('t' << 0),
        Podd = ('P' << 24) + ('o' << 16) + ('d' << 8) + ('d' << 0),
        Pupp = ('P' << 24) + ('u' << 16) + ('p' << 8) + ('p' << 0),
        Scen = ('S' << 24) + ('c' << 16) + ('e' << 8) + ('n' << 0),
        Trak = ('T' << 24) + ('r' << 16) + ('a' << 8) + ('k' << 0),
    }
}
