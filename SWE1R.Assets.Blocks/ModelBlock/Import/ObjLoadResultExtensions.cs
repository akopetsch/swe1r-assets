// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using System.Collections.Generic;
using ObjGroup = ObjLoader.Loader.Data.Elements.Group;
using ObjLoadResult = ObjLoader.Loader.Loaders.LoadResult;
using ObjMaterial = ObjLoader.Loader.Data.Material;
using ObjNormal = ObjLoader.Loader.Data.VertexData.Normal;
using ObjTexture = ObjLoader.Loader.Data.VertexData.Texture;
using ObjVertex = ObjLoader.Loader.Data.VertexData.Vertex;

namespace SWE1R.Assets.Blocks.ModelBlock.Import
{
    public static class ObjLoadResultExtensions
    {
        public static ObjVertex GetVertex(this ObjLoadResult objLoadResult, int index) =>
            GetObjListElement(objLoadResult.Vertices, index);

        public static ObjTexture GetTexture(this ObjLoadResult objLoadResult, int index) =>
            GetObjListElement(objLoadResult.Textures, index);

        public static ObjNormal GetNormal(this ObjLoadResult objLoadResult, int index) =>
            GetObjListElement(objLoadResult.Normals, index);

        public static ObjGroup GetGroup(this ObjLoadResult objLoadResult, int index) =>
            GetObjListElement(objLoadResult.Groups, index);

        public static ObjMaterial GetMaterial(this ObjLoadResult objLoadResult, int index) =>
            GetObjListElement(objLoadResult.Materials, index);

        private static T GetObjListElement<T>(IList<T> objList, int index) =>
            objList[index - 1];
    }
}
