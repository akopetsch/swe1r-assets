// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using Swe1rVector3Byte = SWE1R.Assets.Blocks.Vectors.Vector3Byte;
using Swe1rVector3SByte = SWE1R.Assets.Blocks.Vectors.Vector3SByte;
using Swe1rVector3Int16 = SWE1R.Assets.Blocks.Vectors.Vector3Int16;
using UnityVector3Int = UnityEngine.Vector3Int;

namespace SWE1R.Assets.Blocks.Unity.Extensions
{
    public static class Vector3IntExtensions
    {
        public static UnityVector3Int ToUnityVector3Int(this Swe1rVector3Byte source) =>
            new UnityVector3Int(source.X, source.Y, source.Z);

        public static UnityVector3Int ToUnityVector3Int(this Swe1rVector3SByte source) =>
            new UnityVector3Int(source.X, source.Y, source.Z);

        public static UnityVector3Int ToUnityVector3Int(this Swe1rVector3Int16 source) =>
            new UnityVector3Int(source.X, source.Y, source.Z);

        public static Swe1rVector3Byte ToSwe1rVector3Byte(this UnityVector3Int source) =>
            new Swe1rVector3Byte((byte)source.x, (byte)source.y, (byte)source.z);

        public static Swe1rVector3SByte ToSwe1rVector3SByte(this UnityVector3Int source) =>
            new Swe1rVector3SByte((sbyte)source.x, (sbyte)source.y, (sbyte)source.z);

        public static Swe1rVector3Int16 ToSwe1rVector3Int16(this UnityVector3Int source) =>
            new Swe1rVector3Int16((short)source.x, (short)source.y, (short)source.z);
    }
}
