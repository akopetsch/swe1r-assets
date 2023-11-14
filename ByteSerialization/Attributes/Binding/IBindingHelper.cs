// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization.Components.Values.Composites.Records;
using System;
using System.Collections.Concurrent;

namespace ByteSerialization.Attributes
{
    public interface IBindingHelper
    {
        int GetValue(PropertyComponent property);
    }

    public static class BindingHelperExtensions
    {
        private static readonly ConcurrentDictionary<Type, IBindingHelper> dictionary =
            new ConcurrentDictionary<Type, IBindingHelper>();

        public static IBindingHelper GetBindingHelper(this Type helperType) => 
            dictionary.GetOrAdd(helperType, x => (IBindingHelper)Activator.CreateInstance(x));
    }
}
