// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using CommandLine;
using SWE1R.Assets.Blocks.CommandLine.Exporters;
using SWE1R.Assets.Blocks.CommandLine.Mods;
using SWE1R.Assets.Blocks.ModelBlock;
using SWE1R.Assets.Blocks.SplineBlock;
using SWE1R.Assets.Blocks.SpriteBlock;
using SWE1R.Assets.Blocks.TestApp.ItemListers;
using SWE1R.Assets.Blocks.TextureBlock;
using System.Diagnostics;

namespace SWE1R.Assets.Blocks.CommandLine
{
    public class Program
    {
        #region Constants

        private const int OK = 0;

        #endregion

        #region Clases (options)

        #region list-*

        public abstract class FilenameOptions
        {
            [Value(0)]
            public string BlockPath { get; set; }
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

        #endregion

        #region Methods (main)

        public static int Main(string[] args)
        {
            new TextureImporter();
            return 0;
            int result = Parser.Default.ParseArguments<
                DumpTexturesOptions,
                ListModelsOptions, 
                ListSplinesOptions, 
                ListSpritesOptions,
                ListTexturesOptions,
                ExportSpritesOptions,
                ExportModelTexturesOptions,
                ModModelVertexAlphaOptions>(args)
                .MapResult(
                    (DumpTexturesOptions opts) => RunDumpTexturesOptions(opts),
                    (ListModelsOptions opts) => RunListModelsOptions(opts),
                    (ListSplinesOptions opts) => RunListSplinesOptions(opts),
                    (ListSpritesOptions opts) => RunListSpritesOptions(opts),
                    (ListTexturesOptions opts) => RunListTexturesOptions(opts),
                    (ExportSpritesOptions opts) => RunExportSpritesOptions(opts),
                    (ExportModelTexturesOptions opts) => RunExportModelsTexturesOptions(opts),
                    errs => 1);
            if (Debugger.IsAttached)
                PromptExit();
            return result;
        }

        #endregion

        private static int RunDumpTexturesOptions(DumpTexturesOptions options)
        {
            throw new NotImplementedException();
        }

        #region Methods (list-*)

        private static int RunListModelsOptions(ListModelsOptions options) =>
            RunListOptions<Model>(options);

        private static int RunListSplinesOptions(ListSplinesOptions options) =>
            RunListOptions<Spline>(options);

        private static int RunListSpritesOptions(ListSpritesOptions options) =>
            RunListOptions<Sprite>(options);

        private static int RunListTexturesOptions(ListTexturesOptions options) =>
            RunListOptions<Texture>(options);

        private static int RunListOptions<TItem>(FilenameOptions options) where TItem : BlockItem, new()
        {
            var block = Block.Load<TItem>(options.BlockPath);
            BlockItemListerFactory.Get(block, Console.WriteLine).Run();
            return OK;
        }

        #endregion

        #region Methods (export-*)

        public static int RunExportSpritesOptions(ExportSpritesOptions options)
        {
            var exporter = new SpriteExporter(options.BlockPath, options.Indices.ToArray());
            exporter.Export();
            return OK;
        }

        public static int RunExportModelsTexturesOptions(ExportModelTexturesOptions options)
        {
            var exporter = new ModelTexturesExporter(options.BlockPath, options.TextureBlockPath, options.Indices.ToArray());
            exporter.Export();
            return OK;
        }

        public static int RunModModelVertexAlphaOptions(ModModelVertexAlphaOptions options)
        {
            var block = Block.Load<Model>(options.BlockPath);
            int[] indices = GetIndices(options.Indices, block);

            foreach (int i in indices)
                new ModModelVertexAlpha(options.BlockPath, i).Run();

            return OK;
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

        private static void PromptExit()
        {
            Console.Write("Press enter to exit.");
            while (Console.ReadKey().Key != ConsoleKey.Enter) ;
            Console.WriteLine();
        }

        #endregion
    }
}
