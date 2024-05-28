// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using SWE1R.Assets.Blocks.Unity.Extensions;
using System;
using Swe1rVertex = SWE1R.Assets.Blocks.ModelBlock.Meshes.Vertex;
using UnityVectorInt = UnityEngine.Vector3Int;
using UnityColor32 = UnityEngine.Color32;

namespace SWE1R.Assets.Blocks.Unity.Objects
{
    [Serializable]
    public class VertexObject
    {
        public UnityVectorInt position;
        public short u;
        public short v;
        public byte byte_C;
        public byte byte_D;
        public byte byte_E;
        public byte byte_F;

        public UnityColor32 color;

        public VertexObject(Swe1rVertex source)
        {
            position = source.Position.ToUnityVector3Int();
            u = source.U;
            v = source.V;
            byte_C = source.Byte_C;
            byte_D = source.Byte_D;
            byte_E = source.Byte_E;
            byte_F = source.Byte_F;

            color = source.Color.ToUnityColor32();
        }

        public Swe1rVertex Export()
        {
            var result = new Swe1rVertex();
            result.Position = position.ToSwe1rVector3Int16();
            result.U = u;
            result.V = v;
            result.Byte_C = byte_C;
            result.Byte_D = byte_D;
            result.Byte_E = byte_E;
            result.Byte_F = byte_F;
            return result;
        }
    }
}
