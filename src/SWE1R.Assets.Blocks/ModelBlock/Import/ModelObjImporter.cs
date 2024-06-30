// SPDX-License-Identifier: MIT

using ObjLoader.Loader.Loaders;
using SWE1R.Assets.Blocks.Images;
using SWE1R.Assets.Blocks.ModelBlock.Materials;
using SWE1R.Assets.Blocks.ModelBlock.Meshes;
using SWE1R.Assets.Blocks.ModelBlock.Meshes.Geometry;
using SWE1R.Assets.Blocks.ModelBlock.F3DEX2;
using SWE1R.Assets.Blocks.ModelBlock.Nodes;
using SWE1R.Assets.Blocks.TextureBlock;
using SWE1R.Assets.Blocks.Vectors;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using ObjFace = ObjLoader.Loader.Data.Elements.Face;
using ObjFaceVertex = ObjLoader.Loader.Data.Elements.FaceVertex;
using ObjGroup = ObjLoader.Loader.Data.Elements.Group;
using ObjLoadResult = ObjLoader.Loader.Loaders.LoadResult;
using ObjMaterial = ObjLoader.Loader.Data.Material;
using ObjNormal = ObjLoader.Loader.Data.VertexData.Normal;
using ObjTexture = ObjLoader.Loader.Data.VertexData.Texture;
using ObjVertex = ObjLoader.Loader.Data.VertexData.Vertex;

namespace SWE1R.Assets.Blocks.ModelBlock.Import
{
    public class ModelObjImporter
    {
        #region Fields

        private Dictionary<ObjMaterial, MeshMaterial> _meshMaterials = 
            new Dictionary<ObjMaterial, MeshMaterial>();

        private ModelObjImporterDebugInfoPrinter _debugInfoPrinter;

        #endregion

        #region Properties (input)

        public Stream ObjStream { get; }
        public Block<TextureBlockItem> TextureBlock { get; }
        public ModelObjImporterConfiguration Configuration { get; }
        public LoadImageRgba32FromStreamDelegate ImageLoader { get; }
        public OpenFileStreamDelegate OpenFileStreamDelegate { get; }

        #endregion

        #region Properties (output)

        public ObjLoadResult ObjLoadResult { get; private set; }
        public MeshGroupNode MeshGroupNode { get; private set; }

        #endregion

        #region Constructor

        public ModelObjImporter(
            Stream objStream, 
            Block<TextureBlockItem> textureBlock, 
            ModelObjImporterConfiguration configuration,
            LoadImageRgba32FromStreamDelegate imageLoader,
            OpenFileStreamDelegate openFileStreamDelegate = null)
        {
            ObjStream = objStream;
            TextureBlock = textureBlock;
            Configuration = configuration;
            ImageLoader = imageLoader;
            OpenFileStreamDelegate = openFileStreamDelegate ?? (f => File.OpenRead(f));

            if (Configuration.PrintDebugInfo)
                _debugInfoPrinter = new ModelObjImporterDebugInfoPrinter(this);
        }

        #endregion

        private class MaterialStreamProvider : IMaterialStreamProvider
        {
            public OpenFileStreamDelegate OpenMaterialStreamDelegate { get; }

            public MaterialStreamProvider(OpenFileStreamDelegate openMaterialStreamDelegate) =>
                OpenMaterialStreamDelegate = openMaterialStreamDelegate;

            public Stream Open(string materialFilePath) =>
                OpenMaterialStreamDelegate(materialFilePath);
        }

        #region Methods (import)

        public void Import()
        {
            _debugInfoPrinter?.PrintImportStart();

            ObjLoadResult = new ObjLoaderFactory()
                .Create(new MaterialStreamProvider(OpenFileStreamDelegate)).Load(ObjStream);

            MeshGroupNode = new MeshGroupNode() {
                Flags1 = -1,
                Flags2 = -1,
                Children = new List<INode>(),
            };
            foreach (ObjGroup objGroup in ObjLoadResult.Groups)
            {
                if (objGroup.Faces.Count > 0)
                {
                    List<Mesh> meshes = ImportObjGroup(objGroup);
                    MeshGroupNode.Children.AddRange(meshes);
                }
            }
            MeshGroupNode.UpdateChildrenCount();
            MeshGroupNode.UpdateBounds();

            _debugInfoPrinter?.PrintImportResult();
        }

        private List<Mesh> ImportObjGroup(ObjGroup objGroup)
        {
            var meshes = new List<Mesh>();
            List<MeshHelper> meshHelpers = GetMeshHelpers(objGroup);
            foreach (MeshHelper meshHelper in meshHelpers)
            {
                var mesh = new Mesh();
                meshes.Add(mesh);

                mesh.MeshMaterial = ImportObjMaterial(objGroup.Material); // TODO: !!!!!! must have a value

                mesh.Vertices = meshHelper.Vertices.ToList();
                mesh.CommandList = GetCommandList(meshHelper.VertexBuffers, mesh);

                mesh.UpdateCounts();
                mesh.UpdateFacesCountByCommandList();
                mesh.UpdateBounds();
            }
            return meshes;
        }

        private MeshMaterial ImportObjMaterial(ObjMaterial objMaterial)
        {
            if (Configuration.TryFirstMaterialAsFallback)
                objMaterial ??= ObjLoadResult.Materials.FirstOrDefault();
            if (objMaterial == null)
                return Configuration.FallbackMeshMaterial;
            else
            {
                if (_meshMaterials.TryGetValue(objMaterial, out MeshMaterial existingMeshMaterial))
                    return existingMeshMaterial;
                else
                {
                    string textureImageFilename = objMaterial.DiffuseTextureMap; // map_Kd
                    if (textureImageFilename == null)
                        return Configuration.FallbackMeshMaterial;
                    else
                    {
                        using var stream = OpenFileStreamDelegate(textureImageFilename);
                        return _meshMaterials[objMaterial] = 
                            MeshMaterial.Import(stream, TextureBlock, ImageLoader, Configuration.TextureEndianness).MeshMaterial;
                    }
                }
            }
        }

        private bool WouldExceedMax(MeshHelper meshHelper, int newVerticesCount)
        {
            int? maxVertexCount = Configuration.MaxVertexCountPerMesh;
            if (maxVertexCount.HasValue)
            {
                int existingVerticesCount = meshHelper.Vertices.Count();
                if (existingVerticesCount > maxVertexCount)
                    throw new InvalidOperationException();
                return existingVerticesCount + newVerticesCount > maxVertexCount;
            }
            return false;
        }

        private List<MeshHelper> GetMeshHelpers(ObjGroup objGroup)
        {
            var meshHelpers = new List<MeshHelper>();
            var currentMeshHelper = new MeshHelper();
            meshHelpers.Add(currentMeshHelper);
            int indexBase = 0;
            FaceHelper currentFaceHelper = null;
            foreach (ObjFace face in objGroup.Faces)
            {
                // if exceeds vertices count
                if (currentFaceHelper != null && WouldExceedMax(currentMeshHelper, currentFaceHelper.Vertices.Count))
                {
                    // new MeshHelper
                    currentMeshHelper = new MeshHelper();
                    meshHelpers.Add(currentMeshHelper);

                    indexBase = 0;
                }

                currentFaceHelper = new FaceHelper(face);

                currentFaceHelper.Vertices.AddRange(
                    GetObjFaceVertices(face).Select(f => ImportObjFaceVertex(f, ObjLoadResult)));
                currentFaceHelper.Triangles.AddRange(
                    GetTriangles(face, indexBase));
                indexBase += face.Count;

                currentMeshHelper.FaceHelpers.Add(currentFaceHelper);
            }

            foreach (MeshHelper meshHelper in meshHelpers)
            {
                var currentVertexBuffer = new VertexBuffer();
                meshHelper.VertexBuffers.Add(currentVertexBuffer);
                int v0 = 0;
                foreach (FaceHelper faceHelper in meshHelper.FaceHelpers)
                {
                    // if exceeds IndicesRange max length
                    if (currentVertexBuffer.NextIndicesBase >= Configuration.IndicesRangeMaxLength)
                    {
                        // new IndicesRange
                        v0 += currentVertexBuffer.NextIndicesBase;
                        currentVertexBuffer = new VertexBuffer();
                        meshHelper.VertexBuffers.Add(currentVertexBuffer);
                    }

                    var trianglesCommands = new List<ITrianglesGraphicsCommand>();
                    foreach (Triangle triangle in faceHelper.Triangles)
                    {

                        trianglesCommands.Add(new Gsp1TriangleCommand(
                            Convert.ToByte(triangle.I0 - v0),
                            Convert.ToByte(triangle.I1 - v0),
                            Convert.ToByte(triangle.I2 - v0)
                            ));
                    }
                    currentVertexBuffer.TrianglesCommands.AddRange(trianglesCommands);
                }
            }

            return meshHelpers;
        }

        private IEnumerable<Triangle> GetTriangles(ObjFace objFace, int indexBase)
        {
            // primitives indices
            int[] primitiveIndices = Enumerable.Range(indexBase, objFace.Count).ToArray();

            // primitive from primitive indices
            Primitive primitive;
            if (objFace.Count == 3)
                primitive = new Triangle(primitiveIndices);
            else if (objFace.Count == 4)
                primitive = new Quad(primitiveIndices);
            else if (objFace.Count > 4)
                primitive = new TriangleFan(primitiveIndices);
            else
                throw new InvalidOperationException();

            // triangles from primitive
            return primitive.GetTriangles();
        }

        private GraphicsCommandList GetCommandList(List<VertexBuffer> vertexBuffers, Mesh mesh)
        {
            var commandList = new GraphicsCommandList();
            int verticesStartIndex = 0;
            foreach (VertexBuffer vertexBuffer in vertexBuffers)
            {
                int n = Convert.ToByte(vertexBuffer.Indices.Distinct().Count());
                int v0PlusN = Convert.ToByte(vertexBuffer.NextIndicesBase);
                vertexBuffer.VertexCommand = new GspVertexCommand(mesh.Vertices, verticesStartIndex, n, v0PlusN);
                verticesStartIndex += vertexBuffer.NextIndicesBase;
                commandList.AddRange(vertexBuffer.AllCommands);
            }
            return commandList;
        }

        private Vtx ImportObjFaceVertex(ObjFaceVertex objFaceVertex, ObjLoadResult objLoadResult)
        {
            // position
            Vector3 position = ImportObjVertex(objLoadResult.GetVertex(objFaceVertex.VertexIndex));
            position = Vector3.Multiply(position, Configuration.PositionScale) + Configuration.PositionOffset;

            // texture
            Vector2 texture;
            if (objFaceVertex.TextureIndex > 0)
                texture = ImportObjTexture(objLoadResult.GetTexture(objFaceVertex.TextureIndex));
            else
                texture = Vector2.Zero;
            texture = Vector2.Multiply(texture, VtxExtensions.UvDivisor);

            return new Vtx() {
                Position = new Vector3Int16() {
                    X = Convert.ToInt16(position.X),
                    Y = Convert.ToInt16(position.Y),
                    Z = Convert.ToInt16(position.Z),
                },
                U = Convert.ToInt16(texture.X),
                V = Convert.ToInt16(texture.Y),
                Byte_C = byte.MaxValue,
                Byte_D = byte.MaxValue,
                Byte_E = byte.MaxValue,
                Byte_F = byte.MaxValue,
            };
        }

        #endregion

        #region Methods (helper)

        private Vector3 ImportObjVertex(ObjVertex objVertex) =>
            new Vector3(-objVertex.X, objVertex.Z, objVertex.Y);

        private Vector2 ImportObjTexture(ObjTexture objTexture) =>
            new Vector2(objTexture.X, -objTexture.Y);

        private Vector3 ImportObjNormal(ObjNormal objNormal) =>
            new Vector3(-objNormal.X, objNormal.Z, objNormal.Y);

        private IEnumerable<ObjFaceVertex> GetObjFaceVertices(ObjFace face)
        {
            for (int i = 0; i < face.Count; i++)
                yield return face[i];
        }

        #endregion
    }
}
