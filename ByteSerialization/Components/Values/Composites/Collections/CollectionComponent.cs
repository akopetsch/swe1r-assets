// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization.Attributes.Elements.ElementReference;
using ByteSerialization.Attributes.Elements.ElementReferenceHelper;
using ByteSerialization.Attributes.Elements.ElementTypeDefault;
using ByteSerialization.Attributes.Elements.ElementTypeHelper;
using ByteSerialization.Attributes.Elements.ElementTypeIdentifier;
using ByteSerialization.Attributes.Limiting;
using ByteSerialization.Components.Attributes.Reference;
using ByteSerialization.Components.Values.Composites.Records;
using ByteSerialization.Nodes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ByteSerialization.Components.Values.Composites.Collections
{
    public abstract class CollectionComponent : CompositeComponent
    {
        #region Properties

        public List<CollectionElementComponent> Elements { get; } = new List<CollectionElementComponent>();
        public abstract Type ElementType { get; }
        
        private ILimitingComponent Limiter { get; set; }

        private ElementTypeIdentifierComponent ElementTypeIdentifierComponent { get; set; }
        private ElementTypeHelperComponent ElementTypeHelperComponent { get; set; }
        private ElementTypeDefaultComponent ElementTypeDefaultComponent { get; set; }

        private ElementReferenceComponent ElementReferenceComponent { get; set; }
        private ElementReferenceHelperComponent ElementReferenceHelperComponent { get; set; }

        #endregion

        #region Methods

        protected abstract object CreateValue();
        public abstract void SetElementValue(int index, object value);
        
        #endregion

        #region Methods (serialize / deserialize)

        public override void Serialize()
        {
            GetComponents();

            if (Value == null)
                Node.IsSerializationCancelled = true;
            else
            {
                // create children
                List<object> values = (Value as IEnumerable).Cast<object>().ToList();
                for (int i = 0; i < values.Count; i++)
                    CreateElement(i, values[i]);

                // serialize children
                foreach (Node child in Children)
                {
                    ElementTypeIdentifierComponent?.MarkType(child);
                    child.Serialize();
                }
            }
        }

        public override void Deserialize()
        {
            GetComponents();

            // create/deserialize children
            int i = 0;
            while (HasNext())
                CreateElement(i++).Node.Deserialize();

            // this
            Node.Value = CreateValue();
            Elements.ForEach(e => e.UpdateCollection());
        }

        private void GetComponents()
        {
            var p = Get<PropertyComponent>() ?? Parent.Get<PropertyComponent>();

            // get components

            Limiter = p.AttributeComponents.OfType<ILimitingComponent>().Single();

            ElementTypeIdentifierComponent = p.AttributeComponents.OfType<ElementTypeIdentifierComponent>().FirstOrDefault();
            ElementTypeHelperComponent = p.AttributeComponents.OfType<ElementTypeHelperComponent>().FirstOrDefault();
            ElementTypeDefaultComponent = p.AttributeComponents.OfType<ElementTypeDefaultComponent>().FirstOrDefault();

            ElementReferenceComponent = p.AttributeComponents.OfType<ElementReferenceComponent>().FirstOrDefault();
            ElementReferenceHelperComponent = p.AttributeComponents.OfType<ElementReferenceHelperComponent>().FirstOrDefault();
        }

        private CollectionElementComponent CreateElement(int i, object value = null)
        {
            // add
            var element = AddChild<CollectionElementComponent>();
            Elements.Add(element);
            
            // type
            Type type;
            if (Mode == ByteSerializerMode.Serializing)
                type = value?.GetType() ?? GetDefaultType();
            else
                type = GetNextType();

            // init
            element.Init(this, i, type, value);

            return element;
        }

        private bool HasNext() =>
            !Limiter.IsDeserializedUntilEnd(this);

        private Type GetNextType() =>
            ElementTypeIdentifierComponent?.IdentifyType() ??
            ElementTypeHelperComponent?.Attribute.Helper.GetElementType(this) ??
            GetDefaultType();

        private Type GetDefaultType() =>
            ElementTypeDefaultComponent?.Attribute.Type ??
            ElementType;

        public ReferenceConfiguration GetNextReferenceConfiguration(Type elementType) =>
            ElementReferenceHelperComponent?.Attribute.Helper.GetReferenceConfiguration(this, elementType) ??
            ElementReferenceComponent?.Attribute.Configuration;

        #endregion
    }
}
