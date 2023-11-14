// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using SWE1R.Assets.Blocks.Unity.Components.Models.Types;
using System;
using UnityEngine;
using Swe1rModel = SWE1R.Assets.Blocks.ModelBlock.Model;

namespace SWE1R.Assets.Blocks.Unity.Components.Models
{
    public class ModelComponent : MonoBehaviour
    {
        public void Import(Swe1rModel model, ModelImporter importer)
        {
            // headerComponent
            Type headerComponentType = HeaderComponentFactory.Instance.GetComponentType(model.Header);
            ((IHeaderComponent)gameObject.AddComponent(headerComponentType)).Import(model.Header, importer);

            FixTransform();
        }

        private void FixTransform()
        {
            float scale = 0.1f;
            gameObject.transform.Rotate(-90, 0, 0);
            gameObject.transform.localScale = new Vector3(-scale, scale, scale);
        }

        public Swe1rModel Export(ModelExporter exporter)
        {
            var result = new Swe1rModel();
            result.Header = GetComponent<IHeaderComponent>().Export(exporter);
            return result;
        }
    }
}
