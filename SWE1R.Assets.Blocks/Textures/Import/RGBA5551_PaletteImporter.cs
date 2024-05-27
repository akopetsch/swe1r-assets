// Copyright 2024 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization.IO;
using SWE1R.Assets.Blocks.Colors;
using System;
using System.IO;
using System.Linq;

namespace SWE1R.Assets.Blocks.Textures.Import
{
    public class RGBA5551_PaletteImporter
    {
        #region Properties (input)

        public ColorRgba32[] InputPalette { get; }

        #endregion

        #region Properties (output)

        public ColorRgba5551[] OutputPalette { get; private set; }
        public byte[] OutputBytes { get; private set; }

        #endregion

        #region Constructor

        public RGBA5551_PaletteImporter(ColorRgba32[] inputPalette)
        {
            InputPalette = inputPalette;
        }

        #endregion

        #region Methods

        public void Import()
        {
            // output palette
            OutputPalette = new ColorRgba5551[GetPaletteSize()];
            for (int i = 0; i < OutputPalette.Length; i++)
                OutputPalette[i] = new ColorRgba5551(); // TODO: !!! use structs to make array init unnecessary
            Array.Copy(InputPalette.Select(x => (ColorRgba5551)x).ToArray(), OutputPalette, InputPalette.Length);

            // output bytes
            using var ms = new MemoryStream();
            using var w = new EndianBinaryWriter(ms, Endianness.BigEndian);
            foreach (ColorRgba5551 rgba5551 in OutputPalette)
                rgba5551.Serialize(w);
            OutputBytes = ms.ToArray();
        }

        private int GetPaletteSize()
        {
            int i4Size = (1 << 4); // = 16
            int i8Size = (1 << 8); // = 256
            if (InputPalette.Length <= i4Size)
                return i4Size;
            else if (InputPalette.Length <= i8Size)
                return i8Size;
            else
                throw new InvalidOperationException();
        }

        #endregion
    }
}
