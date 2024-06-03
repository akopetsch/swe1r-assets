// SPDX-License-Identifier: MIT

using SWE1R.Assets.Blocks.ModelBlock.Meshes;
using SWE1R.Assets.Blocks.ModelBlock.Meshes.Geometry;
using SWE1R.Assets.Blocks.ModelBlock.Meshes.N64GspCommands;
using System.Collections.Generic;
using System.Linq;

namespace SWE1R.Assets.Blocks.ModelBlock.Import
{
    public class MeshHelper
    {
        public List<FaceHelper> FaceHelpers { get; } =
            new List<FaceHelper>();

        public List<N64GspVertexBuffer> VertexBuffers { get; } =
            new List<N64GspVertexBuffer>();

        public IEnumerable<Vertex> Vertices =>
            FaceHelpers.SelectMany(x => x.Vertices);

        public IEnumerable<Triangle> Triangles =>
            FaceHelpers.SelectMany(x => x.Triangles);

        public override string ToString() =>
            $"{nameof(FaceHelpers)}.{nameof(FaceHelpers.Count)}={FaceHelpers.Count}, " +
            $"{nameof(VertexBuffers)}.{nameof(VertexBuffers.Count)}={VertexBuffers.Count}";
    }
}
