// Copyright 2024 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization.Attributes;
using ByteSerialization.Attributes.Reference;
using ByteSerialization.Components.Attributes.Reference;
using ByteSerialization.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace ByteSerialization.Components.Values.Composites
{
    public abstract class CompositeComponent : ValueComponent
    {
        public ReferencesCollectorComponent Group { get; private set; }
        
        public override void OnAdded()
        {
            base.OnAdded();
            Group = Get<ReferencesCollectorComponent>() ?? GetAncestor<ReferencesCollectorComponent>();

            Node.OnSerialized += AddReferencesToGroup;
            Node.OnDeserialized += AddReferencesToGroup;
        }

        private void AddReferencesToGroup()
        {
            var references = GetChildren<ReferenceComponent>()
                .Where(r => !r.IsNullReference)
                .OrderBy(r => r.Attribute.Configuration.Order)
                .ToList();
            Group.References.AddRangeIfAny(GetReferences(references, ReferenceHandling.HighPriority));
            Group.References.AddRangeIfAny(GetReferences(references, ReferenceHandling.DefaultPriority));
            Group.References.AddRangeIfAny(GetReferences(references, ReferenceHandling.LowPriority));
            Graph.Root.PostponedReferences.AddRangeIfAny(GetReferences(references, ReferenceHandling.Postpone));
            Graph.Root.ForceReuseReferences.AddRangeIfAny(GetReferences(references, ReferenceHandling.ForceReuse));
        }

        private List<ReferenceComponent> GetReferences(List<ReferenceComponent> references, ReferenceHandling handling) =>
            references.Where(r => r.Attribute.Configuration.Handling == handling).ToList();
    }
}
