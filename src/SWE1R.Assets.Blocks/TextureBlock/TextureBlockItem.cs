// SPDX-License-Identifier: GPL-2.0-only

using ByteSerialization;
using SWE1R.Assets.Blocks.Colors;
using System;

namespace SWE1R.Assets.Blocks.TextureBlock
{
    [BlockItemType(BlockItemType.TextureBlockItem)]
    public class TextureBlockItem : BlockItem<TextureBlockItemPixelsPart, TextureBlockItemPalettePart>
    {
        #region Properties

        public TextureBlockItemPixelsPart PixelsPart => Part1;
        public TextureBlockItemPalettePart PalettePart => Part2;

        public ColorRgba5551[] PaletteColors { get; set; }

        public override BlockItemType BlockItemType =>
            BlockItemType.TextureBlockItem;

        #endregion

        #region Constructor

        public TextureBlockItem() : base() { }
        public TextureBlockItem(TextureBlockItem source) : base(source) { }

        #endregion

        #region Methods

        public override void Load(out ByteSerializerContext context)
        {
            context = null;
            PaletteColors = PalettePart.GetColors();
        }

        public override void Unload() => PaletteColors = null;

        public override void Save(out ByteSerializerContext context) => 
            throw new NotImplementedException();

        public override BlockItem Clone() => new TextureBlockItem(this);

        #endregion
    }
}
