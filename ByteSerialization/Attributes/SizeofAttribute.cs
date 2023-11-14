// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization.Attributes.Limiting.Sizeof;
using System;

namespace ByteSerialization.Attributes
{
    [AttributeComponent(typeof(SizeofComponent))]
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Class)]
    public class SizeofAttribute : BindingAttribute
    {
        public int Multiplier { get; set; } = 1;
        
        public SizeofAttribute(int value) : 
            base(value) { }

        public SizeofAttribute(string propertyName) : 
            base(propertyName) { }

        public SizeofAttribute(Type bindingHelperType) : 
            base(bindingHelperType) { }
    }
}
