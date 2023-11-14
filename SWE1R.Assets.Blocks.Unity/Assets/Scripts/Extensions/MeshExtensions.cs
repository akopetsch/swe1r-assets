// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using System.Linq;
using UnityEngine;

namespace SWE1R.Assets.Blocks.Unity.Assets.Scripts.Extensions
{
    public static class MeshExtensions
    {
        public static Mesh DoubleSided(this Mesh mesh)
        {
            Vector3[] vertices = mesh.vertices.Concat(mesh.vertices).ToArray();
            Vector2[] uv = mesh.uv.Concat(mesh.uv).ToArray();
            int[] triangles = mesh.triangles.Concat(mesh.triangles.Select(i => i + mesh.vertices.Length).Reverse()).ToArray();

            var result = new Mesh() {
                vertices = vertices,
                uv = uv,
                triangles = triangles,
            };
            result.RecalculateBounds();
            result.RecalculateNormals();

            return result;
        }
    }
}
