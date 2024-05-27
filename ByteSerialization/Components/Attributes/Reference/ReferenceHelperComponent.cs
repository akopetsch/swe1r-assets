// Copyright 2024 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization.Attributes;
using ByteSerialization.Attributes.Reference;
using ByteSerialization.Components.Values.Composites.Records;
using System;

namespace ByteSerialization.Components.Attributes.Reference
{
    public class ReferenceHelperComponent : AttributeComponent<ReferenceHelperAttribute>
    {
        private bool isHelperEvaluated = false;

        protected override void OnInitialized()
        {
            base.OnInitialized();

            EvaluateHelper();

            // Type might be object first and specialize later (TypeChanged)
            if (Mode == ByteSerializerMode.Deserializing)
                Node.TypeChanged += (before, after) => EvaluateHelper();
        }

        private void EvaluateHelper()
        {
            if (Mode == ByteSerializerMode.Deserializing)
            {
                // helper is not evaluated if Type is object (because Type is not specialized yet)
                if (Type == typeof(object))
                    return;

                // Type can only be specialized once
                if (isHelperEvaluated)
                    throw new InvalidOperationException(
                        $"{nameof(EvaluateHelper)} can be called only once.");
            }

            var propertyComponent = (PropertyComponent)Target;
            if (Attribute.Helper.IsReference(propertyComponent))
            {
                ReferenceConfiguration referenceConfiguration = 
                    Attribute.Helper.GetReferenceConfiguration(propertyComponent);
                Node.Add<ReferenceComponent>().Init(this, new ReferenceAttribute(referenceConfiguration));
                // TODO: do not create ReferenceAttribute, use ReferenceConfiguration only
            }

            isHelperEvaluated = true;
        }
    }
}
