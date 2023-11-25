// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using SWE1R.Assets.Blocks.Unity.Extensions;
using System.Collections.Generic;
using UnityEngine;
using Swe1rFlaggedNodeOrGroup5066ChildReference = SWE1R.Assets.Blocks.ModelBlock.FlaggedNodeOrGroup5066ChildReference;
using Swe1rModel = SWE1R.Assets.Blocks.ModelBlock.Model;
using Swer1rFlaggedNode = SWE1R.Assets.Blocks.ModelBlock.Nodes.FlaggedNode;

namespace SWE1R.Assets.Blocks.Unity.Components.Models
{
    public class AltNComponent : MonoBehaviour
    {
        public void Import(List<Swe1rFlaggedNodeOrGroup5066ChildReference> source, ModelImporter importer)
        {
            gameObject.name = nameof(Swe1rModel.AltN);

            foreach (Swe1rFlaggedNodeOrGroup5066ChildReference item in source)
            {
                if (item.FlaggedNode != null)
                    importer.CreateFlaggedNodeGameObject(item.FlaggedNode, gameObject);
                else
                    gameObject.AddChild().AddComponent<Group5066ChildReferenceComponent>().Import(item.Group5066ChildReference, importer);
            }
        }

        public List<Swe1rFlaggedNodeOrGroup5066ChildReference> Export(ModelExporter exporter)
        {
            var result = new List<Swe1rFlaggedNodeOrGroup5066ChildReference>();
            foreach (GameObject go in gameObject.GetChildren())
            {
                var item = new Swe1rFlaggedNodeOrGroup5066ChildReference();

                Swer1rFlaggedNode flaggedNode = exporter.GetFlaggedNode(go);
                if (flaggedNode != null)
                    item.FlaggedNode = flaggedNode;
                else
                    item.Group5066ChildReference =
                            go.GetComponent<Group5066ChildReferenceComponent>().Export(exporter);

                result.Add(item);
            }
            return result;
        }
    }
}
