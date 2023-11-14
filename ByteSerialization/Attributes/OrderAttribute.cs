// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using System;
using SystemAttribute = System.Attribute;

namespace ByteSerialization.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class OrderAttribute : SystemAttribute
    {
        public int Order { get; }

        public OrderAttribute(int order) => 
            Order = order;
    }
}
