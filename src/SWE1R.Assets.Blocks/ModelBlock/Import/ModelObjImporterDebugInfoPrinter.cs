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
            MeshGroupNode meshGroupNode = ModelObjImporter.MeshGroupNode;
            for (int i = 0; i < meshGroupNode.Meshes.Count; i++)
                Console.WriteLine(GetMeshInfoString(i, meshGroupNode.Meshes[i]));
            Console.WriteLine(GetSumInfoString(meshGroupNode));
        }

        private string GetMeshInfoString(int i, Mesh mesh) =>
            $"[{i}] {GetInfoString(mesh.Vertices.Count, mesh.CommandList.Count)}";

        private string GetSumInfoString(MeshGroupNode meshGroupNode) =>
            $"total: {GetInfoString(GetVerticesCount(meshGroupNode), GetCommandsCount(meshGroupNode))}";

        private int GetVerticesCount(MeshGroupNode meshGroupNode) =>
            meshGroupNode.Meshes.Sum(m => m.Vertices.Count);

        private int GetCommandsCount(MeshGroupNode meshGroupNode) =>
            meshGroupNode.Meshes.Sum(m => m.CommandList.Count);

        private string GetInfoString(int verticesCount, int commandsCount) =>
            $"{nameof(verticesCount)} = {verticesCount}, " +
            $"{nameof(commandsCount)} = {commandsCount}";

        #endregion
    }
}
