// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization.Attributes.Limiting.SerializeUntil;
using System;

namespace ByteSerialization.Attributes
{
    [AttributeComponent(typeof(SerializeUntilComponent))]
    [AttributeUsage(AttributeTargets.Property)]
    public class SerializeUntilAttribute : Attribute
    {
        public object Value { get; }

        public SerializeUntilAttribute(object value) =>
            Value = value;
    }
}
