// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization.Attributes.Alignment;
using ByteSerialization.Attributes.Helpers;
using System;

namespace ByteSerialization.Attributes
{
    [AttributeComponent(typeof(AlignmentComponent))]
    [AttributeUsage(AttributeTargets.Class)]
    public class AlignmentAttribute : ByteSerializationAttribute
    {
        public int? Value { get; }
        public IAlignmentHelper Helper { get; }

        public AlignmentAttribute(int alignment) => 
            Value = alignment;

        public AlignmentAttribute(Type helperType) => 
            Helper = helperType.GetAlignmentHelper();
    }
}
