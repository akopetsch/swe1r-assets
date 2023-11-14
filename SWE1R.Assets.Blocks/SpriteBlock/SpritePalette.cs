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
        #region Properties (serialization)

        [Order(0), Length(typeof(PaletteLengthHelper))]
        private short[] Data { get; set; }

        #endregion

        #region Properties

        public int Length => Data.Length;

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

        #region Indexers

        public ColorArgbF this[int index]
        {
            get => ColorArgbF.FromRgba5551(Data[index]);
            //set => Data[index] = value;
        }

        #endregion
    }
}
