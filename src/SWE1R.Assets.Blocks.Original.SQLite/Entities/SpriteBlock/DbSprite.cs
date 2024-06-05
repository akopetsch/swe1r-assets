// SPDX-License-Identifier: MIT

using ByteSerialization.Nodes;
using SWE1R.Assets.Blocks.SpriteBlock;
using System.ComponentModel.DataAnnotations.Schema;

namespace SWE1R.Assets.Blocks.Original.SQLite.Entities.SpriteBlock
{
    [Table(nameof(Sprite))]
    public class DbSprite : DbBlockItemStructure<Sprite>
    {
        #region Properties

        public short Width { get; set; }
        public short Height { get; set; }
        public short Format { get; set; }
        public short Word_6 { get; set; }
        public int P_Palette { get; set; }
        public int? Palette_Length { get; set; }
        public short TilesCount { get; set; }
        public short Word_E { get; set; }
        public int P_Tiles { get; set; }

        #endregion

        public override void CopyFrom(Node node)
        {
            base.CopyFrom(node);

            var x = (Sprite)node.Value;

            Width = x.Width;
            Height = x.Height;
            Format = (byte)x.Format;
            Word_6 = x.Word_6;
            P_Palette = GetPropertyPointer(node, nameof(Sprite.Palette));
            Palette_Length = x.Palette?.Length;
            TilesCount = x.TilesCount;
            Word_E = x.Word_E;
            P_Tiles = GetPropertyPointer(node, nameof(Sprite.Tiles));
        }

        public override bool Equals(DbBlockItemStructure<Sprite> other)
        {
            var x = (DbSprite)other;

            if (!base.Equals(x))
                return false;

            if (Width != x.Width) return false;
            if (Height != x.Height) return false;
            if (Format != x.Format) return false;
            if (Word_6 != x.Word_6) return false;
            if (P_Palette != x.P_Palette) return false;
            if (Palette_Length != x.Palette_Length) return false;
            if (TilesCount != x.TilesCount) return false;
            if (Word_E != x.Word_E) return false;
            if (P_Tiles != x.P_Tiles) return false;

            return true;
        }

        public override bool Equals(object obj)
        {
            if (obj is DbSprite x)
                return Equals(x);
            else
                return base.Equals(obj);
        }

        public override int GetHashCode() =>
            CombineHashCodes(base.GetHashCode(), 
                Width, Height, Format, Word_6, P_Palette, Palette_Length, TilesCount, Word_E, P_Tiles);
    }
}
