﻿// SPDX-License-Identifier: MIT

using SWE1R.Assets.Blocks.ModelBlock.F3DEX2;
using SWE1R.Assets.Blocks.ModelBlock.Meshes.Geometry;
using System.Collections.Generic;
using ObjFace = ObjLoader.Loader.Data.Elements.Face;

namespace SWE1R.Assets.Blocks.ModelBlock.Import
{
    public class FaceHelper
    {
        public ObjFace ObjFace { get; }
        public List<Vtx> Vertices { get; }
        public List<Triangle> Triangles { get; }

        public FaceHelper(ObjFace objFace)
        {
            ObjFace = objFace;
            Vertices = new List<Vtx>();
            Triangles = new List<Triangle>();
        }

        public override string ToString() =>
            $"{nameof(ObjFace)}.{nameof(ObjFace.Count)}={ObjFace.Count}, " +
            $"{nameof(Vertices)}.{nameof(Vertices.Count)}={Vertices.Count}, " +
            $"{nameof(Triangles)}.{nameof(Triangles.Count)}={Triangles.Count}";
    }
}
