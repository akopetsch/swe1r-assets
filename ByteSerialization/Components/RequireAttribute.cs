// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization.Extensions;
using System;

namespace ByteSerialization
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class RequireAttribute : Attribute
    {
        public Type ComponentType { get; set; }

        public RequireAttribute(Type componentType)
        {
            if (!(ComponentType = componentType).Is<Component>())
                throw new InvalidOperationException();
        }
    }
}
