// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization.IO.Extensions;
using System;

namespace SWE1R.Assets.Blocks.TextureBlock
{
    public class TextureBlockItemPixelsPart : BlockItemPart
    {
        #region Properties (helper)

        public int NibblesCount => Bytes.Length * 2;

        #endregion

        #region Constructor

        public TextureBlockItemPixelsPart() : base() { }
        private TextureBlockItemPixelsPart(TextureBlockItemPixelsPart source) : base(source) { }

        #endregion

        #region Methods (helper)

        public int GetNibble(int i) => Bytes.GetNibble(i);
        public byte GetByte(int i) => Bytes[i];
        public int GetInt32(int i) => BitConverter.ToInt32(Bytes, i * sizeof(int)); // TODO: is big endian ensured?

        #endregion

        #region Methods (: BlockItemPart

        public override BlockItemPart Clone() => new TextureBlockItemPixelsPart(this);

        #endregion
    }
}
