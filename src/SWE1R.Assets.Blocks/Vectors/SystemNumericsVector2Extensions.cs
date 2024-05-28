// Copyright 2024 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using System;
using System.Numerics;

namespace SWE1R.Assets.Blocks.Vectors
{
    public static class SystemNumericsVector2Extensions
    {
        public static bool Contains(this Vector2 vector2, Vector2 other) =>
            vector2.X >= other.X &&
            vector2.Y >= other.Y;

        public static Vector2 ScaleWithinBounds(this Vector2 vector2, Vector2 bounds)
        {
            double xScale = (double)bounds.X / vector2.X;
            double yScale = (double)bounds.Y / vector2.Y;
            double scaleFactor = Math.Min(xScale, yScale);
            double scaledX = vector2.X * scaleFactor;
            double scaledY = vector2.Y * scaleFactor;
            return new Vector2((float)scaledX, (float)scaledY);
        }

        public static Vector2 ScaleWithinBounds(this Vector2 vector2, float boundsX, float boundsY) =>
            vector2.ScaleWithinBounds(new Vector2(boundsX, boundsY));
    }
}
