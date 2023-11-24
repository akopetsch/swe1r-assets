// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using SWE1R.Assets.Blocks.Unity.Components.Models.Types;
using System;
using UnityEngine;
using Swe1rModelBlockItem = SWE1R.Assets.Blocks.ModelBlock.ModelBlockItem;

namespace SWE1R.Assets.Blocks.Unity.Components.Models
{
    public class ModelComponent : MonoBehaviour
    {
        public void Import(Swe1rModelBlockItem modelBlockItem, ModelImporter importer)
        {
            // headerComponent
            Type headerComponentType = HeaderComponentFactory.Instance.GetComponentType(modelBlockItem.Header);
            ((IHeaderComponent)gameObject.AddComponent(headerComponentType)).Import(modelBlockItem.Header, importer);

            FixTransform();
        }

        private void FixTransform()
        {
            float scale = 0.1f;
            gameObject.transform.Rotate(-90, 0, 0);
            gameObject.transform.localScale = new Vector3(-scale, scale, scale);
        }

        public Swe1rModelBlockItem Export(ModelExporter exporter)
        {
            var result = new Swe1rModelBlockItem();
            result.Header = GetComponent<IHeaderComponent>().Export(exporter);
            return result;
        }
    }
}
