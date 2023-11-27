// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using SWE1R.Assets.Blocks;
using SWE1R.Assets.Blocks.Images;
using SWE1R.Assets.Blocks.Images.SystemDrawing;
using SWE1R.Assets.Blocks.SpriteBlock;
using SWE1R.Assets.Blocks.SpriteBlock.Import;
using SWE1R.Assets.Blocks.Utils;
using System.Diagnostics;

namespace FiddleApp
{
    public class Program
    {
        public static int Main(string[] args)
        {
            int result = Fiddle();
            if (Debugger.IsAttached)
                ConsoleUtil.PromptExit();
            return result;
        }

        private static int Fiddle()
        {
            ImageRgba32 image = new SystemDrawingImageRgba32Loader().Load("sprite-133_256x128_I8.png");
            var spriteImporter = new SpriteImporter(image);
            spriteImporter.Import();
            //sprite.ExportImage().ToImageSharp().SaveAsPng("sprite-133_I8_out.png");

            var spriteBlock = Block.Load<SpriteBlockItem>(BlockDefaultFilenames.SpriteBlock);
            spriteImporter.SpriteBlockItem.Block = spriteBlock;
            spriteBlock[133] = spriteImporter.SpriteBlockItem;
            spriteBlock.Save(BlockDefaultFilenames.SpriteBlock);

            //new RoslynCodeGenerationExample();
            //new ByteSerializerSqlFiddle(typeof(Sprite));

            return ExitCodes.Success;
        }
    }
}
