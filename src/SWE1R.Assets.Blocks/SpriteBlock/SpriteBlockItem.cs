// SPDX-License-Identifier: GPL-2.0-only

using ByteSerialization;
using ByteSerialization.IO;
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
            Sprite = new ByteSerializer().Deserialize<Sprite>(ms, Endianness.BigEndian, out context);
            Sprite.BlockItem = this;
        }

        public override void Unload() => Sprite = null;

        public override void Save(out ByteSerializerContext context)
        {
            using var ms = new MemoryStream();
            new ByteSerializer().Serialize(ms, Sprite, Endianness.BigEndian, out context);
            Part.Load(ms.ToArray());
        }

        public override BlockItem Clone() => new SpriteBlockItem(this);

        #endregion
    }
}
