// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization.Attributes.Padding;
using System;

namespace ByteSerialization.Attributes
{
    [AttributeComponent(typeof(PaddingComponent))]
    [AttributeUsage(AttributeTargets.Property)]
    public class PaddingAttribute : Attribute
    {
        public int Alignment { get; }

        public PaddingAttribute(int alignment) =>
            Alignment = alignment;
    }
}
