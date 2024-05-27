// Copyright 2024 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using System;
using System.Collections.Generic;
using System.Linq;

namespace ByteSerialization.Extensions
{
    public static class TypeExtensions
    {
        public static bool Is<T>(this Type type) =>
            typeof(T).IsAssignableFrom(type);

        public static bool Is(this Type type, Type otherType) => 
            otherType.IsAssignableFrom(type);

        public static bool IsOneOf<T1, T2>(this Type type) => 
            type.IsOneOf(typeof(T1), typeof(T2));

        public static bool IsOneOf(this Type type, params Type[] otherTypes) => 
            otherTypes.Contains(type);

        public static bool IsBuiltinList(this Type type) =>
            type.IsGenericType && type.GetGenericTypeDefinition() == typeof(List<>);

        public static string GetFriendlyName(this Type type)
        {
            if (type.IsGenericType)
            {
                string name = type.Name.Split('`').First();
                return $"{name}<{string.Join(", ", type.GenericTypeArguments.Select(GetFriendlyName))}>";
            }
            else
            {
                // TODO: use CSharpCodeProvider
                //var compiler = new CSharpCodeProvider();
                //var type = new CodeTypeReference(typeof(ModelSerializationTest));
                //return compiler.GetTypeOutput(type);
                return type.Name;
            }
        }
    }
}
