// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization;
using SWE1R.Assets.Blocks.Metadata;
using SWE1R.Assets.Blocks.ModelBlock;
using SWE1R.Assets.Blocks.ModelBlock.Nodes;
using SWE1R.Assets.Blocks.Unity.Components.Models;
using SWE1R.Assets.Blocks.Unity.Components.Models.Meshes;
using SWE1R.Assets.Blocks.Unity.Components.Models.Nodes;
using SWE1R.Assets.Blocks.Unity.Extensions;
using SWE1R.Assets.Blocks.Unity.Objects;
using SWE1R.Assets.Blocks.Unity.ScriptableObjects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Swe1rFlaggedNode = SWE1R.Assets.Blocks.ModelBlock.Nodes.FlaggedNode;
using Swe1rIndicesChunk = SWE1R.Assets.Blocks.ModelBlock.Meshes.VertexIndices.IndicesChunk;
using Swe1rMapping = SWE1R.Assets.Blocks.ModelBlock.Meshes.Mapping;
using Swe1rMappingChild = SWE1R.Assets.Blocks.ModelBlock.Meshes.MappingChild;
using Swe1rMappingSub = SWE1R.Assets.Blocks.ModelBlock.Meshes.MappingSub;
using Swe1rMaterial = SWE1R.Assets.Blocks.ModelBlock.Materials.Material;
using Swe1rMaterialProperties = SWE1R.Assets.Blocks.ModelBlock.Materials.MaterialProperties;
using Swe1rMaterialReference = SWE1R.Assets.Blocks.ModelBlock.Animations.MaterialReference;
using Swe1rMaterialTexture = SWE1R.Assets.Blocks.ModelBlock.Materials.MaterialTexture;
using Swe1rMaterialTextureChild = SWE1R.Assets.Blocks.ModelBlock.Materials.MaterialTextureChild;
using Swe1rMesh = SWE1R.Assets.Blocks.ModelBlock.Meshes.Mesh;
using Swe1rModelBlockItem = SWE1R.Assets.Blocks.ModelBlock.ModelBlockItem;
using Swe1rTextureBlockItem = SWE1R.Assets.Blocks.TextureBlock.TextureBlockItem;
using Swe1rVertex = SWE1R.Assets.Blocks.ModelBlock.Meshes.Vertex;

namespace SWE1R.Assets.Blocks.Unity
{
    public class ModelImporter
    {
        #region Fields

        private readonly MetadataProvider _metadataProvider;
        private readonly ReleaseMetadata _releaseMetadata;
        private readonly BlockItemDumper _dumper = new UnityBlockItemDumper("in");
        private ByteSerializerContext _bitSerializerContext;
        private int _offsetHexStringLength;

        #endregion

        #region Fields (mapping)

        private Dictionary<Swe1rFlaggedNode, GameObject> prefabByFlaggedNode =
            new Dictionary<Swe1rFlaggedNode, GameObject>();

        private Dictionary<Type, Dictionary<object, ScriptableObject>> scriptableObjectsBySourceBySourceType =
            new Dictionary<Type, Dictionary<object, ScriptableObject>>();

        private Dictionary<Swe1rMaterialTextureChild, MaterialTextureChildObject> materialTextureChildObjects =
            new Dictionary<Swe1rMaterialTextureChild, MaterialTextureChildObject>();
        private Dictionary<Swe1rMaterialProperties, MaterialPropertiesObject> materialPropertiesObjects = 
            new Dictionary<Swe1rMaterialProperties, MaterialPropertiesObject>();
        
        private Dictionary<Swe1rVertex, VertexObject> vertexObjects = 
            new Dictionary<Swe1rVertex, VertexObject>();
        private Dictionary<Swe1rIndicesChunk, IndicesChunkObject> indicesChunkObjects = 
            new Dictionary<Swe1rIndicesChunk, IndicesChunkObject>();

        private Dictionary<Swe1rMaterialReference, MaterialReferenceObject> materialReferenceObjects =
            new Dictionary<Swe1rMaterialReference, MaterialReferenceObject>();

        #endregion

        #region Properties (constructor)

        public Block<Swe1rModelBlockItem> ModelBlock { get; }
        public int ModelIndex { get; }
        public Block<Swe1rTextureBlockItem> TextureBlock { get; }

        #endregion

        #region Properties (import)

        public string Name { get; private set; }
        public AssetsHelper AssetsHelper { get; private set; }
        public Swe1rModelBlockItem ModelBlockItem { get; private set; }
        
        public GameObject GameObject { get; private set; }

        #endregion

        #region Constructor

        public ModelImporter(
            Block<Swe1rModelBlockItem> modelBlock,
            int modelIndex,
            Block<Swe1rTextureBlockItem> textureBlock)
        {
            ModelBlock = modelBlock;
            ModelIndex = modelIndex;
            TextureBlock = textureBlock;

            _metadataProvider = new();
            _releaseMetadata = _metadataProvider.Releases.First(x => x.Id == 0); // TODO: !!! _releaseMetadata
        }

        #endregion

        #region Methods

        public void Import()
        {
            Name = GetName();
            AssetsHelper = new AssetsHelper(Name);

            // deserialize
            ModelBlockItem = ModelBlock[ModelIndex];
            _dumper.DumpItemPartsBytes(ModelBlockItem, ModelIndex);
            ModelBlockItem.Load(out _bitSerializerContext);
            _dumper.DumpItemLog(_bitSerializerContext, ModelIndex);
            _offsetHexStringLength = GetOffsetHexStringLength();

            // import
            GameObject = new GameObject(Name);
            GameObject.AddComponent<ModelComponent>().Import(ModelBlockItem, this);
            AssetDatabase.SaveAssets();
        }

        private string GetName() // TODO: enumerate instead of using Path.GetRandomFilename()
        {
            string name = _metadataProvider.GetBlockItemValueName<Swe1rModelBlockItem>(ModelIndex, _releaseMetadata);
            return $"{ModelIndex:000} - {name} - {Path.GetRandomFileName()}";
        }

        private int GetOffsetHexStringLength()
        {
            int length = ModelBlockItem.Data.Length.ToString("x").Length;
            if (length % 2 == 1)
                length++;
            return length;
        }

        #endregion

        #region Methods (ScriptableObjects)

        public TResult GetScriptableObject<TResult, TSource>(TSource source) where TResult : AbstractScriptableObject<TSource>
        {
            if (source == null)
                return null;

            Dictionary<object, ScriptableObject> scriptableObjectsBySource = scriptableObjectsBySourceBySourceType
                .GetOrCreate(typeof(TSource), x => new Dictionary<object, ScriptableObject>());

            if (scriptableObjectsBySource.TryGetValue(source, out ScriptableObject existingResult))
                return (TResult)existingResult;
            else
            {
                TResult newResult = ScriptableObject.CreateInstance<TResult>();
                scriptableObjectsBySource.Add(source, newResult);
                newResult.Import(source, this);

                string assetFolderName = source.GetType().Name;
                string assetFileName = $"{source.GetType().Name} {GetValueOffsetHexString(source)}.asset";
                AssetsHelper.SaveAsAsset(newResult, assetFolderName, assetFileName);

                return newResult;
            }
        }

        public List<T> GetSourceObjects<T>()
        {
            if (scriptableObjectsBySourceBySourceType.TryGetValue(
                typeof(T), out Dictionary<object, ScriptableObject> objectsBySource))
                return objectsBySource.Keys.Cast<T>().ToList();
            else
                return new List<T>();
        }

        public MaterialScriptableObject GetMaterialScriptableObject(Swe1rMaterial source) =>
            GetScriptableObject<MaterialScriptableObject, Swe1rMaterial>(source);

        public MaterialTextureScriptableObject GetMaterialTextureScriptableObject(Swe1rMaterialTexture source) =>
            GetScriptableObject<MaterialTextureScriptableObject, Swe1rMaterialTexture>(source);

        public MappingScriptableObject GetMappingScriptableObject(Swe1rMapping source) =>
            GetScriptableObject<MappingScriptableObject, Swe1rMapping>(source);

        public MappingSubScriptableObject GetMappingSubScriptableObject(Swe1rMappingSub source) =>
            GetScriptableObject<MappingSubScriptableObject, Swe1rMappingSub>(source);

        public MappingChildScriptableObject GetMappingChildScriptableObject(Swe1rMappingChild source) =>
            GetScriptableObject<MappingChildScriptableObject, Swe1rMappingChild>(source);

        #endregion

        #region Methods (mapping)

        public T GetFlaggedNodeComponent<T>(Swe1rFlaggedNode source) where T : FlaggedNodeComponent =>
            prefabByFlaggedNode[source].GetComponent<T>();

        public MaterialTextureChildObject GetMaterialTextureChildObject(Swe1rMaterialTextureChild source) =>
            materialTextureChildObjects.GetOrCreate(source, x => new MaterialTextureChildObject(x, this));

        public MaterialPropertiesObject GetMaterialPropertiesObject(Swe1rMaterialProperties source) =>
            materialPropertiesObjects.GetOrCreate(source, x => new MaterialPropertiesObject(x, this));

        public IndicesChunkObject GetIndicesChunkObject(Swe1rIndicesChunk source) =>
            indicesChunkObjects.GetOrCreate(source, ic => IndicesChunkObjectFactory.Instance.CreateIndicesChunkObject(ic, this));

        public VertexObject GetVertexObject(Swe1rVertex source) =>
            vertexObjects.GetOrCreate(source, x => new VertexObject(source));

        public MaterialReferenceObject GetMaterialReferenceObject(Swe1rMaterialReference source) =>
            materialReferenceObjects.GetOrCreate(source, x => new MaterialReferenceObject(x, this));

        #endregion

        #region Methods

        public FlaggedNodeComponent CreateFlaggedNodeGameObject(Swe1rFlaggedNode swe1rFlaggedNode, GameObject parentGameObject)
        {
            GameObject prefab;
            GameObject prefabInstance;
            FlaggedNodeComponent flaggedNodeComponent;

            if (swe1rFlaggedNode == null)
            {
                parentGameObject.AddChild().AddComponent<NullComponent>().Import();
                flaggedNodeComponent = null;
            }
            else if (prefabByFlaggedNode.TryGetValue(swe1rFlaggedNode, out prefab))
            {
                prefabInstance = (GameObject)PrefabUtility.InstantiatePrefab(prefab);
                prefabInstance.SetParent(parentGameObject);
                prefabInstance.SetActive(false);
                flaggedNodeComponent = prefabInstance.GetComponent<FlaggedNodeComponent>();
            }
            else
            {
                // create GameObject
                prefabInstance = parentGameObject.AddChild(GetName(swe1rFlaggedNode));

                // component
                Type componentType = FlaggedNodeComponentFactory.Instance.GetComponentType(swe1rFlaggedNode);
                var instanceflaggedNodeComponent = (FlaggedNodeComponent)prefabInstance.AddComponent(componentType);
                instanceflaggedNodeComponent.Import(swe1rFlaggedNode);

                // get or create children recursively
                if (swe1rFlaggedNode.Children != null)
                {
                    foreach (INode childNode in swe1rFlaggedNode.Children)
                    {
                        if (childNode == null)
                            CreateFlaggedNodeGameObject(null, prefabInstance);
                        if (childNode is Swe1rFlaggedNode childFlaggedNode)
                            CreateFlaggedNodeGameObject(childFlaggedNode, prefabInstance);
                        else if (childNode is Swe1rMesh childMesh)
                            prefabInstance.AddChild().AddComponent<MeshComponent>().Import(childMesh, this);
                    }
                }

                // save as asset and replace
                prefab = prefabByFlaggedNode[swe1rFlaggedNode] = AssetsHelper.SaveAsPrefabAssetAndConnect(instanceflaggedNodeComponent);
                flaggedNodeComponent = prefab.GetComponent<FlaggedNodeComponent>();
            }
            return flaggedNodeComponent;
        }

        public string GetName(object value) // TODO: better method name
        {
            string className = value.GetType().Name;
            string offsetHexString = GetValueOffsetHexString(value);
            return $"{className} @ {offsetHexString} ";
        }

        private string GetValueOffsetHexString(object value) =>
            _bitSerializerContext.Graph.GetValueComponent(value)
                .Position.Value.ToString($"X{_offsetHexStringLength}");

        #endregion
    }
}
