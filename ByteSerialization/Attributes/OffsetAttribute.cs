﻿// Copyright 2024 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization.Attributes.Offset;
using System;

namespace ByteSerialization.Attributes
{
    [AttributeComponent(typeof(OffsetComponent))]
    [AttributeUsage(AttributeTargets.Property)]
    public class OffsetAttribute : ByteSerializationAttribute
    {
        public int Value { get; }

        public OffsetAttribute(int value) => 
            Value = value;
    }
}
