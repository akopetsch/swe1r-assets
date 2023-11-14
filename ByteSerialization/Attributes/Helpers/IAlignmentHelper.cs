// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization.Components.Values.Composites.Records;
using System;
using System.Collections.Concurrent;

namespace ByteSerialization.Attributes.Helpers
{
    public interface IAlignmentHelper
    {
        int GetAlignment(RecordComponent record);
    }

    public static class IAlignmentHelperExtensions
    {
        private static readonly ConcurrentDictionary<Type, IAlignmentHelper> dictionary = 
            new ConcurrentDictionary<Type, IAlignmentHelper>();

        public static IAlignmentHelper GetAlignmentHelper(this Type type) => 
            dictionary.GetOrAdd(type, x => (IAlignmentHelper)Activator.CreateInstance(x));
    }
}
