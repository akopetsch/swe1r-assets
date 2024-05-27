// Copyright 2024 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ByteSerialization.Attributes
{
    // TODO: consider different folder

    public abstract class AbstractTypeIdentifierComponent<TAttribute> : AttributesComponent<TAttribute> 
        where TAttribute : AbstractTypeIdentifierAttribute
    {
        #region Properties

        protected Dictionary<object, Type> TypesByIdentifier { get; private set; }
        protected Dictionary<Type, object> IdentifiersByType { get; private set; }
        protected Type IdentifierType { get; private set; }

        #endregion

        #region Methods

        protected override void OnInitialized()
        {
            base.OnInitialized();

            TypesByIdentifier = Attributes.ToDictionary(a => a.Identifier, a => a.Type);
            IdentifiersByType = Attributes.ToDictionary(a => a.Type, a => a.Identifier);

            // get IdentifierType
            Type[] types = Attributes.Select(a => a.Identifier.GetType()).Distinct().ToArray();
            if (types.Length > 1)
                throw new InvalidOperationException("Type identifiers must be of the same type.");
            else
                IdentifierType = types.Single();
        }

        public void MarkType(Node node)
        {
            if (IdentifiersByType.TryGetValue(node.Type, out object identifier))
            {
                if (IdentifierType.IsEnum)
                {
                    Type underlyingType = Enum.GetUnderlyingType(IdentifierType);
                    object underlyingValue = Convert.ChangeType(identifier, underlyingType);
                    Writer.Write(underlyingValue);
                }
                else if (IdentifierType.IsPrimitive)
                {
                    Writer.Write(identifier);
                }
                else if (IdentifierType == typeof(string))
                {
                    Writer.Write(Encoding.UTF8.GetBytes((string)identifier));
                }
            }
        }

        public Type IdentifyType()
        {
            long startPosition = Context.Position;

            // get readValue
            object readValue = null;
            if (IdentifierType.IsEnum)
            {
                Type underlyingType = Enum.GetUnderlyingType(IdentifierType);
                readValue = Reader.Read(underlyingType);
            }
            else if (IdentifierType.IsPrimitive)
            {
                readValue = Reader.Read(IdentifierType);
            }
            else if (IdentifierType == typeof(string))
            {
                int[] lengths = Attributes.Select(a => ((string)a.Identifier).Length).Distinct().ToArray();
                if (lengths.Length > 1)
                    throw new InvalidOperationException("String type identifiers must be of the same length.");
                else
                {
                    int length = lengths.Single();
                    byte[] readBytes = Reader.ReadBytes(length);
                    readValue = Encoding.UTF8.GetString(readBytes);
                }
            }
            
            if (TypesByIdentifier.TryGetValue(readValue, out Type type))
                return type;
            else
                Context.Position = startPosition;
            return null;
        }

        #endregion
    }
}
