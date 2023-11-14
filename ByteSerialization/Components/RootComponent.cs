// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization.Attributes.Reference;
using ByteSerialization.Components.Attributes.Reference;
using ByteSerialization.Components.Values;
using ByteSerialization.Nodes;
using System.Collections.Generic;

namespace ByteSerialization.Components
{
    [Require(typeof(ReferencesCollectorComponent))]
    public sealed class RootComponent : Component
    {
        #region Properties

        public ValueComponent ValueComponent { get; private set; }

        public List<ReferenceComponent> PostponedReferences { get; } = new List<ReferenceComponent>();
        public List<ReferenceComponent> ForceReuseReferences { get; } = new List<ReferenceComponent>();

        #endregion

        #region Methods

        public override void OnAdded()
        {
            base.OnAdded();

            ValueComponent = Node.AddValueComponent(Node.Type);
        }

        #endregion
    }
}
