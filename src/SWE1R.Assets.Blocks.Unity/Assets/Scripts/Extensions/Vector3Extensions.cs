// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using Swe1rVector3Float = SWE1R.Assets.Blocks.Vectors.Vector3Single;
using Swe1rVector3Int16 = SWE1R.Assets.Blocks.Vectors.Vector3Int16;
using UnityVector3 = UnityEngine.Vector3;

namespace SWE1R.Assets.Blocks.Unity.Extensions
{
    public static class Vector3Extensions
    {
        public static UnityVector3 ToUnityVector3(this Swe1rVector3Int16 source) =>
            new UnityVector3(source.X, source.Y, source.Z);

        public static UnityVector3 ToUnityVector3(this Swe1rVector3Float source) =>
            new UnityVector3(source.X, source.Y, source.Z);
        
        public static Swe1rVector3Int16 ToSwe1rVector3Int16(this UnityVector3 source) =>
            new Swe1rVector3Int16((short)source.x, (short)source.y, (short)source.z);

        public static Swe1rVector3Float ToSwe1rVector3Single(this UnityVector3 source) =>
            new Swe1rVector3Float(source.x, source.y, source.z);
    }
}
