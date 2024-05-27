// Copyright 2024 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization.Attributes;
using ByteSerialization.Attributes.Reference;
using ByteSerialization.Nodes;
using System.Collections.Generic;
using System.Linq;

namespace ByteSerialization.Components.Attributes.Reference
{
    // TODO: responsibilites overlap with RootComponent/CompositeComponent/ReferenceComponent

    public sealed class ReferencesCollectorComponent : Component
    {
        #region Properties

        public List<ReferenceComponent> References { get; } = new List<ReferenceComponent>();

        #endregion

        #region Methods

        public override void OnAdded()
        {
            base.OnAdded();

            Node.AfterSerializing += SerializeReferences;
            Node.AfterDeserializing += DeserializeReferences;
        }

        private void SerializeReferences()
        {
            var references = References.Where(r => r.Value != null)
                .OrderBy(r => r.Node.Depth)
                .ToList();

            // by priority
            SerializeReferences(references, ReferenceHandling.HighPriority);
            SerializeReferences(references, ReferenceHandling.DefaultPriority);
            SerializeReferences(references, ReferenceHandling.LowPriority);

            // Postpone & ForceReuse
            if (Node.IsRoot)
            {
                SerializeReferences(Graph.Root.PostponedReferences);
                foreach (ReferenceComponent r in Graph.Root.ForceReuseReferences)
                    r.ReuseSerializedValueComponent();
            }
        }

        private void SerializeReferences(List<ReferenceComponent> references, ReferenceHandling handling)
        {
            var rs = references
                .Where(r => r.Attribute.Configuration.Handling == handling)
                .ToList();
            SerializeReferences(rs);
        }

        private void SerializeReferences(List<ReferenceComponent> references)
        {
            foreach (ReferenceComponent r in references)
                if (!r.ValueComponent.Node.IsSerialized)
                    r.ValueComponent.Node.Serialize();
        }

        private void DeserializeReferences()
        {
            // by priority
            var byParent = References
                .Where(r => !r.HasNullPointer)
                .GroupBy(r => r.Node.Parent)
                .ToList();
            foreach (IGrouping<Node, ReferenceComponent> rs in byParent)
                DeserializeReferences(rs.ToList());

            // Postpone & ForceReuse
            if (Node.IsRoot)
            {
                DeserializeReferences(Graph.Root.PostponedReferences);
                foreach (ReferenceComponent r in Graph.Root.ForceReuseReferences)
                    r.ReuseDeserializedValueComponent();
            }
        }

        private void DeserializeReferences(List<ReferenceComponent> references)
        {
            foreach (ReferenceComponent r in references.ToList())
                if (r.ValueComponent.Value == null)
                {
                    Context.Position = r.Pointer.Value;
                    r.ValueComponent.Node.Deserialize();
                }
        }

        #endregion
    }
}
