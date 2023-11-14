// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization;
using System.Diagnostics;

namespace SWE1R.Assets.Blocks.CommandLine.Exporters
{
    public abstract class BlockItemExporter<TItem> where TItem : BlockItem, new()
    {
        public Block<TItem> Block { get; }

        public string ExportFolderPath { get; }
        public int[] Indices { get; }

        public BlockItemExporter(string blockFilename, int[] indices)
        {
            Block = new Block<TItem>();
            Block.Load(blockFilename);

            ExportFolderPath = GetExportFolderPath(blockFilename);
            Directory.CreateDirectory(ExportFolderPath);
            Indices = GetIndices(indices);
        }

        public void Export()
        {
            foreach (int index in Indices)
            {
                Console.Write($"{GetIndexString(index)} ");
                Debug.WriteLine(GetIndexString(index));

                TItem item = Block[index];
                item.Load(out ByteSerializerContext byteSerializerContext);

                ExportItem(index, item, byteSerializerContext);

                Console.WriteLine();
                Debug.WriteLine(string.Empty);
                Debug.WriteLine(string.Empty);
            }
        }

        protected abstract void ExportItem(int index, TItem item, ByteSerializerContext byteSerializerContext);

        private string GetExportFolderPath(string blockFilename)
        {
            var folderPath = Path.GetDirectoryName(blockFilename);
            string exportFolderName = Path.GetFileNameWithoutExtension(blockFilename);
            return Path.Combine(folderPath, exportFolderName);
        }

        private int[] GetIndices(IEnumerable<int> indices)
        {
            if (indices.Count() == 0)
                return Enumerable.Range(0, Block.Count).ToArray();
            else
                return indices.ToArray();
        }

        protected string GetIndexString(int index) => $"{index:d4}";
    }
}
