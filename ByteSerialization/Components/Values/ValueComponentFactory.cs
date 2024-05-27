// Copyright 2024 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization.Components.Values.Composites.Collections;
using ByteSerialization.Components.Values.Composites.Records;
using ByteSerialization.Components.Values.Customs;
using ByteSerialization.Components.Values.Primitives;
using ByteSerialization.Extensions;
using System;

namespace ByteSerialization.Components.Values
{
    public class ValueComponentFactory
    {
        #region Singleton

        public static ValueComponentFactory Instance { get; } = new ValueComponentFactory();
        private ValueComponentFactory() { }

        #endregion

        public Type GetComponentType(Type type)
        {
            if (type.IsValueType)
            {
                if (Nullable.GetUnderlyingType(type)?.IsPrimitive == true)
                    return typeof(NullablePrimitiveComponent);
                if (type.IsPrimitive)
                    return typeof(PrimitiveComponent);
                if (type.IsEnum)
                    return typeof(EnumComponent);
            }

            if (typeof(ICustomSerializable).IsAssignableFrom(type))
                return typeof(CustomComponent);

            if (type.IsArray)
                return typeof(ArrayComponent);
            if (type.IsBuiltinList())
                return typeof(ListComponent);
            if (type.IsClass)
                return typeof(RecordComponent);

            throw new InvalidOperationException();
        }
    }
}
