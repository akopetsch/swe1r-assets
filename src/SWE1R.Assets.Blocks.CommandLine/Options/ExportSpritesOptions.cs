// SPDX-License-Identifier: MIT

using CommandLine;

namespace SWE1R.Assets.Blocks.CommandLine.Options
{
    [Verb("export-sprites", HelpText = "Export sprites as PNG files.")]
    public class ExportSpritesOptions : FilenameAndIndicesOptions { }
}
