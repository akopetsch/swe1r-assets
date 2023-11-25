// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization;
using SWE1R.Assets.Blocks.Metadata.IdNames;
using SWE1R.Assets.Blocks.Original;
using SWE1R.Assets.Blocks.Original.SQLite;
using SWE1R.Assets.Blocks.Original.SQLite.Entities.SpriteBlock;
using SWE1R.Assets.Blocks.SpriteBlock;
using SWE1R.Assets.Blocks.Utils;
using System.Diagnostics;

public class Program
{
    public static int Main(string[] args)
    {
        int result = ImportSprites();
        if (Debugger.IsAttached)
            ConsoleUtils.PromptExit();
        return result;

    }

    private static int ImportSprites()
    {
        using AssetsDbContext assetsDbContext = new();

        var spriteBlock = new OriginalBlockProvider().LoadBlock<SpriteBlockItem>(SpriteBlockIdNames.Default);

        // DbSprite
        foreach (SpriteBlockItem spriteBlockItem in spriteBlock)
        {
            spriteBlockItem.Load(out ByteSerializerContext byteSerializerContext);
            Sprite sprite = spriteBlockItem.Sprite;
            var dbSprite = new DbSprite();
            dbSprite.CopyFrom(byteSerializerContext.Graph.GetValueComponent(sprite).Node);
            assetsDbContext.Sprites.Add(dbSprite);
        }
        assetsDbContext.SaveChanges();

        return ExitCodes.Success;
    }
}
