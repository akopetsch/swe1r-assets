// Copyright 2024 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization.Attributes.Conditional;
using System;

namespace ByteSerialization.Attributes
{
    [AttributeComponent(typeof(IndicatorComponent))]
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Class)]
    public class IndicatorAttribute : ByteSerializationAttribute
    {
        public string Value { get; }

        public IndicatorAttribute(string value) =>
            Value = value;
    }
}
