// Copyright 2024 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using System;
using System.Collections.Generic;

namespace ByteSerialization.Utilities
{
    public class NumberConverter
    {
        private static readonly Dictionary<Type, Func<object, object>> converterByOutputType =
            new Dictionary<Type, Func<object, object>>() {
                { typeof(int), n => System.Convert.ToInt32(n) },
                { typeof(short), n => System.Convert.ToInt16(n) },
                // TODO: support more number types
            };

        private Func<object, object> converter;

        public Type OutputType { get; }

        public NumberConverter(Type outputType)
        {
            OutputType = outputType;
            converter = converterByOutputType[OutputType];
        }

        public object Convert(object number) =>
            converter(number);
    }
}
