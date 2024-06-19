// SPDX-License-Identifier: MIT

using ByteSerialization.IO;
using CommandLine;
using SWE1R.Assets.Blocks.CommandLine.Exporters;
using SWE1R.Assets.Blocks.CommandLine.Mods;
using SWE1R.Assets.Blocks.CommandLine.Options;
using SWE1R.Assets.Blocks.ModelBlock;
using SWE1R.Assets.Blocks.SplineBlock;
using SWE1R.Assets.Blocks.SpriteBlock;
using SWE1R.Assets.Blocks.TestApp.ItemListers;
using SWE1R.Assets.Blocks.TextureBlock;
using SWE1R.Assets.Blocks.Utils;
using System.Diagnostics;

namespace SWE1R.Assets.Blocks.CommandLine
{
    public partial class Program
    {
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
            var block = BlockLoader.Load<TItem>(options.BlockPath, options.Endianness);
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
            var block = BlockLoader.Load<ModelBlockItem>(options.BlockPath, options.Endianness);
            int[] indices = GetIndices(options.Indices, block);
            foreach (int i in indices)
                new ModModelVertexAlpha(options.BlockPath, options.Endianness, i).Run();
            return ExitCodes.Success;
        }

        private static int RunScratchpadOptions(ScratchpadOptions options)
        {
            new Ps4Scratchpad().Run();
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
