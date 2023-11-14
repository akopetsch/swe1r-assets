// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using SystemAttribute = System.Attribute;

namespace ByteSerialization.Attributes
{
    public abstract class Attribute : SystemAttribute
    {
        
    }

    public static class AttributeExtensions
    {
        private static readonly ConcurrentDictionary<MemberInfo, List<Attribute>> dictionary =
            new ConcurrentDictionary<MemberInfo, List<Attribute>>();

        public static List<Attribute> GetAttributes(this MemberInfo memberInfo) =>
            dictionary.GetOrAdd(memberInfo, x => x.GetCustomAttributes<Attribute>(inherit: false).ToList());
    }
}
