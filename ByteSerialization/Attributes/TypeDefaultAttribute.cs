// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization.Components.Attributes.Types;
using System;

namespace ByteSerialization.Attributes
{
    [AttributeComponent(typeof(TypeDefaultComponent))]
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class TypeDefaultAttribute : Attribute
    {
        public Type Type { get; }

        public TypeDefaultAttribute(Type type) =>
            Type = type;
    }
}
