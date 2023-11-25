// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using SWE1R.Assets.Blocks.Unity.Extensions;
using SWE1R.Assets.Blocks.Unity.ScriptableObjects;
using UnityEngine;
using Swe1rMappingChild = SWE1R.Assets.Blocks.ModelBlock.Meshes.MappingChild;
using Swe1rModel = SWE1R.Assets.Blocks.ModelBlock.Model;

namespace SWE1R.Assets.Blocks.Unity.Components.Models.Types
{
    public abstract class ModelComponent<T> : 
        MonoBehaviour, IModelComponent where T : Swe1rModel, new()
    {
        #region Fields

        public NodesComponent nodesComponent;
        public DataComponent dataComponent;
        public AnimationsComponent animationsComponent;
        public AltNComponent altNComponent;

        #endregion

        #region Methods (import)

        public void Import(Swe1rModel model, ModelImporter importer) =>
            Import((T)model, importer);

        public virtual void Import(T source, ModelImporter importer)
        {
            // Nodes
            nodesComponent = gameObject.AddChild().AddComponent<NodesComponent>();
            nodesComponent.Import(source.Nodes, importer);
            
            // Data
            if (source.Data != null)
            {
                dataComponent = gameObject.AddChild().AddComponent<DataComponent>();
                dataComponent.Import(source.Data, importer);
            }

            // Animations
            if (source.Animations != null)
            {
                animationsComponent = gameObject.AddChild().AddComponent<AnimationsComponent>();
                animationsComponent.Import(source.Animations, importer);
            }

            // AltN
            if (source.AltN != null)
            {
                altNComponent = gameObject.AddChild().AddComponent<AltNComponent>();
                altNComponent.Import(source.AltN, importer);
            }

            ImportMappingChildPostponedReferences(importer);
        }

        private void ImportMappingChildPostponedReferences(ModelImporter importer)
        {
            foreach (Swe1rMappingChild source in importer.GetSourceObjects<Swe1rMappingChild>())
            {
                var scriptableObject = importer
                    .GetScriptableObject<MappingChildScriptableObject, Swe1rMappingChild>(source);
                scriptableObject.ImportFlaggedNode(source, importer);
            }
        }

        #endregion

        #region Methods (export)

        public virtual Swe1rModel Export(ModelExporter exporter)
        {
            var result = new T();

            result.Nodes = nodesComponent.Export(exporter);
            result.Data = dataComponent?.Export(exporter);
            result.Animations = animationsComponent?.Export(exporter);
            result.AltN = altNComponent?.Export(exporter);

            result.BlockItem = exporter.ModelBlockItem;

            return result;
        }

        #endregion
    }
}
