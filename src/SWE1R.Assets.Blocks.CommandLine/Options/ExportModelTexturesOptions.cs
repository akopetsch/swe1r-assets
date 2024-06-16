// SPDX-License-Identifier: MIT

using CommandLine;

namespace SWE1R.Assets.Blocks.CommandLine.Options
{
    [Verb("export-models-textures", HelpText = "Export models' textures as PNG files.")]
    public class ExportModelTexturesOptions : FilenameAndIndicesOptions
    {
        [Option('t', "texture-block", Required = true)]
        public string TextureBlockPath { get; set; }
    }
}
