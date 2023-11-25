// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization.Nodes;
using SWE1R.Assets.Blocks.SpriteBlock;

namespace SWE1R.Assets.Blocks.Original.SQLite.Entities.SpriteBlock
{
    public class DbSpriteStructures : DbBlockItemStructures, IEquatable<DbSpriteStructures>
    {
        #region Properties

        public List<DbSprite> Sprites { get; set; }
        public List<DbSpritePage> SpritePages { get; set; }

        #endregion

        #region Constructor

        public DbSpriteStructures(int blockItemIndex) : 
            base(blockItemIndex)
        { }

        #endregion

        #region Methods

        public override void Load(AssetsDbContext context)
        {
            Sprites = GetStructures(context.Sprites);
            SpritePages = GetStructures(context.SpritePages);
        }

        public override void Load(Graph g)
        {
            Sprites = GetStructures<Sprite, DbSprite>(g);
            SpritePages = GetStructures<SpriteTile, DbSpritePage>(g);
        }

        public bool Equals(DbSpriteStructures other)
        {
            if (!Sprites.SequenceEqual(other.Sprites)) return false;
            if (!SpritePages.SequenceEqual(other.SpritePages)) return false;

            return true;
        }

        #endregion
    }
}
