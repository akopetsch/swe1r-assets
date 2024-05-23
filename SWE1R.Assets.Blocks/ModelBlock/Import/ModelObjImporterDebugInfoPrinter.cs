// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using SWE1R.Assets.Blocks.ModelBlock.Meshes;
using SWE1R.Assets.Blocks.ModelBlock.Nodes;
using System;
using System.Linq;

namespace SWE1R.Assets.Blocks.ModelBlock.Import
{
    public class ModelObjImporterDebugInfoPrinter
    {
        #region Properties

        public ModelObjImporter ModelObjImporter { get; }

        #endregion

        #region Constructor

        public ModelObjImporterDebugInfoPrinter(ModelObjImporter modelObjImporter) =>
            ModelObjImporter = modelObjImporter;

        #endregion

        #region Methods

        public void PrintImportStart() =>
            Console.WriteLine("Import OBJ file.");

        public void PrintImportResult()
        {
            MeshGroup3064 meshGroup3064 = ModelObjImporter.MeshGroup3064;
            for (int i = 0; i < meshGroup3064.Meshes.Count; i++)
                Console.WriteLine(GetMeshInfoString(i, meshGroup3064.Meshes[i]));
            Console.WriteLine(GetSumInfoString(meshGroup3064));
        }

        private string GetMeshInfoString(int i, Mesh mesh) =>
            $"[{i}] {GetInfoString(mesh.VisibleVertices.Count, mesh.VisibleIndicesChunks.Count)}";

        private string GetSumInfoString(MeshGroup3064 meshGroup3064) =>
            $"total: {GetInfoString(meshGroup3064.Meshes.Sum(m => m.VisibleVertices.Count), meshGroup3064.Meshes.Sum(m => m.VisibleIndicesChunks.Count))}";

        private string GetInfoString(int verticesCount, int chunksCount) =>
            $"{nameof(verticesCount)} = {verticesCount}, " +
            $"{nameof(chunksCount)} = {chunksCount}";

        #endregion
    }
}
