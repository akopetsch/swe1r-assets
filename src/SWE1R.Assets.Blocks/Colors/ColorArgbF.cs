// Copyright 2024 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using System;

namespace SWE1R.Assets.Blocks.Colors
{
    public struct ColorArgbF // TODO: remove ColorArgbF
    {
        #region Properties (serialized)

        // TODO: use ByteSerializer

        public float A { get; }
        public float R { get; }
        public float G { get; }
        public float B { get; }

        #endregion

        #region Properties (helper)

        public static readonly ColorArgbF Pink =
            new ColorArgbF(a: 1, r: 1, g: 0, b: 1);

        #endregion

        #region Constructor

        public ColorArgbF(float a, float r, float g, float b)
        {
            A = a;
            R = r;
            G = g;
            B = b;
        }

        #endregion

        #region Methods (: object)

        public override int GetHashCode() =>
            HashCode.Combine(R, G, B, A);

        #endregion
    }
}
