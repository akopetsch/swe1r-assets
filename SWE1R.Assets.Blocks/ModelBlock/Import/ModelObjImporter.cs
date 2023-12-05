// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ObjLoader.Loader.Loaders;
using SWE1R.Assets.Blocks.Images;
using SWE1R.Assets.Blocks.ModelBlock.Materials;
using SWE1R.Assets.Blocks.ModelBlock.Materials.Import;
using SWE1R.Assets.Blocks.ModelBlock.Meshes;
using SWE1R.Assets.Blocks.ModelBlock.Meshes.Geometry;
using SWE1R.Assets.Blocks.ModelBlock.Meshes.VertexIndices;
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

        private const int _indicesRangeMaxLength = byte.MaxValue / 4; // TODO: _indicesRangeMaxLength
        private const string _testImageFilename = "cube.png"; // TODO: _testImageFilename, test texture in resources

        private Material _testMaterial;

        private ObjLoadResult _objLoadResult;
        private Dictionary<ObjMaterial, Material> _materials = 
            new Dictionary<ObjMaterial, Material>();

        #endregion

        #region Properties (input)

        public string ObjFilename { get; }
        public Block<TextureBlockItem> TextureBlock { get; }
        public IImageRgba32Loader ImageLoader { get; }
        public ModelObjImporterConfiguration Configuration { get; }

        #endregion

        #region Properties (output)

        public MeshGroup3064 MeshGroup3064 { get; private set; }

        #endregion

        #region Constructor

        public ModelObjImporter(
            string objFilename, 
            Block<TextureBlockItem> textureBlock, 
            IImageRgba32Loader imageLoader, 
            ModelObjImporterConfiguration configuration)
        {
            ObjFilename = objFilename;
            TextureBlock = textureBlock;
            ImageLoader = imageLoader;
            Configuration = configuration;
        }

        #endregion

        #region Methods (import)

        public void Import()
        {
            _testMaterial = ImportTestMaterial();

            IObjLoader objLoader = new ObjLoaderFactory().Create();
            using var fileStream = File.OpenRead(ObjFilename);
            _objLoadResult = objLoader.Load(fileStream);

            MeshGroup3064 = new MeshGroup3064() {
                Bitfield1 = -1,
                Bitfield2 = -1,
                Children = new List<INode>(),
            };
            foreach (ObjGroup objGroup in _objLoadResult.Groups)
            {
                if (objGroup.Faces.Count > 0)
                {
                    List<Mesh> meshes = ImportObjGroup(objGroup);
                    MeshGroup3064.Children.AddRange(meshes);
                }
            }
            MeshGroup3064.UpdateChildrenCount();
            MeshGroup3064.UpdateBounds();
        }

        private Material ImportTestMaterial()
        {
            ImageRgba32 imageRgba32 = ImageLoader.Load(_testImageFilename);
            MaterialImporter importer = new MaterialImporterFactory().Get(imageRgba32, TextureBlock);
            importer.Import();
            return importer.Material;
        }

        private List<Mesh> ImportObjGroup(ObjGroup objGroup)
        {
            var meshes = new List<Mesh>();

            var mesh = new Mesh();
            meshes.Add(mesh);

            mesh.Material = ImportObjMaterial(objGroup.Material);

            VerticesAndIndicesRanges verticesAndIndicesRanges = GetVerticesAndIndicesRanges(objGroup);
            mesh.VisibleVertices = verticesAndIndicesRanges.Vertices;
            mesh.VisibleIndicesChunks = GetIndicesChunks(verticesAndIndicesRanges.IndicesRanges, mesh);

            mesh.UpdateCounts();
            mesh.UpdateFacesCountByVisibleIndicesChunks();
            mesh.UpdateBounds();

            return meshes;
        }

        private Material ImportObjMaterial(ObjMaterial objMaterial)
        {
            objMaterial ??= _objLoadResult.Materials.FirstOrDefault(); // HACK: workaround for missing 'usemtl'
            if (objMaterial == null)
                return _testMaterial;
            else
            {
                if (_materials.TryGetValue(objMaterial, out Material existingMaterial))
                    return existingMaterial;
                else
                {
                    string textureImageFilename = objMaterial.DiffuseTextureMap; // map_Kd
                    if (textureImageFilename == null)
                        return _testMaterial;
                    else
                    {
                        ImageRgba32 imageRgba32 = ImageLoader.Load(textureImageFilename);
                        MaterialImporter importer = new MaterialImporterFactory().Get(imageRgba32, TextureBlock);
                        importer.Import();

                        Material material = importer.Material;
                        _materials[objMaterial] = material;
                        return material;
                    }
                }
            }
        }

        private class VerticesAndIndicesRanges
        {
            public List<Vertex> Vertices { get; }
            public List<IndicesRange> IndicesRanges { get; }

            public VerticesAndIndicesRanges()
            {
                Vertices = new List<Vertex>();
                IndicesRanges = new List<IndicesRange>();
            }

            public VerticesAndIndicesRanges(
                List<Vertex> vertices, List<IndicesRange> indicesRanges)
            {
                Vertices = vertices;
                IndicesRanges = indicesRanges;
            }
        }

        private VerticesAndIndicesRanges GetVerticesAndIndicesRanges(ObjGroup objGroup)
        {
            int startVertexIndex = 0;
            int indexBase = 0;
            var vertices = new List<Vertex>();
            var indicesRange = new IndicesRange();
            var indicesRanges = new List<IndicesRange>() { indicesRange };
            foreach (ObjFace face in objGroup.Faces)
            {
                if (indicesRange.NextIndicesBase >= _indicesRangeMaxLength)
                {
                    // HACK: this should happen in the foreach below
                    // new indices range
                    startVertexIndex += indicesRange.NextIndicesBase / 2;
                    indicesRange = new IndicesRange();
                    indicesRanges.Add(indicesRange);
                }

                // vertices
                List<Vertex> newVertices = GetObjFaceVertices(face).Select(f => ImportObjFaceVertex(f, _objLoadResult)).ToList();
                List<IndicesChunk> newIndicesChunks = GetIndicesChunks(indexBase, face.Count, startVertexIndex).ToList();
                indexBase += face.Count;
                int? maxVertexCount = Configuration.MaxVertexCountPerMesh;
                if (maxVertexCount.HasValue)
                {
                    if (newVertices.Count > maxVertexCount)
                        throw new InvalidOperationException();
                    if (newVertices.Count + vertices.Count > maxVertexCount)
                    {
                        // TODO: new mesh
                    }
                }
                vertices.AddRange(newVertices);
                indicesRange.Chunks0506.AddRange(newIndicesChunks);
            }
            return new VerticesAndIndicesRanges(vertices, indicesRanges);
        }

        private IEnumerable<IndicesChunk> GetIndicesChunks(int indexBase, int faceCount, int startVertexIndex)
        {
            // primitives indices
            int[] primitiveIndices = Enumerable.Range(indexBase, faceCount).ToArray();
            
            // primitives
            var primitives = new List<Primitive>();
            if (faceCount == 3)
                primitives.Add(new Triangle(primitiveIndices));
            else if (faceCount == 4)
                primitives.Add(new Quad(primitiveIndices));
            else if (faceCount > 4)
                primitives.Add(new TriangleFan(primitiveIndices));
            else
                throw new InvalidOperationException();

            // triangles -> indicesRange
            var triangles = primitives.SelectMany(p => p.GetTriangles()).ToList();
            foreach (Triangle triangle in triangles)
            {
                yield return new IndicesChunk05() {
                    Index0 = Convert.ToByte(2 * (triangle.I0 - startVertexIndex)),
                    Index1 = Convert.ToByte(2 * (triangle.I1 - startVertexIndex)),
                    Index2 = Convert.ToByte(2 * (triangle.I2 - startVertexIndex)),
                };
                // TODO: use IndicesChunk06 (e.g. for Quad) for smaller file size
            }
        }

        private IndicesChunks GetIndicesChunks(List<IndicesRange> indicesRanges, Mesh mesh)
        {
            int startVertexIndex = 0;
            var indicesChunks = new IndicesChunks();
            foreach (IndicesRange range in indicesRanges)
            {
                range.Chunk01 = new IndicesChunk01() {
                    Length = Convert.ToInt16(range.Indices.Distinct().Count() * Vertex.StructureSize),
                    NextIndicesBase = Convert.ToByte(range.NextIndicesBase),
                    StartVertex = new ReferenceByIndex<Vertex>() {
                        Collection = mesh.VisibleVertices,
                        Index = startVertexIndex,
                    }
                };
                startVertexIndex += range.NextIndicesBase / 2;
                indicesChunks.AddRange(range.AllChunks);
            }
            return indicesChunks;
        }

        private Vertex ImportObjFaceVertex(ObjFaceVertex objFaceVertex, ObjLoadResult objLoadResult)
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
            texture = Vector2.Multiply(texture, Vertex.UvDivisor);

            return new Vertex() {
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
