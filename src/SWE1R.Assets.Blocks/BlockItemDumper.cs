// SPDX-License-Identifier: MIT

using ByteSerialization;
using System.Collections.Generic;
using System.IO;

namespace SWE1R.Assets.Blocks
{
    public class BlockItemDumper
    {
        #region Properties

        public string DumpPath { get; }
        public string Suffix { get; }

        #endregion

        #region Constructor

        public BlockItemDumper(string dumpPath, string suffix)
        {
            DumpPath = dumpPath;
            Suffix = suffix;
        }

        #endregion

        #region Methods

        public void DumpItem(BlockItem item, int i, ByteSerializerContext context)
        {
            DumpItemPartsBytes(item, i);
            DumpItemLog(context, i);
        }

        public void DumpItemPartsBytes(BlockItem item, int i)
        {
            Directory.CreateDirectory(DumpPath);
            for (int p = 0; p < item.Parts.Length; p++)
            {
                string path = GetPath(i, "bin", p);
                byte[] bytes = item.Parts[p].Bytes;
                File.WriteAllBytes(path, bytes);
            }
        }

        public void DumpItemLog(ByteSerializerContext context, int i)
        {
            Directory.CreateDirectory(DumpPath);
            string path = GetPath(i, "log");
            string log = context.Log.ToString();
            File.WriteAllText(path, log);
        }

        private string GetPath(int i, string fileExtension, int? p = null)
        {
            var fileNameParts = new List<string> {
                BlockItem.GetIndexString(i)
            };
            if (p.HasValue)
                fileNameParts.Add($"part{p}");
            if (Suffix != null)
                fileNameParts.Add(Suffix);
            if (fileExtension != null)
                fileNameParts.Add(fileExtension);
            var filename = string.Join('.', fileNameParts);
            return Path.Combine(DumpPath, filename.ToString());
        }

        #endregion
    }
}
