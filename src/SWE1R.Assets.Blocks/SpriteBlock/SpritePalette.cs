// SPDX-License-Identifier: GPL-2.0-only

using ByteSerialization.Attributes;
using ByteSerialization.Components.Values;
using ByteSerialization.Components.Values.Composites.Records;
using SWE1R.Assets.Blocks.Colors;
using SWE1R.Assets.Blocks.Textures;

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
            public int GetValue(PropertyComponent p) =>
                p.GetAncestorValue<Sprite>().Format.GetPaletteSize();
        }

        #endregion
    }
}
