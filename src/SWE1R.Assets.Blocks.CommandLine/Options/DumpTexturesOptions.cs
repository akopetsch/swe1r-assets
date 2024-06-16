// SPDX-License-Identifier: MIT

using CommandLine;

namespace SWE1R.Assets.Blocks.CommandLine.Options
{
    [Verb("dump-textures", HelpText = "List texture block contents.")]
    public class DumpTexturesOptions : FilenameOptions { }
}
