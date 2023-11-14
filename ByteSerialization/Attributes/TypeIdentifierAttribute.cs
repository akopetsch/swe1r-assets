// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization.Components.Attributes.Types;
using System;

namespace ByteSerialization.Attributes
{
    [AttributeComponent(typeof(TypeIdentifierComponent))]
    public class TypeIdentifierAttribute : AbstractTypeIdentifierAttribute
    {
        public TypeIdentifierAttribute(object identifier, Type type) : 
            base(identifier, type)
        { }
    }
}
