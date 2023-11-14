// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization;
using SWE1R.Assets.Blocks.ModelBlock;
using SWE1R.Assets.Blocks.Unity.Components.Models;
using SWE1R.Assets.Blocks.Unity.Components.Models.Meshes;
using SWE1R.Assets.Blocks.Unity.Components.Models.Nodes;
using SWE1R.Assets.Blocks.Unity.Extensions;
using SWE1R.Assets.Blocks.Unity.Objects;
using SWE1R.Assets.Blocks.Unity.ScriptableObjects;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Swe1rDoubleMaterial = SWE1R.Assets.Blocks.ModelBlock.Animations.DoubleMaterial;
using Swe1rFlaggedNode = SWE1R.Assets.Blocks.ModelBlock.Nodes.FlaggedNode;
using Swe1rINode = SWE1R.Assets.Blocks.ModelBlock.Nodes.INode; // TODO: alias name should start with 'I'?
using Swe1rKeyframesOrInteger = SWE1R.Assets.Blocks.ModelBlock.Animations.KeyframesOrInteger;
using Swe1rMapping = SWE1R.Assets.Blocks.ModelBlock.Meshes.Mapping;
using Swe1rMappingChild = SWE1R.Assets.Blocks.ModelBlock.Meshes.MappingChild;
using Swe1rMaterial = SWE1R.Assets.Blocks.ModelBlock.Meshes.Material;
using Swe1rMaterialProperties = SWE1R.Assets.Blocks.ModelBlock.Meshes.MaterialProperties;
using Swe1rMaterialTexture = SWE1R.Assets.Blocks.ModelBlock.Meshes.MaterialTexture;
using Swe1rMaterialTextureChild = SWE1R.Assets.Blocks.ModelBlock.Meshes.MaterialTextureChild;
using Swe1rMesh = SWE1R.Assets.Blocks.ModelBlock.Meshes.Mesh;
using Swe1rModel = SWE1R.Assets.Blocks.ModelBlock.Model;
using Swe1rTargetOrInteger = SWE1R.Assets.Blocks.ModelBlock.Animations.TargetOrInteger;
using Swe1rVertex = SWE1R.Assets.Blocks.ModelBlock.Meshes.Vertex;

namespace SWE1R.Assets.Blocks.Unity
{
    public class ModelExporter
    {
        #region Fields

        private readonly BlockItemDumper dumper = new UnityBlockItemDumper();
        private const string dumperSuffix = "out";
        private ByteSerializerContext bitSerializerContext;

        #endregion

        #region Fields (mapping)

        private Dictionary<GameObject, Swe1rFlaggedNode> flaggedNodeByPrefab =
            new Dictionary<GameObject, Swe1rFlaggedNode>();

        private Dictionary<MaterialScriptableObject, Swe1rMaterial> materials =
            new Dictionary<MaterialScriptableObject, Swe1rMaterial>();

        private Dictionary<MaterialTextureScriptableObject, Swe1rMaterialTexture> materialTextures =
            new Dictionary<MaterialTextureScriptableObject, Swe1rMaterialTexture>();

        private Dictionary<MaterialTextureChildObject, Swe1rMaterialTextureChild> materialTextureChildren =
            new Dictionary<MaterialTextureChildObject, Swe1rMaterialTextureChild>();

        private Dictionary<MaterialPropertiesObject, Swe1rMaterialProperties> materialProperties =
            new Dictionary<MaterialPropertiesObject, Swe1rMaterialProperties>();

        private Dictionary<MappingScriptableObject, Swe1rMapping> mappings =
            new Dictionary<MappingScriptableObject, Swe1rMapping>();

        private Dictionary<MappingChildScriptableObject, Swe1rMappingChild> mappingChildren =
            new Dictionary<MappingChildScriptableObject, Swe1rMappingChild>();

        private Dictionary<VertexObject, Swe1rVertex> vertices =
            new Dictionary<VertexObject, Swe1rVertex>();

        private Dictionary<DoubleMaterialObject, Swe1rDoubleMaterial> doubleMaterials =
            new Dictionary<DoubleMaterialObject, Swe1rDoubleMaterial>();

        private Dictionary<KeyframesOrIntegerObject, Swe1rKeyframesOrInteger> keyframesOrIntegers =
            new Dictionary<KeyframesOrIntegerObject, Swe1rKeyframesOrInteger>();

        private Dictionary<TargetOrIntegerObject, Swe1rTargetOrInteger> targetOrIntegers =
            new Dictionary<TargetOrIntegerObject, Swe1rTargetOrInteger>();

        #endregion

        #region Properties (constructor)

        public ModelComponent ModelComponent { get; }
        public int ModelIndex { get; private set; }

        #endregion

        #region Properties (export)

        public Swe1rModel Model { get; private set; }

        #endregion

        #region Constructor

        public ModelExporter(ModelComponent modelComponent, int modelIndex)
        {
            ModelComponent = modelComponent;
            ModelIndex = modelIndex;
        }

        #endregion

        #region Methods (export)

        public void Export()
        {
            // export
            Model = ModelComponent.Export(this);

            // serialize
            Model.Save(out bitSerializerContext);
            dumper.DumpItem(Model, ModelIndex, bitSerializerContext, dumperSuffix);
        }

        #endregion

        #region Methods (mapping)

        public Swe1rFlaggedNode GetFlaggedNode(GameObject gameObject)
        {
            GameObject prefab = PrefabUtility.GetCorrespondingObjectFromOriginalSource(gameObject);
            if (prefab == null)
                return null; // TODO: ever called?
            else if (flaggedNodeByPrefab.TryGetValue(prefab, out Swe1rFlaggedNode swe1rFlaggedNode))
                return swe1rFlaggedNode;
            else
                return CreateFlaggedNode(prefab);
        }

        public Swe1rMaterial GetMaterial(MaterialScriptableObject materialObject) =>
            materialObject == null ? null : materials.GetOrCreate(materialObject, x => x.Export(this));

        public Swe1rMaterialTexture GetMaterialTexture(MaterialTextureScriptableObject materialTexureObject) =>
            materialTextures.GetOrCreate(materialTexureObject, x => x.Export(this));

        public Swe1rMaterialTextureChild GetMaterialTextureChild(MaterialTextureChildObject materialTexureChildObject) =>
            materialTextureChildren.GetOrCreate(materialTexureChildObject, x => x.Export());

        public Swe1rMaterialProperties GetMaterialProperties(MaterialPropertiesObject materialPropertiesObject) =>
            materialProperties.GetOrCreate(materialPropertiesObject, x => x.Export());

        public Swe1rMapping GetMapping(MappingScriptableObject mappingObject) =>
            mappings.GetOrCreate(mappingObject, x => x.Export(this));

        public Swe1rMappingChild GetMappingChild(MappingChildScriptableObject mappingChildObject) =>
            mappingChildren.GetOrCreate(mappingChildObject, x => x.Export(this));

        public Swe1rVertex GetVertex(VertexObject vertexObject) =>
            vertices.GetOrCreate(vertexObject, x => x.Export());

        public Swe1rDoubleMaterial GetDoubleMaterial(DoubleMaterialObject doubleMaterialObject) =>
            doubleMaterials.GetOrCreate(doubleMaterialObject, x => x.Export(this));

        public Swe1rTargetOrInteger GetTargetOrInteger(TargetOrIntegerObject targetOrInteger) =>
            targetOrIntegers.GetOrCreate(targetOrInteger, x => x.Export(this));

        #endregion

        #region Methods (private)

        private Swe1rFlaggedNode CreateFlaggedNode(GameObject prefab)
        {
            var flaggedNodeComponent = prefab.GetComponent<FlaggedNodeComponent>();
            if (flaggedNodeComponent == null)
                return null;
            Swe1rFlaggedNode swe1rFlaggedNode = flaggedNodeComponent.Export(this);
            flaggedNodeByPrefab[prefab] = swe1rFlaggedNode;

            List<GameObject> childGameObjects = prefab.GetChildren();
            if (childGameObjects.Count > 0)
            {
                swe1rFlaggedNode.Children = new List<Swe1rINode>();
                foreach (GameObject childGameObject in childGameObjects)
                {
                    GameObject childPrefab = PrefabUtility.GetCorrespondingObjectFromOriginalSource(childGameObject);

                    Swe1rMesh swe1rMesh = childPrefab.GetComponent<MeshComponent>()?.Export(this);
                    if (swe1rMesh != null)
                        swe1rFlaggedNode.Children.Add(swe1rMesh);
                    else
                        swe1rFlaggedNode.Children.Add(GetFlaggedNode(childPrefab));
                }
            }
            swe1rFlaggedNode.UpdateChildrenCount();

            return swe1rFlaggedNode;
        }

        #endregion
    }
}
