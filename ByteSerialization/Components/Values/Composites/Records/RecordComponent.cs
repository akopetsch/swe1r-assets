// Copyright 2024 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization.Attributes;
using ByteSerialization.Nodes;
using ByteSerialization.Pooling;
using ByteSerialization.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ByteSerialization.Components.Values.Composites.Records
{
    public class RecordComponent : CompositeComponent
    {
        #region Properties

        public PropertyComponentList Properties { get; private set; }
        public List<AttributeComponent> AttributeComponents { get; private set; } // TODO: use pooling

        public INodeListener NodeListener { get; private set; }

        private bool IsInitialized { get; set; } = false; // HACK: remove IsInitialized

        #endregion

        #region Methods (: Component)

        public override void OnAdded()
        {
            base.OnAdded();
            
            Properties = UniversalPool.Instance.Get<PropertyComponentList>();
            AttributeComponents = new List<AttributeComponent>();

            Node.BeforeSerializing += BeforeSerializing;

            // TODO: called only during deserialization:
            Node.ValueChanged += OnValueChanged;
            Node.TypeChanged += OnTypeChanged;
        }

        private void OnValueChanged(object before, object after) =>
            TrySubscription();

        #endregion

        private void OnTypeChanged(Type before, Type after)
        {
            if (before == null)
            {
                // type initialized
                AddPropertyAndAttributeComponents(after.GetHierarchy());
            }
            else if (before.IsAssignableFrom(after))
            {
                // type specialized
                AddPropertyAndAttributeComponents(after.GetHierarchy()
                    .Except(before.GetHierarchy()).ToList());
            }
            else
                throw new NotImplementedException();
            
            // create value
            Node.Value = CreateValue();
        }

        private void AddPropertyAndAttributeComponents(List<Type> typeHierarchyDifference)
        {
            // add properties
            Properties.AddRange(CreateProperties(typeHierarchyDifference));

            // add attributes
            List<ByteSerializationAttribute> attributes = 
                typeHierarchyDifference.SelectMany(t => t.GetAttributes()).ToList();
            AttributeComponents.AddRange(AddAttributeComponents(attributes));
            // TODO: type specialization: are all attributes inherited?

            IsInitialized = true;
        }

        #region Methods (: ISerializableComponent)

        private void TrySubscription()
        {
            NodeListener?.UnsubscribeFrom(this);
            NodeListener = Node.Value as INodeListener;
            NodeListener?.SubscribeTo(this);
        }

        #endregion

        #region Methods (properties)

        private IEnumerable<PropertyComponent> CreateProperties(IEnumerable<Type> types) =>
            types.SelectMany(t => CreateProperties(t));

        private IEnumerable<PropertyComponent> CreateProperties(Type type) =>
            type.GetOrderedPropertyInfos().Select(info => CreateProperty(info));

        private PropertyComponent CreateProperty(PropertyInfo info)
        {
            var p = AddChild<PropertyComponent>();
            p.Init(this, info);
            return p;
        }

        #endregion

        #region Methods (serialize)

        private void BeforeSerializing()
        {
            if (Value == null)
                Node.IsSerializationCancelled = true;
            else if (!IsInitialized)
            {
                TrySubscription();
                AddPropertyAndAttributeComponents(Type.GetHierarchy()); // TODO: also when Value == null?
            }
        }

        public override void Serialize()
        {
            if (!Node.IsSerializationCancelled)
            {
                // serialize children
                foreach (Node child in Children)
                    child.Serialize();
            }
        }

        #endregion

        #region Methods (deserialize)

        public override void Deserialize()
        {
            if (!IsInitialized)
            {
                AddPropertyAndAttributeComponents(Type.GetHierarchy());
                Node.Value = CreateValue();
            }

            // deserialize children
            var processed = new List<Node>();
            var queue = new List<Node>(Children);
            while (queue.Count > 0)
            {
                foreach (Node child in queue)
                {
                    child.Deserialize();
                    processed.Add(child);
                }
                queue.Clear();
                queue.AddRange(Children.Except(processed));
            }

            // this
            Properties.ForEach(p => p.UpdateRecord());
        }

        public object CreateValue()
        {
            Type t = Node.Type;
            if (!t.IsAbstract)
                return Activator.CreateInstance(t);
            else
                return null;
        }

        #endregion
    }
}
