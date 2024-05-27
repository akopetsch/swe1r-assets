// Copyright 2024 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization.Extensions;
using System;

namespace ByteSerialization.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class AttributeComponentAttribute : ByteSerializationAttribute
    {
        public Type ComponentType { get; set; }

        public AttributeComponentAttribute(Type componentType)
        {
            if (!(ComponentType = componentType).Is<AttributeComponent>())
                throw new InvalidOperationException();
        }
    }
}
