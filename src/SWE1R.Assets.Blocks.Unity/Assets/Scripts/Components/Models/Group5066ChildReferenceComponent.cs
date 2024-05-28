// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using SWE1R.Assets.Blocks.ModelBlock.Nodes;
using SWE1R.Assets.Blocks.Unity.Components.Models.Nodes;
using UnityEngine;
using Swe1rGroup5066ChildReference = SWE1R.Assets.Blocks.ModelBlock.Group5066ChildReference;

namespace SWE1R.Assets.Blocks.Unity.Components.Models
{
    public class Group5066ChildReferenceComponent : MonoBehaviour
    {
        public Group5066Component group5066;
        public int index;

        public void Import(Swe1rGroup5066ChildReference source, ModelImporter importer)
        {
            group5066 = importer.GetFlaggedNodeComponent<Group5066Component>(source.Group5066);
            index = source.Index;

            gameObject.name = $"{group5066.gameObject.name} [{index}]";
        }

        public Swe1rGroup5066ChildReference Export(ModelExporter exporter)
        {
            var result = new Swe1rGroup5066ChildReference();

            result.Group5066 = (Group5066)exporter.GetFlaggedNode(group5066.gameObject);
            result.Index = index;
            
            return result;
        }
    }
}
