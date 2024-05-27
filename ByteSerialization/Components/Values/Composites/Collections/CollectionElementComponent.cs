// Copyright 2024 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization.Attributes;
using ByteSerialization.Attributes.Reference;
using ByteSerialization.Components.Attributes.Reference;
using System;

namespace ByteSerialization.Components.Values.Composites.Collections
{
    public class CollectionElementComponent : Component
    {
        public CollectionComponent Collection { get; private set; }
        public int Index { get; private set; }
        public bool IsLastElement => 
            Collection?.Elements.IndexOf(this) == Collection.Elements.Count - 1;
        public CollectionElementComponent NextElement =>
            IsLastElement ? null : Collection?.Elements[Index + 1];

        public void Init(CollectionComponent collection, int index, Type type, object value)
        {
            // init
            Collection = collection;
            Index = index;

            // type, value
            Node.Type = type;
            Node.Value = value;

            ReferenceConfiguration referenceConfiguration = Collection.GetNextReferenceConfiguration(Node.Type);
            if (referenceConfiguration != null)
            {
                // reference
                var rc = Node.Add<ReferenceComponent>();
                rc.Init(this, new ReferenceAttribute(referenceConfiguration));
                // TODO: do not create ReferenceAttribute, use ReferenceConfiguration only
            }
            else
            {
                // value
                Node.AddValueComponent(Node.Type);
            }

            // update composite
            Node.ValueChanged += (v0, v1) => UpdateCollection();
        }

        public void UpdateCollection()
        {
            if (Collection.Value != null)
                Collection.SetElementValue(Index, Value);
        }
    }
}
