// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization;
using System.IO;
using System.Text;

namespace SWE1R.Assets.Blocks
{
    public class BlockItemDumper
    {
        public string DumpPath { get; }

        public BlockItemDumper(string dumpPath) =>
            DumpPath = dumpPath;

        public void DumpItem(BlockItem item, int i, ByteSerializerContext context, string suffix)
        {
            DumpItemPartsBytes(item, i, suffix);
            DumpItemLog(context, i, suffix);
        }

        public void DumpItemPartsBytes(BlockItem model, int i, string suffix)
        {
            Directory.CreateDirectory(DumpPath);
            for (int p = 0; p < model.Parts.Length; p++)
            {
                string path = GetPath(i, "bin", suffix, p);
                byte[] bytes = model.Parts[p].Bytes;
                File.WriteAllBytes(path, bytes);
            }
        }

        public void DumpItemLog(ByteSerializerContext context, int i, string suffix)
        {
            Directory.CreateDirectory(DumpPath);
            string path = GetPath(i, "log", suffix);
            string log = context.Log.ToString();
            File.WriteAllText(path, log);
        }

        private string GetPath(int i, string fileExtension, string suffix, int? p = null)
        {
            var filename = new StringBuilder();
            filename.Append($"{i:d4}.");
            if (p.HasValue)
                filename.Append($"part{p}.");
            filename.Append($"{suffix}.{fileExtension}");
            return Path.Combine(DumpPath, filename.ToString());
        }
    }
}
