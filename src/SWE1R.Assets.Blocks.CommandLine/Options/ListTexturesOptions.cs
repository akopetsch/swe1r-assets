// SPDX-License-Identifier: MIT

using CommandLine;

namespace SWE1R.Assets.Blocks.CommandLine.Options
{
    [Verb("list-textures", HelpText = "List texture block contents.")]
    public class ListTexturesOptions : FilenameOptions { }
}
