// SPDX-License-Identifier: MIT

using CommandLine;

namespace SWE1R.Assets.Blocks.CommandLine.Options
{
    [Verb("list-splines", HelpText = "List spline block contents.")]
    public class ListSplinesOptions : FilenameOptions { }
}
