// SPDX-License-Identifier: MIT

using ByteSerialization;
using System.IO;

namespace SWE1R.Assets.Blocks.SpriteBlock
{
    [BlockItemType(BlockItemType.SpriteBlockItem)]
    public class SpriteBlockItem : BlockItem<SpriteBlockItemPart>
    {
        #region Properties

        public Sprite Sprite { get; set; }

        public override BlockItemType BlockItemType =>
            BlockItemType.SpriteBlockItem;

        #endregion

        #region Constructor

        public SpriteBlockItem() : base() { }
        public SpriteBlockItem(SpriteBlockItem source) : base(source) { }

        #endregion

        #region Methods

        public override void Load(out ByteSerializerContext context)
        {
            using var ms = new MemoryStream(Bytes);
            Sprite = new ByteSerializer().Deserialize<Sprite>(ms, Block.Endianness, out context);
            Sprite.BlockItem = this;
        }

        public override void Unload() => Sprite = null;

        public override void Save(out ByteSerializerContext context)
        {
            using var ms = new MemoryStream();
            new ByteSerializer().Serialize(ms, Sprite, Block.Endianness, out context);
            Part.Load(ms.ToArray());
        }

        public override BlockItem Clone() => new SpriteBlockItem(this);

        #endregion
    }
}
