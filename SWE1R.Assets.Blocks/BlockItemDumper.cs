// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization;
using System.Collections.Generic;
using System.IO;

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

        public void DumpItemPartsBytes(BlockItem item, int i, string suffix)
        {
            Directory.CreateDirectory(DumpPath);
            for (int p = 0; p < item.Parts.Length; p++)
            {
                string path = GetPath(i, suffix, "bin", p);
                byte[] bytes = item.Parts[p].Bytes;
                File.WriteAllBytes(path, bytes);
            }
        }

        public void DumpItemLog(ByteSerializerContext context, int i, string suffix)
        {
            Directory.CreateDirectory(DumpPath);
            string path = GetPath(i, suffix, "log");
            string log = context.Log.ToString();
            File.WriteAllText(path, log);
        }

        private string GetPath(int i, string suffix, string fileExtension, int? p = null)
        {
            var fileNameParts = new List<string> {
                BlockItem.GetIndexString(i)
            };
            if (p.HasValue)
                fileNameParts.Add($"part{p}");
            if (suffix != null)
                fileNameParts.Add(suffix);
            if (fileExtension != null)
                fileNameParts.Add(fileExtension);
            var filename = string.Join('.', fileNameParts);
            return Path.Combine(DumpPath, filename.ToString());
        }
    }
}
