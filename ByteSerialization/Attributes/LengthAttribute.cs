// Copyright 2024 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization.Attributes.Limiting.Length;
using System;

namespace ByteSerialization.Attributes
{
    [AttributeComponent(typeof(LengthComponent))]
    [AttributeUsage(AttributeTargets.Property)]
    public class LengthAttribute : BindingAttribute
    {
        public LengthAttribute(int value) : 
            base(value) { }

        public LengthAttribute(string propertyName) : 
            base(propertyName) { }

        public LengthAttribute(Type bindingHelperType) : 
            base(bindingHelperType) { }
    }
}
