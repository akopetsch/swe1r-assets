﻿// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using SWE1R.Assets.Blocks.ModelBlock.Meshes;
using SWE1R.Assets.Blocks.ModelBlock.Meshes.Geometry;
using SWE1R.Assets.Blocks.ModelBlock.Meshes.VertexIndices;
using System.Collections.Generic;
using System.Linq;

namespace SWE1R.Assets.Blocks.ModelBlock.Import
{
    public class MeshHelper
    {
        public List<FaceHelper> FaceHelpers { get; } =
            new List<FaceHelper>();

        public List<IndicesRange> IndicesRanges { get; } =
            new List<IndicesRange>();

        public IEnumerable<Vertex> Vertices =>
            FaceHelpers.SelectMany(x => x.Vertices);

        public IEnumerable<Triangle> Triangles =>
            FaceHelpers.SelectMany(x => x.Triangles);

        public override string ToString() =>
            $"{nameof(FaceHelpers)}.{nameof(FaceHelpers.Count)}={FaceHelpers.Count}, " +
            $"{nameof(IndicesRanges)}.{nameof(IndicesRanges.Count)}={IndicesRanges.Count}";
    }
}
