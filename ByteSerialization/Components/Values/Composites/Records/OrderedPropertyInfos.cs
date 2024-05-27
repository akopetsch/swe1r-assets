// Copyright 2024 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization.Attributes;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ByteSerialization.Components.Values.Composites.Records
{
    public class OrderedPropertyInfos : List<PropertyInfo> // FIXME: should be read-only
    {
        public Type Type { get; }

        public OrderedPropertyInfos(Type type)
        {
            Type = type;

            List<IGrouping<int?, PropertyInfo>> byOrder = 
                type.GetProperties(
                    BindingFlags.Instance |
                    BindingFlags.Public |
                    BindingFlags.NonPublic |
                    BindingFlags.DeclaredOnly).
                GroupBy(x => x.GetCustomAttribute<OrderAttribute>()?.Order).
                Where(g => g.Key.HasValue).
                ToList();

            if (byOrder.Any(g => g.Count() != 1))
                throw new InvalidOperationException(
                    $"Type {type} contains properties with identical ordering information.");

            List<PropertyInfo> propertyInfos = byOrder.
                OrderBy(g => g.Key).Select(g => g.Single()).ToList();

            AddRange(propertyInfos);
        }
    }

    public static class OrderedPropertyInfosExtensions
    {
        private static readonly ConcurrentDictionary<Type, OrderedPropertyInfos> dictionary =
            new ConcurrentDictionary<Type, OrderedPropertyInfos>();

        public static OrderedPropertyInfos GetOrderedPropertyInfos(this Type type) =>
            dictionary.GetOrAdd(type, x => new OrderedPropertyInfos(x));
    }
}
