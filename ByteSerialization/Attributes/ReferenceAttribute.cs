// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization.Attributes.Reference;
using ByteSerialization.Components.Attributes.Reference;
using System;

namespace ByteSerialization.Attributes
{
    [AttributeComponent(typeof(ReferenceComponent))]
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class ReferenceAttribute : ByteSerializationAttribute
    {
        public ReferenceConfiguration Configuration { get; } = new ReferenceConfiguration();

        public ReferenceAttribute() { }
        public ReferenceAttribute(int order) => Configuration.Order = order;
        public ReferenceAttribute(ReferenceHandling handling) => Configuration.Handling = handling;
        public ReferenceAttribute(ReferenceConfiguration configuration) => Configuration = configuration;
    }
}
