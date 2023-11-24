// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ObjLoader.Loader.Loaders;
using SWE1R.Assets.Blocks.Common.Colors;
using SWE1R.Assets.Blocks.Common.Images;
using SWE1R.Assets.Blocks.Common.Vectors;
using SWE1R.Assets.Blocks.ModelBlock.Meshes;
using SWE1R.Assets.Blocks.ModelBlock.Meshes.Geometry;
using SWE1R.Assets.Blocks.ModelBlock.Meshes.VertexIndices;
using SWE1R.Assets.Blocks.ModelBlock.Nodes;
using SWE1R.Assets.Blocks.TextureBlock;
using System.Numerics;
using ObjFace = ObjLoader.Loader.Data.Elements.Face;
using ObjFaceVertex = ObjLoader.Loader.Data.Elements.FaceVertex;
using ObjGroup = ObjLoader.Loader.Data.Elements.Group;
using ObjLoadResult = ObjLoader.Loader.Loaders.LoadResult;
using ObjMaterial = ObjLoader.Loader.Data.Material;
using ObjNormal = ObjLoader.Loader.Data.VertexData.Normal;
using ObjTexture = ObjLoader.Loader.Data.VertexData.Texture;
using ObjVertex = ObjLoader.Loader.Data.VertexData.Vertex;

namespace SWE1R.Assets.Blocks.CommandLine
{
    public class ObjImporter
    {
        #region Fields

        private const int _indicesRangeMaxLength = byte.MaxValue / 4;
        private ObjLoadResult _objLoadResult;
        private Dictionary<ObjMaterial, Material> _materials = 
            new Dictionary<ObjMaterial, Material>();

        #endregion

        #region Properties (input)

        public string ObjFilename { get; }
        public Block<Texture> TextureBlock { get; }
        public Func<string, ImageRgba32> ImageLoadFunc { get; }
        public ObjImporterConfiguration Configuration { get; }

        #endregion

        #region Properties (output)

        public MeshGroup3064 MeshGroup3064 { get; private set; }

        #endregion

        #region Constructor

        public ObjImporter(
            string objFilename, 
            Block<Texture> textureBlock, 
            Func<string, ImageRgba32> imageLoadFunc, 
            ObjImporterConfiguration configuration)
        {
            ObjFilename = objFilename;
            TextureBlock = textureBlock;
            ImageLoadFunc = imageLoadFunc;
            Configuration = configuration;
        }

        #endregion

        #region Methods (import)

        public void Import()
        {
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
                    Mesh mesh = ImportObjGroup(objGroup);
                    MeshGroup3064.Children.Add(mesh);
                }
            }
            MeshGroup3064.UpdateChildrenCount();
            MeshGroup3064.UpdateBounds();
        }

        private Mesh ImportObjGroup(ObjGroup objGroup)
        {
            var mesh = new Mesh();

            mesh.Material = ImportMaterial(objGroup.Material);

            mesh.MeshGroupOrShorts = new MeshGroupOrShorts();

            mesh.PrimitiveType = PrimitiveType.Triangles;

            List<IndicesRange> indicesRanges;
            (mesh.VisibleVertices, indicesRanges) = GetVerticesAndIndicesRanges(objGroup, mesh);
            mesh.VisibleIndicesChunks = GetIndicesChunks(indicesRanges, mesh);

            // counts
            mesh.UpdateCounts();

            // bounds
            var bounds = new Bounds3Single(mesh.VisibleVertices.Select(v => (Vector3Single)v.Position).ToArray());
            mesh.Bounds0 = bounds.Min;
            mesh.Bounds1 = bounds.Max;

            // TODO: FacesCount
            //Mesh.FacesCount = (short)Mesh.VisibleIndicesChunks.SelectMany(x => x.Triangles).Count();
            mesh.FacesCount = (short)indicesRanges.SelectMany(r => r.Chunks0506).Count();
            mesh.FacesVertexCounts = indicesRanges.SelectMany(r => r.Chunks0506).Select(x => x.Indices.Count()).ToList();
            
            return mesh;
        }

        private Material ImportMaterial(ObjMaterial objMaterial)
        {
            objMaterial ??= _objLoadResult.Materials.FirstOrDefault(); // HACK: workaround for missing 'usemtl'

            if (_materials.TryGetValue(objMaterial, out Material existingMaterial))
                return existingMaterial;
            else
            {
                // load image
                ImageRgba32 imageRgba32;
                string textureImageFilename = objMaterial?.DiffuseTextureMap; // map_Kd
                if (textureImageFilename != null)
                    imageRgba32 = ImageLoadFunc(textureImageFilename);
                else
                    imageRgba32 = ImageLoadFunc("cube.png"); // TODO: !!! test texture in resources

                // import material/texture
                MaterialImporter importer = new MaterialImporterFactory().Get(imageRgba32, TextureBlock);
                importer.Import();

                Material material = importer.Material;
                _materials[objMaterial] = material;
                return material;
            }
        }

        private (List<Vertex> vertices, List<IndicesRange> indicesRanges) GetVerticesAndIndicesRanges(ObjGroup objGroup, Mesh mesh)
        {
            int startVertexIndex = 0;
            int indexBase = 0;
            var vertices = new List<Vertex>();
            var indicesRange = new IndicesRange();
            var indicesRanges = new List<IndicesRange>() { indicesRange };
            foreach (ObjFace face in objGroup.Faces)
            {
                if (face.Count == 3) // triangle
                {
                    // vertices
                    vertices.Add(GetVertex(face[0], _objLoadResult));
                    vertices.Add(GetVertex(face[1], _objLoadResult));
                    vertices.Add(GetVertex(face[2], _objLoadResult));

                    // indices
                    var triangle = new Triangle(Enumerable.Range(indexBase, 3).ToArray());
                    indexBase += 3;
                    int[] indices = triangle.GetIndices().ToArray();
                    var chunk05 = new IndicesChunk05() {
                        Index0 = ValidateAndConvert(2 * (indices[0] - startVertexIndex)),
                        Index1 = ValidateAndConvert(2 * (indices[1] - startVertexIndex)),
                        Index2 = ValidateAndConvert(2 * (indices[2] - startVertexIndex)),
                    };
                    indicesRange.Chunks0506.Add(chunk05);
                }
                else if (face.Count == 4) // quad
                {
                    // vertices
                    vertices.Add(GetVertex(face[0], _objLoadResult));
                    vertices.Add(GetVertex(face[1], _objLoadResult));
                    vertices.Add(GetVertex(face[2], _objLoadResult));
                    vertices.Add(GetVertex(face[3], _objLoadResult));

                    // indices
                    var quad = new Quad(Enumerable.Range(indexBase, 4).ToArray());
                    indexBase += 4;
                    int[] indices = quad.GetTriangles().SelectMany(t => t.GetIndices()).ToArray();
                    //var chunk06 = new IndicesChunk06() {
                    //    Index0 = ValidateAndConvert(2 * (indices[0] - startVertexIndex)),
                    //    Index1 = ValidateAndConvert(2 * (indices[1] - startVertexIndex)),
                    //    Index2 = ValidateAndConvert(2 * (indices[2] - startVertexIndex)),

                    //    Index3 = ValidateAndConvert(2 * (indices[3] - startVertexIndex)),
                    //    Index4 = ValidateAndConvert(2 * (indices[4] - startVertexIndex)),
                    //    Index5 = ValidateAndConvert(2 * (indices[5] - startVertexIndex)),
                    //};
                    //indicesRange.Chunks0506.Add(chunk06);
                    var chunk05_1 = new IndicesChunk05() {
                        Index0 = ValidateAndConvert(2 * (indices[0] - startVertexIndex)),
                        Index1 = ValidateAndConvert(2 * (indices[1] - startVertexIndex)),
                        Index2 = ValidateAndConvert(2 * (indices[2] - startVertexIndex)),
                    };
                    var chunk05_2 = new IndicesChunk05() {
                        Index0 = ValidateAndConvert(2 * (indices[3] - startVertexIndex)),
                        Index1 = ValidateAndConvert(2 * (indices[4] - startVertexIndex)),
                        Index2 = ValidateAndConvert(2 * (indices[5] - startVertexIndex)),
                    };
                    indicesRange.Chunks0506.Add(chunk05_1);
                    indicesRange.Chunks0506.Add(chunk05_2);
                }
                else // polygon
                {
                    // vertices
                    vertices.AddRange(
                        GetFaceVertices(face).Select(f => GetVertex(f, _objLoadResult)));

                    // indices
                    var triangleFan = new TriangleFan(Enumerable.Range(indexBase, face.Count).ToArray());
                    indexBase += face.Count;
                    foreach (Triangle triangle in triangleFan.GetTriangles())
                    {
                        var chunk05 = new IndicesChunk05() {
                            Index0 = ValidateAndConvert(2 * (triangle.I0 - startVertexIndex)),
                            Index1 = ValidateAndConvert(2 * (triangle.I1 - startVertexIndex)),
                            Index2 = ValidateAndConvert(2 * (triangle.I2 - startVertexIndex)),
                        };
                        indicesRange.Chunks0506.Add(chunk05);
                    }
                }

                // indices
                if (indicesRange.NextIndicesBase >= _indicesRangeMaxLength) // TODO: indicesRangeMaxLength
                {
                    // new indices range
                    startVertexIndex += indicesRange.NextIndicesBase / 2;
                    indicesRange = new IndicesRange();
                    indicesRanges.Add(indicesRange);
                }
            }
            return (vertices, indicesRanges);
        }

        private IndicesChunks GetIndicesChunks(List<IndicesRange> indicesRanges, Mesh mesh)
        {
            int startVertexIndex = 0;
            var indicesChunks = new IndicesChunks();
            foreach (IndicesRange range in indicesRanges)
            {
                if (range.Chunks0506.Count == 0)
                    break;
                range.Chunk01 = new IndicesChunk01() {
                    Length = (byte)(range.Indices.Distinct().Count() * Vertex.StructureSize),
                    NextIndicesBase = (byte)range.NextIndicesBase,
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

        private Vertex GetVertex(ObjFaceVertex objFaceVertex, ObjLoadResult objLoadResult)
        {
            // position
            Vector3 position = ToVector(objLoadResult.GetVertex(objFaceVertex.VertexIndex));
            position = Vector3.Multiply(position, Configuration.PositionScale) + Configuration.PositionOffset;

            // texture
            Vector2 texture;
            if (objFaceVertex.TextureIndex > 0)
                texture = ToVector(objLoadResult.GetTexture(objFaceVertex.TextureIndex));
            else
                texture = Vector2.Zero;
            texture = Vector2.Multiply(texture, Vertex.UvDivisor);

            return new Vertex() {
                Position = new Vector3Int16() {
                    X = (short)position.X,
                    Y = (short)position.Y,
                    Z = (short)position.Z,
                },
                U = (short)texture.X,
                V = (short)texture.Y,
                Color = ColorRgba32.White,
            };
        }

        #endregion

        #region Methods (helper)

        protected byte ValidateAndConvert(int value) // TODO: extract as extension method
        {
            if (value < byte.MinValue || 
                value > byte.MaxValue)
                throw new ArgumentOutOfRangeException(nameof(value));
            return (byte)value;
        }

        private Vector3 ToVector(ObjVertex objVertex) =>
            new Vector3(-objVertex.X, objVertex.Z, objVertex.Y);

        private Vector2 ToVector(ObjTexture objTexture) =>
            new Vector2(objTexture.X, -objTexture.Y);

        private Vector3 ToVector(ObjNormal objNormal) =>
            new Vector3(-objNormal.X, objNormal.Z, objNormal.Y);

        private IEnumerable<ObjFaceVertex> GetFaceVertices(ObjFace face)
        {
            for (int i = 0; i < face.Count; i++)
                yield return face[i];
        }

        #endregion
    }
}
