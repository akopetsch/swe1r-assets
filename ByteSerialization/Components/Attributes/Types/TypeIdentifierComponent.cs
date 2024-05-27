// Copyright 2024 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization.Attributes;
using ByteSerialization.Attributes.Reference;
using ByteSerialization.Components.Values;
using ByteSerialization.Components.Values.Composites.Records;
using System.Linq;

namespace ByteSerialization.Components.Attributes.Types
{
    public class TypeIdentifierComponent : AbstractTypeIdentifierComponent<TypeIdentifierAttribute>
    {
        #region Methods

        protected override void OnInitialized()
        {
            base.OnInitialized();

            Node.OnSerializing += () => MarkType(Node);
            Node.BeforeDeserializing += DefineType;
        }

        private void DefineType()
        {
            var propertyComponent = (PropertyComponent)Target;
            var typeDefaultComponent = propertyComponent.AttributeComponents.OfType<TypeDefaultComponent>().SingleOrDefault();
            Node.Type = IdentifyType() ?? typeDefaultComponent.Attribute.Type;

            if (!Has<ReferenceComponent>() && Node.Get<ValueComponent>() == null)
                Node.AddValueComponent(Node.Type); // TODO: code duplication in TypeHelperComponent
        }

        #endregion
    }
}
