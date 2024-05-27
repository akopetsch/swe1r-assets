// Copyright 2024 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using System;
using System.Diagnostics;

namespace ByteSerialization.Attributes
{
    // TODO: consider different folder

    [DebuggerDisplay("{" + nameof(DebuggerDisplay) + ",nq}")]
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public abstract class AbstractTypeIdentifierAttribute : ByteSerializationAttribute
    {
        private string DebuggerDisplay =>
            $"{Identifier} -> {Type.Name}";

        public object Identifier { get; }
        public Type Type { get; }

        protected AbstractTypeIdentifierAttribute(object identifier, Type type)
        {
            Identifier = identifier;
            Type = type;
        }
    }
}
