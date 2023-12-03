// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using SWE1R.Assets.Blocks.ModelBlock.Meshes.Geometry;
using SWE1R.Assets.Blocks.Unity.Assets.Scripts.Extensions;
using SWE1R.Assets.Blocks.Unity.Extensions;
using SWE1R.Assets.Blocks.Unity.Objects;
using SWE1R.Assets.Blocks.Unity.ScriptableObjects;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Swe1rIndicesChunks = SWE1R.Assets.Blocks.ModelBlock.Meshes.VertexIndices.IndicesChunks;
using Swe1rMaterialExporter = SWE1R.Assets.Blocks.ModelBlock.Materials.Export.MaterialExporter;
using Swe1rMaterialTexture = SWE1R.Assets.Blocks.ModelBlock.Materials.MaterialTexture;
using Swe1rMaterialTextureChild = SWE1R.Assets.Blocks.ModelBlock.Materials.MaterialTextureChild;
using Swe1rMesh = SWE1R.Assets.Blocks.ModelBlock.Meshes.Mesh;
using Swe1rModelImporter = SWE1R.Assets.Blocks.Unity.ModelImporter;
using Swe1rPrimitiveType = SWE1R.Assets.Blocks.ModelBlock.Meshes.PrimitiveType;
using UnityMaterial = UnityEngine.Material;
using UnityMesh = UnityEngine.Mesh;
using UnityTexture2D = UnityEngine.Texture2D;
using UnityVector2 = UnityEngine.Vector2;
using UnityVector3 = UnityEngine.Vector3;

namespace SWE1R.Assets.Blocks.Unity.Components.Models.Meshes
{
    [RequireComponent(typeof(MeshFilter))]
    [RequireComponent(typeof(MeshRenderer))]
    [RequireComponent(typeof(MeshCollider))]
    public class MeshComponent : MonoBehaviour
    {
        #region Fields

        public MaterialScriptableObject material;
        [SerializeReference] public MappingScriptableObject mapping;
        public UnityVector3 bounds0;
        public UnityVector3 bounds1;
        public short facesCount;
        public Swe1rPrimitiveType primitiveType;
        [SerializeReference] public List<int> facesVertexCounts;
        public MeshGroupOrShortsObject meshGroupOrShorts;
        [SerializeReference] public CollisionVerticesObject collisionVertices;
        [SerializeReference] public List<IndicesChunkObject> indicesChunks;
        public List<VertexObject> vertices;
        public short unk_Count;

        #endregion

        #region Methods

        public void Import(Swe1rMesh source, Swe1rModelImporter importer)
        {
            gameObject.name = importer.GetName(source);

            // fields
            material = importer.GetMaterialScriptableObject(source.Material);
            if (source.Mapping != null)
                mapping = importer.GetMappingScriptableObject(source.Mapping);
            bounds0 = source.Bounds0.ToUnityVector3();
            bounds1 = source.Bounds1.ToUnityVector3();
            facesCount = source.FacesCount;
            primitiveType = source.PrimitiveType;
            if (source.FacesVertexCounts != null)
                facesVertexCounts = source.FacesVertexCounts;
            meshGroupOrShorts = new MeshGroupOrShortsObject(source.MeshGroupOrShorts, importer);
            if (source.CollisionVertices != null)
                collisionVertices = new CollisionVerticesObject(source.CollisionVertices);
            if (source.VisibleIndicesChunks != null)
                indicesChunks = source.VisibleIndicesChunks.Select(ic => importer.GetIndicesChunkObject(ic)).ToList();
            if (source.VisibleVertices != null)
                vertices = source.VisibleVertices.Select(x => new VertexObject(x)).ToList();
            unk_Count = source.Unk_Count;

            AddLabelsToName(source);
            UpdateVisualization(source, importer);
        }

        private void AddLabelsToName(Swe1rMesh source)
        {
            var labels = new List<string>();

            Swe1rMaterialTexture texture = source.Material.Texture;
            if (texture != null)
            {
                labels.Add($"fmt:{((short)texture.Format):x4}");
                labels.Add($"i:{texture.TextureIndex}");
            }
            if (source.Mapping != null)
            {
                labels.Add("Mp");
                if (source.Mapping.Subs.Any(x => x.Child?.FlaggedNode_20 != null))
                    labels.Add("MpSubFn");
            }
            if (source.CollisionVertices != null)
                labels.Add("Cv");
            if (source.VisibleIndicesChunks != null)
                labels.Add("Ic");
            if (source.VisibleVertices != null)
                labels.Add("Vv");
            labels.Add($"A{source.Material.Properties.AlphaBpp}");

            gameObject.name += string.Join(string.Empty, labels.Select(x => $"[{x}]"));
        }

        public Swe1rMesh Export(ModelExporter modelExporter)
        {
            var result = new Swe1rMesh();

            result.Material = modelExporter.GetMaterial(material);
            if (mapping != null)
                result.Mapping = modelExporter.GetMapping(mapping);
            result.Bounds0 = bounds0.ToSwe1rVector3Single();
            result.Bounds1 = bounds1.ToSwe1rVector3Single();
            result.FacesCount = facesCount;
            result.PrimitiveType = primitiveType;
            if (facesVertexCounts.Count > 0)
                result.FacesVertexCounts = facesVertexCounts;
            result.MeshGroupOrShorts = meshGroupOrShorts.Export(modelExporter);
            if (collisionVertices?.Count > 0) // TODO: nullable necessary? ([SerializeReference])
                result.CollisionVertices = collisionVertices.Export();
            if (vertices.Count > 0)
                result.VisibleVertices = vertices.Select(v => modelExporter.GetVertex(v)).ToList();
            if (indicesChunks.Count > 0)
                result.VisibleIndicesChunks = 
                    new Swe1rIndicesChunks(indicesChunks.Select(ic => ic.Export(modelExporter, result)).ToList());
            result.Unk_Count = unk_Count;

            result.UpdateCounts();

            return result;
        }

        #endregion

        #region Methods (visualization)

        private void UpdateVisualization(Swe1rMesh source, Swe1rModelImporter importer)
        {
            Swe1rMaterialTextureChild firstMaterialTextureChild = source.Material.Texture?.Children.FirstOrDefault();
            gameObject.GetComponent<MeshFilter>().sharedMesh = GetUnityVisibleMesh(source, firstMaterialTextureChild);
            gameObject.GetComponent<MeshRenderer>().sharedMaterial = GetUnityMaterial(source, firstMaterialTextureChild, importer);
            gameObject.GetComponent<MeshCollider>().sharedMesh = GetUnityCollisionMesh(source);
        }

        private UnityMesh GetUnityVisibleMesh(Swe1rMesh source, Swe1rMaterialTextureChild swe1rMaterialTextureChild)
        {
            UnityMesh unityMesh = null;
            List<Triangle> triangles = source.VisibleIndicesChunks?.GetTriangles();
            if (triangles != null)
            {
                List<UnityVector3> unityVertices = source.VisibleVertices
                    .Select(v => v.Position.ToUnityVector3()).ToList();
                List<UnityVector2> unityUvs = source.VisibleVertices
                    .Select(v => v.GetEffectiveUV(swe1rMaterialTextureChild).ToUnityVector2()).ToList();
                unityMesh = new UnityMesh() {
                    vertices = unityVertices.ToArray(),
                    uv = unityUvs.ToArray(),
                    triangles = triangles.SelectMany(t => t.GetIndices()).ToArray(),
                };
                unityMesh.RecalculateNormals();
                unityMesh.RecalculateBounds();
                if (!source.Material.HasBackfaceCulling)
                    unityMesh = unityMesh.DoubleSided();
            }
            return unityMesh;
        }

        private UnityMaterial GetUnityMaterial(Swe1rMesh source, Swe1rMaterialTextureChild swe1rMaterialTextureChild, Swe1rModelImporter modelImporter)
        {
            Shader shader;
            if (source.Material.Properties.AlphaBpp == 8)
                shader = Shader.Find("Transparent/Diffuse");
            else
                shader = Shader.Find("Standard");
            var unityMaterial = new UnityMaterial(shader);
            int? swe1rTextureIndex = source.Material.Texture?.TextureIndex;
            if (swe1rTextureIndex.HasValue)
            {
                UnityTexture2D unityTexture = null;
                if (swe1rTextureIndex == -1)
                    unityTexture = new TestTextureHelper().LoadTexture();
                else
                {
                    var sourceMaterialExporter = new Swe1rMaterialExporter(source.Material, modelImporter.TextureBlock);
                    sourceMaterialExporter.Export();
                    unityTexture = sourceMaterialExporter.EffectiveImage.ToUnityTexture2D();
                }
                unityMaterial.SetTexture("_MainTex", unityTexture);
                unityMaterial.SetFloat("_Glossiness", 0); // TODO: necessary for "Standard" shader but also for "Transparent/Diffuse"?
            }
            unityMaterial.name = gameObject.name;
            return unityMaterial;
        }

        private UnityMesh GetUnityCollisionMesh(Swe1rMesh source)
        {
            if (source.CollisionVerticesCount > 2)
            {
                List<UnityVector3> unityVertices =
                    source.CollisionVertices.ShortVectors?.Select(v => v.ToUnityVector3()).ToList() ??
                    source.CollisionVertices.FloatVectors.Select(v => v.ToUnityVector3()).ToList();
                if (unityVertices.Distinct().Count() <= 2)
                {
                    Debug.LogWarning("Number of distinct collision vertices not greater than 2.", this);
                    return null;
                }

                List<int> unityIndices =
                    source.GetCollisionTriangles().SelectMany(t => t.GetIndices().Reverse()).ToList();
                if (unityIndices.Any(i => i >= unityVertices.Count))
                {
                    Debug.LogWarning("Out-of-bound collision vertex indices.", this);
                    return null;
                }

                var unityMesh = new UnityMesh() {
                    vertices = unityVertices.ToArray(),
                    triangles = unityIndices.ToArray(),
                };
                unityMesh.RecalculateNormals();
                unityMesh.RecalculateBounds();
                unityMesh.name = gameObject.name;
                return unityMesh;
            }
            return null;
        }

        #endregion
    }
}
