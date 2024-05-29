// SPDX-License-Identifier: GPL-2.0-only

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

        public DbSpriteStructures(int blockItemValueId) : 
            base(blockItemValueId)
        { }

        #endregion

        #region Methods

        public override void Load(AssetsDbContext context)
        {
            Sprites = GetStructures(context.Sprites);
            SpritePages = GetStructures(context.SpritePages);
        }

        public override void Load(ByteSerializerGraph g)
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
