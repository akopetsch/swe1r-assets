// Copyright 2024 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ByteSerialization.Attributes
{
    public abstract class ByteSerializationAttribute : Attribute
    {
        
    }

    public static class AttributeExtensions
    {
        private static readonly ConcurrentDictionary<MemberInfo, List<ByteSerializationAttribute>> dictionary =
            new ConcurrentDictionary<MemberInfo, List<ByteSerializationAttribute>>();

        public static List<ByteSerializationAttribute> GetAttributes(this MemberInfo memberInfo) =>
            dictionary.GetOrAdd(memberInfo, x => x.GetCustomAttributes<ByteSerializationAttribute>(inherit: false).ToList());
    }
}
