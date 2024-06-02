// SPDX-License-Identifier: MIT

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
            $"[{i}] {GetInfoString(mesh.Vertices.Count, mesh.CommandList.Count)}";

        private string GetSumInfoString(MeshGroup3064 meshGroup3064) =>
            $"total: {GetInfoString(GetVerticesCount(meshGroup3064), GetCommandsCount(meshGroup3064))}";

        private int GetVerticesCount(MeshGroup3064 meshGroup3064) =>
            meshGroup3064.Meshes.Sum(m => m.Vertices.Count);

        private int GetCommandsCount(MeshGroup3064 meshGroup3064) =>
            meshGroup3064.Meshes.Sum(m => m.CommandList.Count);

        private string GetInfoString(int verticesCount, int commandsCount) =>
            $"{nameof(verticesCount)} = {verticesCount}, " +
            $"{nameof(commandsCount)} = {commandsCount}";

        #endregion
    }
}
