// SPDX-License-Identifier: MIT

using ByteSerialization;
using System.Diagnostics;

namespace SWE1R.Assets.Blocks.CommandLine.Exporters
{
    public abstract class BlockExporter<TItem> where TItem : BlockItem, new()
    {
        public Block<TItem> Block { get; }

        public string ExportFolderPath { get; }
        public int[] Indices { get; }

        public BlockExporter(string blockPath, int[] indices)
        {
            Block = new Block<TItem>();
            Block.Load(blockPath);

            ExportFolderPath = GetExportFolderPath(blockPath);
            Directory.CreateDirectory(ExportFolderPath);
            Indices = GetIndices(indices);
        }

        public void Export()
        {
            foreach (int index in Indices)
            {
                Console.Write($"{BlockItem.GetIndexString(index)} ");
                Debug.WriteLine(BlockItem.GetIndexString(index));

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
    }
}
