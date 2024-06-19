﻿// SPDX-License-Identifier: MIT

using ByteSerialization.IO;
using SWE1R.Assets.Blocks;
using SWE1R.Assets.Blocks.Images;
using SWE1R.Assets.Blocks.Images.SystemDrawing;
using SWE1R.Assets.Blocks.Metadata.IdNames;
using SWE1R.Assets.Blocks.Original.SQLite.CodeGen;
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
            int result = Fiddle0();
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

        private static int Fiddle1()
        {
            var spriteBlock = BlockLoader.Load<SpriteBlockItem>(SpriteBlockIdNames.Default, Endianness.BigEndian);

            // import sprite
            using var stream = File.OpenRead("sprite-133_256x128_I8.png");
            ImageRgba32 image = new SystemDrawingImageRgba32Loader().Load(stream);
            var spriteImporter = new SpriteImporter(image, Endianness.BigEndian);
            spriteImporter.Import();
            spriteImporter.SpriteBlockItem.Block = spriteBlock;
            spriteBlock[133] = spriteImporter.SpriteBlockItem;

            spriteBlock.Save(BlockDefaultFilenames.SpriteBlock);

            return ExitCodes.Success;
        }

        private static int Fiddle0()
        {
            var generator = new DbEntityClassGenerator(typeof(Sprite));
            generator.Generate();
            Console.WriteLine(generator.Code);
            return ExitCodes.Success;
        }
    }
}
