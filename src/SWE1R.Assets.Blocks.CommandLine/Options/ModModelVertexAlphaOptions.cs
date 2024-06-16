// SPDX-License-Identifier: MIT

using CommandLine;

namespace SWE1R.Assets.Blocks.CommandLine.Options
{
    [Verb("mod-model-vertex-alpha", HelpText = "Modify a model by changing all vertices' alpha values to 128.")]
    public class ModModelVertexAlphaOptions : FilenameAndIndicesOptions { }
}
