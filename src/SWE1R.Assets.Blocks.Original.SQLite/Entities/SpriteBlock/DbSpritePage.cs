// Copyright 2024 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization.Nodes;
using SWE1R.Assets.Blocks.SpriteBlock;
using System.ComponentModel.DataAnnotations.Schema;

namespace SWE1R.Assets.Blocks.Original.SQLite.Entities.SpriteBlock
{
    [Table("SpritePage")]
    public class DbSpritePage : DbBlockItemStructure<SpriteTile>
    {
        #region Properties

        public short Width { get; set; }
        public short Height { get; set; }
        public int P_Pixels { get; set; }
        public int Pixels_Length { get; set; }

        #endregion

        public override void CopyFrom(Node node)
        {
            base.CopyFrom(node);

            var spritePage = (SpriteTile)node.Value;

            Width = spritePage.Width;
            Height = spritePage.Height;
            P_Pixels = GetPropertyPointer(node, nameof(SpriteTile.PixelsBytes));
            Pixels_Length = spritePage.PixelsBytes.Length;
        }

        public override bool Equals(DbBlockItemStructure<SpriteTile> other)
        {
            var _other = (DbSpritePage)other;

            if (!base.Equals(_other))
                return false;

            if (Width != _other.Width) return false;
            if (Height != _other.Height) return false;
            if (P_Pixels != _other.P_Pixels) return false;
            if (Pixels_Length != _other.Pixels_Length) return false;

            return true;
        }

        public override bool Equals(object obj)
        {
            if (obj is DbSpritePage)
                return this.Equals((DbSpritePage)obj);
            else
                return base.Equals(obj);
        }

        public override int GetHashCode() =>
            HashCode.Combine(base.GetHashCode(),
                HashCode.Combine(Width, Height, P_Pixels, Pixels_Length));
    }
}
