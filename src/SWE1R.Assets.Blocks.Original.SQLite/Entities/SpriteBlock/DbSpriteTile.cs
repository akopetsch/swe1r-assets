// SPDX-License-Identifier: MIT

using ByteSerialization.Nodes;
using SWE1R.Assets.Blocks.SpriteBlock;
using System.ComponentModel.DataAnnotations.Schema;

namespace SWE1R.Assets.Blocks.Original.SQLite.Entities.SpriteBlock
{
    [Table($"{nameof(Sprite)}_{nameof(SpriteTile)}")]
    public class DbSpriteTile : DbBlockItemStructure<SpriteTile>
    {
        #region Properties

        public short Width { get; set; }
        public short Height { get; set; }
        public int P_PixelsBytes { get; set; }
        public int PixelsBytes_Length { get; set; }

        #endregion

        public override void CopyFrom(Node node)
        {
            base.CopyFrom(node);

            var x = (SpriteTile)node.Value;

            Width = x.Width;
            Height = x.Height;
            P_PixelsBytes = GetPropertyPointer(node, nameof(SpriteTile.PixelsBytes));
            PixelsBytes_Length = x.PixelsBytes.Length;
        }

        public override bool Equals(DbBlockItemStructure<SpriteTile> other)
        {
            var x = (DbSpriteTile)other;

            if (!base.Equals(x))
                return false;

            if (Width != x.Width) return false;
            if (Height != x.Height) return false;
            if (P_PixelsBytes != x.P_PixelsBytes) return false;
            if (PixelsBytes_Length != x.PixelsBytes_Length) return false;

            return true;
        }

        public override bool Equals(object obj)
        {
            if (obj is DbSpriteTile x)
                return Equals(x);
            else
                return base.Equals(obj);
        }

        public override int GetHashCode() =>
            CombineHashCodes(base.GetHashCode(),
                Width, Height, P_PixelsBytes, PixelsBytes_Length);
    }
}
