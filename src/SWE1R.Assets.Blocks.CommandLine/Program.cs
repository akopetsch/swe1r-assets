// SPDX-License-Identifier: MIT

using ByteSerialization.IO;
using CommandLine;
using SWE1R.Assets.Blocks.CommandLine.Exporters;
using SWE1R.Assets.Blocks.CommandLine.Mods;
using SWE1R.Assets.Blocks.ModelBlock;
using SWE1R.Assets.Blocks.SplineBlock;
using SWE1R.Assets.Blocks.SpriteBlock;
using SWE1R.Assets.Blocks.TestApp.ItemListers;
using SWE1R.Assets.Blocks.TextureBlock;
using SWE1R.Assets.Blocks.Utils;
using System.Diagnostics;

namespace SWE1R.Assets.Blocks.CommandLine
{
    public class Program
    {
        #region Clases (options)

        #region list-*

        public abstract class FilenameOptions
        {
            [Value(0)]
            public string BlockPath { get; set; }

            [Option("big-endian", Required = false, Default = true)]
            public bool BigEndian { get; set; }

            public Endianness Endianness =>
                BigEndian ? Endianness.BigEndian : Endianness.LittleEndian;
        }

        public abstract class FilenameAndIndicesOptions : FilenameOptions
        {
            [Option('i', "indices", Required = false, Separator = ',')]
            public IEnumerable<int> Indices { get; set; }
        }

        [Verb("list-models", HelpText = "List model block contents.")]
        public class ListModelsOptions : FilenameOptions { }

        [Verb("list-splines", HelpText = "List spline block contents.")]
        public class ListSplinesOptions : FilenameOptions { }

        [Verb("list-sprites", HelpText = "List sprite block contents.")]
        public class ListSpritesOptions : FilenameOptions { }

        [Verb("list-textures", HelpText = "List texture block contents.")]
        public class ListTexturesOptions : FilenameOptions { }

        #endregion

        #region dump-*

        [Verb("dump-textures", HelpText = "List texture block contents.")]
        public class DumpTexturesOptions : FilenameOptions { }

        #endregion

        #region export-*

        [Verb("export-sprites", HelpText = "Export sprites as PNG files.")]
        public class ExportSpritesOptions : FilenameAndIndicesOptions { }

        [Verb("export-models-textures", HelpText = "Export models' textures as PNG files.")]
        public class ExportModelTexturesOptions : FilenameAndIndicesOptions
        {
            [Option('t', "texture-block", Required = true)]
            public string TextureBlockPath { get; set; }
        }

        #endregion

        #region mod-*

        [Verb("mod-model-vertex-alpha", HelpText = "Modify a model by changing all vertices' alpha values to 128." )]
        public class ModModelVertexAlphaOptions : FilenameAndIndicesOptions { }

        #endregion

        #region scratchpad

        [Verb("scratchpad")]
        public class ScratchpadOptions { }

        #endregion

        #endregion

        #region Methods (main)

        public static int Main(string[] args)
        {
            int result = Parser.Default.ParseArguments<
                DumpTexturesOptions,
                ListModelsOptions,
                ListSplinesOptions,
                ListSpritesOptions,
                ListTexturesOptions,
                ExportSpritesOptions,
                ExportModelTexturesOptions,
                ModModelVertexAlphaOptions,
                ScratchpadOptions>(args)
                .MapResult(
                    (DumpTexturesOptions opts) => RunDumpTexturesOptions(opts),
                    (ListModelsOptions opts) => RunListModelsOptions(opts),
                    (ListSplinesOptions opts) => RunListSplinesOptions(opts),
                    (ListSpritesOptions opts) => RunListSpritesOptions(opts),
                    (ListTexturesOptions opts) => RunListTexturesOptions(opts),
                    (ExportSpritesOptions opts) => RunExportSpritesOptions(opts),
                    (ExportModelTexturesOptions opts) => RunExportModelsTexturesOptions(opts),
                    (ModModelVertexAlphaOptions opts) => RunModModelVertexAlphaOptions(opts), 
                    (ScratchpadOptions opts) => RunScratchpadOptions(opts),
                    errs => 1); ;
            if (Debugger.IsAttached)
                ConsoleUtil.PromptExit();
            return result;
        }

        #endregion

        private static int RunDumpTexturesOptions(DumpTexturesOptions options)
        {
            throw new NotImplementedException();
        }

        #region Methods (list-*)

        private static int RunListModelsOptions(ListModelsOptions options) =>
            RunListOptions<ModelBlockItem>(options);

        private static int RunListSplinesOptions(ListSplinesOptions options) =>
            RunListOptions<SplineBlockItem>(options);

        private static int RunListSpritesOptions(ListSpritesOptions options) =>
            RunListOptions<SpriteBlockItem>(options);

        private static int RunListTexturesOptions(ListTexturesOptions options) =>
            RunListOptions<TextureBlockItem>(options);

        private static int RunListOptions<TItem>(FilenameOptions options) where TItem : BlockItem, new()
        {
            var block = Block.Load<TItem>(options.BlockPath, Endianness.BigEndian);
            BlockItemListerFactory.Get(block, Console.WriteLine).Run();
            return ExitCodes.Success;
        }

        #endregion

        #region Methods (export-*)

        private static int RunExportSpritesOptions(ExportSpritesOptions options)
        {
            var exporter = new SpriteBlockExporter(
                options.BlockPath,
                options.Endianness,
                options.Indices.ToArray());
            exporter.Export();
            return ExitCodes.Success;
        }

        private static int RunExportModelsTexturesOptions(ExportModelTexturesOptions options)
        {
            var exporter = new ModelTexturesBlockExporter(
                options.BlockPath, 
                options.TextureBlockPath, 
                options.Endianness,
                options.Indices.ToArray());
            exporter.Export();
            return ExitCodes.Success;
        }

        private static int RunModModelVertexAlphaOptions(ModModelVertexAlphaOptions options)
        {
            var block = Block.Load<ModelBlockItem>(options.BlockPath, Endianness.BigEndian);
            int[] indices = GetIndices(options.Indices, block);
            foreach (int i in indices)
                new ModModelVertexAlpha(options.BlockPath, i).Run();
            return ExitCodes.Success;
        }

        private static int RunScratchpadOptions(ScratchpadOptions options)
        {
            new Scratchpad();
            return ExitCodes.Success;
        }

        #endregion

        #region Methods (helper)

        private static string GetExportFolderPath(string blockFilename)
        {
            var folderPath = Path.GetDirectoryName(blockFilename);
            string exportFolderName = Path.GetFileNameWithoutExtension(blockFilename);
            return Path.Combine(folderPath, exportFolderName);
        }

        private static int[] GetIndices(IEnumerable<int> indices, IBlock block)
        {
            if (indices.Count() == 0)
                return Enumerable.Range(0, block.Count).ToArray();
            else
                return indices.ToArray();
        }

        #endregion
    }
}
