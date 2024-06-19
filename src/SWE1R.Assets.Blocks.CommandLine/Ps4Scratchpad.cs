// SPDX-License-Identifier: MIT

using ByteSerialization;
using ByteSerialization.IO;
using SWE1R.Assets.Blocks.ModelBlock;

namespace SWE1R.Assets.Blocks.CommandLine
{
    internal class Ps4Scratchpad
    {
        public void Run()
        {
            string dir = @"C:\Users\akope\code\swe1r-ps4\x-eur\inner-pfs-image\uroot\data\lev01";
            string filename = "out_modelblock_64.bin";
            string path = Path.Combine(dir, filename);
            var modelBlock = BlockLoader.Load<ModelBlockItem>(path, Endianness.LittleEndian);

            ModelBlockItem modelBlockItem = modelBlock[0];
            modelBlockItem.Load(out ByteSerializerContext loadContext);
            File.WriteAllBytes($"{path}.{0}.mask.bin", modelBlockItem.Bitmask.Bytes);
            File.WriteAllBytes($"{path}.{0}.data.bin", modelBlockItem.Data.Bytes);
            File.WriteAllText($"{path}.{0}.log", loadContext.Log.ToString());

            modelBlockItem.Save();
        }
    }
}
