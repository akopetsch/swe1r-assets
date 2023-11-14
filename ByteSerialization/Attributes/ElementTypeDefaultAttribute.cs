// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization.Attributes.Elements.ElementTypeDefault;
using System;

namespace ByteSerialization.Attributes
{
    [AttributeComponent(typeof(ElementTypeDefaultComponent))]
    [AttributeUsage(AttributeTargets.Property)]
    public class ElementTypeDefaultAttribute : Attribute
    {
        public Type Type { get; }

        public ElementTypeDefaultAttribute(Type type) => 
            Type = type;
    }
}
