// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization.Attributes.Elements.ElementTypeHelper;
using ByteSerialization.Attributes.Helpers;
using System;

namespace ByteSerialization.Attributes
{
    [AttributeComponent(typeof(ElementTypeHelperComponent))]
    [AttributeUsage(AttributeTargets.Property)]
    public class ElementTypeHelperAttribute : Attribute
    {
        public IElementTypeHelper Helper { get; }
        
        public ElementTypeHelperAttribute(Type helperType) => 
            Helper = helperType.GetElementTypeHelper();
    }
}
