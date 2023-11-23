// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization.Attributes;
using ByteSerialization.Components.Values;
using ByteSerialization.Components.Values.Composites.Records;
using SWE1R.Assets.Blocks.Common.Colors;

namespace SWE1R.Assets.Blocks.SpriteBlock
{
    public class SpritePalette
    {
        #region Properties (serialized)

        [Order(0), Length(typeof(PaletteLengthHelper))]
        public ColorRgba5551[] Colors { get; set; }

        #endregion

        #region Properties (serialization)

        public int Length => Colors.Length;

        #endregion

        #region Classes (serialization)

        private class PaletteLengthHelper : IBindingHelper
        {
            public int GetValue(PropertyComponent p)
            {
                var spriteData = p.GetAncestorValue<SpriteData>();
                if (spriteData.Format == 2 && spriteData.PageWidthAlignment == 0)
                    return 16;
                else if (spriteData.Format == 2 && spriteData.PageWidthAlignment == 1)
                    return 256;
                else
                    return 0;
            }
        }

        #endregion
    }
}
