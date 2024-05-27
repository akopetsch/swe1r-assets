// Copyright 2024 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization.Attributes.Helpers;
using ByteSerialization.Attributes.Types.TypeHelper;
using System;

namespace ByteSerialization.Attributes
{
    [AttributeComponent(typeof(TypeHelperComponent))]
    [AttributeUsage(AttributeTargets.Property)]
    public class TypeHelperAttribute : ByteSerializationAttribute
    {
        public ITypeHelper Helper { get; }
        public Type HelperType { get; }

        public TypeHelperAttribute(Type helperType) =>
            Helper = helperType.GetTypeHelper();
    }
}
