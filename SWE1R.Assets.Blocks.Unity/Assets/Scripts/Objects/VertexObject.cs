// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using SWE1R.Assets.Blocks.Unity.Extensions;
using System;
using Swe1rVertex = SWE1R.Assets.Blocks.ModelBlock.Meshes.Vertex;
using UnityVectorInt = UnityEngine.Vector3Int;

namespace SWE1R.Assets.Blocks.Unity.Objects
{
    [Serializable]
    public class VertexObject
    {
        public UnityVectorInt position;
        public short u;
        public short v;
        public UnityVectorInt normal;
        public byte alpha;

        public VertexObject(Swe1rVertex source)
        {
            position = source.Position.ToUnityVector3Int();
            u = source.U;
            v = source.V;
            normal = source.Normal.ToUnityVector3Int();
            alpha = source.Alpha;
        }

        public Swe1rVertex Export()
        {
            var result = new Swe1rVertex();
            result.Position = position.ToSwe1rVector3Int16();
            result.U = u;
            result.V = v;
            result.Normal = normal.ToSwe1rVector3SByte();
            result.Alpha = alpha;
            return result;
        }
    }
}
