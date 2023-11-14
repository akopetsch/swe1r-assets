// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization;
using ByteSerialization.Attributes;
using ByteSerialization.Attributes.Helpers;
using ByteSerialization.Components.Attributes.Reference;
using ByteSerialization.Components.Values.Composites.Records;
using SWE1R.Assets.Blocks.ModelBlock.Nodes;
using System;

namespace SWE1R.Assets.Blocks.ModelBlock
{
    public class FlaggedNodeOrInteger
    {
        #region Properties

        [TypeHelper(typeof(TypeHelper))]
        [ReferenceHelper(typeof(ReferenceHelper))]
        [Order(0)] public object Value { get; private set; }

        public FlaggedNode FlaggedNode
        {
            get => Value as FlaggedNode;
            set => Value = value;
        }

        public int? Integer
        {
            get => Value as int?;
            set => Value = value;
        }

        #endregion

        #region Classes

        private class TypeHelper : ITypeHelper
        {
            public Type GetPropertyType(RecordComponent recordNode)
            {
                int word = recordNode.Reader.ReadInt32();
                recordNode.Context.Position -= sizeof(int);

                if (word < 0)
                    // only 156: Scen_Anakin_Racer
                    // TODO: move comment
                    return typeof(int);
                else
                    return typeof(FlaggedNode);
            }
        }

        private class ReferenceHelper : IReferenceHelper
        {
            public bool IsReference(PropertyComponent propertyComponent)
            {
                if (propertyComponent.Mode == ByteSerializerMode.Serializing)
                    return propertyComponent.Value == null || propertyComponent.Value is FlaggedNode;
                else
                    return propertyComponent.Type == typeof(FlaggedNode);
            }

            public ReferenceConfiguration GetReferenceConfiguration(PropertyComponent propertyComponent) =>
                new ReferenceConfiguration() { Handling = ReferenceHandling.DefaultPriority };

        }

        #endregion

        #region Methods

        public override string ToString()
        {
            if (FlaggedNode != null)
                return FlaggedNode.ToString();
            else if (Integer.HasValue)
                return $"{Integer.Value:x8}";
            else
                return "null";
        }

        #endregion
    }
}
