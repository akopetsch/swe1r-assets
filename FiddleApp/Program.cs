// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using SWE1R.Assets.Blocks;
using SWE1R.Assets.Blocks.Images;
using SWE1R.Assets.Blocks.Images.SystemDrawing;
using SWE1R.Assets.Blocks.Metadata.IdNames;
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
            int result = Fiddle3();
            if (Debugger.IsAttached)
                ConsoleUtil.PromptExit();
            return result;
        }

        private static int Fiddle3()
        {
            var testClassGenerator = new TestClassGenerator();
            testClassGenerator.Generate();
            return ExitCodes.Success;
        }

        private static int Fiddle2()
        {
            var metadataFoo = new MetadataGenerator();
            metadataFoo.Generate();
            return ExitCodes.Success;
        }

        private static int Fiddle()
        {
            var spriteBlock = Block.Load<SpriteBlockItem>(SpriteBlockIdNames.Default);

            // import sprite
            ImageRgba32 image = new SystemDrawingImageRgba32Loader().Load("sprite-133_256x128_I8.png");
            var spriteImporter = new SpriteImporter(image);
            spriteImporter.Import();
            spriteImporter.SpriteBlockItem.Block = spriteBlock;
            spriteBlock[133] = spriteImporter.SpriteBlockItem;

            spriteBlock.Save(BlockDefaultFilenames.SpriteBlock);

            //new RoslynCodeGenerationExample();
            //new ByteSerializerSqlFiddle(typeof(Sprite));

            return ExitCodes.Success;
        }
    }
}
