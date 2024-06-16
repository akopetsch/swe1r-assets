// SPDX-License-Identifier: MIT

using CommandLine;

namespace SWE1R.Assets.Blocks.CommandLine.Options
{
    [Verb("list-models", HelpText = "List model block contents.")]
    public class ListModelsOptions : FilenameOptions { }
}
