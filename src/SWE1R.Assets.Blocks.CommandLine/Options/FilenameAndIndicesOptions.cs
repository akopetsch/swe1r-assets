// SPDX-License-Identifier: MIT

using CommandLine;

namespace SWE1R.Assets.Blocks.CommandLine.Options
{
    public abstract class FilenameAndIndicesOptions : FilenameOptions
    {
        [Option('i', "indices", Required = false, Separator = ',')]
        public IEnumerable<int> Indices { get; set; }
    }
}
