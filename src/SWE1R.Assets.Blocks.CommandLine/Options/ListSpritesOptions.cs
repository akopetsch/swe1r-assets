// SPDX-License-Identifier: MIT

using CommandLine;

namespace SWE1R.Assets.Blocks.CommandLine.Options
{
    [Verb("list-sprites", HelpText = "List sprite block contents.")]
    public class ListSpritesOptions : FilenameOptions { }
}
