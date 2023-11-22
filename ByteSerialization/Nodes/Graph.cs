// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization.Attributes.Reference;
using ByteSerialization.Components;
using ByteSerialization.Components.Values;
using ByteSerialization.Components.Values.Composites.Records;
using ByteSerialization.Extensions;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace ByteSerialization.Nodes
{
    public class Graph
    {
        #region Fields

        private readonly ConcurrentDictionary<long?, List<Node>> nodesByPosition =
            new ConcurrentDictionary<long?, List<Node>>();

        private readonly ConcurrentDictionary<object, List<ValueComponent>> valueComponentsByValue =
            new ConcurrentDictionary<object, List<ValueComponent>>();

        #endregion

        #region Properties

        public List<Node> Nodes { get; } = new List<Node>();
        
        public RootComponent Root { get; set; }

        public List<Component> Components { get; } = new List<Component>();
        public List<ValueComponent> ValueComponents { get; } = new List<ValueComponent>();
        public List<ReferenceComponent> References { get; } = new List<ReferenceComponent>();

        #endregion

        #region Methods

        public void AddNode(Node node)
        {
            Nodes.Add(node);

            node.PositionChanged += NodePositionChanged;
        }

        private void NodePositionChanged(Node node, long? before, long? after)
        {
            if (before.HasValue)
                GetNodesByPosition(before).Remove(node);

            if (after.HasValue)
                GetNodesByPosition(after).Add(node);
        }

        public void AddComponent(Component component)
        {
            Components.Add(component);

            if (component is ReferenceComponent)
                References.Add((ReferenceComponent) component);

            if (component is ValueComponent)
            {
                var valueComponent = (ValueComponent)component;
                ValueComponents.Add(valueComponent);

                // add to ValueComponentByValue
                if (valueComponent.Value != null)
                    GetValueComponentsByValue(valueComponent.Value).Add(valueComponent);
                else
                {
                    valueComponent.Node.ValueChanged += (before, after) => {
                        if (before != null)
                            GetValueComponentsByValue(before).Remove(valueComponent);
                        GetValueComponentsByValue(after).Add(valueComponent);
                    };
                }
            }
        }

        public ValueComponent GetValueComponent<TValue>() =>
            GetValueComponents<TValue>().FirstOrDefault();

        public IEnumerable<ValueComponent> GetValueComponents<TValue>() =>
            ValueComponents.Where(vc => typeof(TValue).IsAssignableFrom(vc.Type));

        public ValueComponent GetValueComponent(object value)
        {
            if (value == null)
                return null;
            else
                return GetValueComponentsByValue(value).FirstOrDefault();
        }

        public ValueComponent GetValueComponent(Type type, long position) =>
            GetValueComponents(position)
                .Where(vc => type.IsAssignableFrom(vc.Type))
                .SingleOrDefault();

        public ValueComponent GetValueComponent<TValue>(long position) =>
            GetValueComponent(typeof(TValue), position);

        public IEnumerable<ValueComponent> GetValueComponents(long position) =>
            GetNodesByPosition(position)
                .Select(n => n.Get<ValueComponent>())
                .Where(vn => vn != null);

        public TValue GetValue<TValue>() =>
            GetValues<TValue>().FirstOrDefault();

        public TValue GetValue<TValue>(long position) =>
            (TValue)GetValueComponent<TValue>(position).Value;

        public IEnumerable<TValue> GetValues<TValue>() =>
            GetValueComponents<TValue>().Select(vc => (TValue)vc.Value);

        public RecordComponent GetRecordComponent<TValue>() =>
            GetRecordComponents<TValue>().FirstOrDefault();

        public IEnumerable<RecordComponent> GetRecordComponents<TValue>() =>
            GetValueComponents<TValue>().OfType<RecordComponent>();

        #endregion

        #region Methods (dictionary helpers)

        private List<Node> GetNodesByPosition(long? position) =>
            nodesByPosition.GetOrAdd(position, x => new List<Node>());

        private List<ValueComponent> GetValueComponentsByValue(object value) =>
            valueComponentsByValue.GetOrAdd(value, x => new List<ValueComponent>());

        #endregion
    }
}
