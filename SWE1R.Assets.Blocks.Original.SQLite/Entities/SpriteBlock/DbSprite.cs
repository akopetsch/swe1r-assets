// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization.Nodes;
using SWE1R.Assets.Blocks.SpriteBlock;
using System.ComponentModel.DataAnnotations.Schema;

namespace SWE1R.Assets.Blocks.Original.SQLite.Entities.SpriteBlock
{
    [Table("Sprite")]
    public class DbSprite : DbBlockItemStructure<Sprite>
    {
        #region Properties

        public short Width { get; set; }
        public short Height { get; set; }
        public short TextureFormat { get; set; }
        public short Int16_6 { get; set; }
        public int P_Palette { get; set; }
        public int? Palette_Length { get; set; }
        public short PagesCount { get; set; }
        public short Int16_E { get; set; }
        public int P_Pages { get; set; }

        #endregion

        public override void CopyFrom(Node node)
        {
            base.CopyFrom(node);

            var sprite = (Sprite)node.Value;

            Width = sprite.Width;
            Height = sprite.Height;
            TextureFormat = (byte)sprite.TextureFormat;
            Int16_6 = sprite.Word_6;
            P_Palette = GetPropertyPointer(node, nameof(Sprite.Palette));
            Palette_Length = sprite.Palette?.Length;
            PagesCount = sprite.TilesCount;
            Int16_E = sprite.Word_E;
            P_Pages = GetPropertyPointer(node, nameof(Sprite.Tiles));
        }

        public override bool Equals(DbBlockItemStructure<Sprite> other)
        {
            var _other = (DbSprite)other;

            if (!base.Equals(_other))
                return false;

            if (Width != _other.Width) return false;
            if (Height != _other.Height) return false;
            if (TextureFormat != _other.TextureFormat) return false;
            if (Int16_6 != _other.Int16_6) return false;
            if (P_Palette != _other.P_Palette) return false;
            if (Palette_Length != _other.Palette_Length) return false;
            if (PagesCount != _other.PagesCount) return false;
            if (Int16_E != _other.Int16_E) return false;
            if (P_Pages != _other.P_Pages) return false;

            return true;
        }

        public override bool Equals(object obj)
        {
            if (obj is DbSprite)
                return this.Equals((DbSprite)obj);
            else
                return base.Equals(obj);
        }

        public override int GetHashCode() =>
            HashCode.Combine(base.GetHashCode(),
                HashCode.Combine(Width, Height, TextureFormat, Int16_6, P_Palette, Palette_Length, PagesCount, Int16_E),
                HashCode.Combine(P_Pages));
    }
}
