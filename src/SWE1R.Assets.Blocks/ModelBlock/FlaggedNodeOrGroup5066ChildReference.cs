// Copyright 2024 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization;
using ByteSerialization.Attributes;
using ByteSerialization.Attributes.Helpers;
using ByteSerialization.Components.Attributes.Reference;
using ByteSerialization.Components.Values.Composites.Records;
using SWE1R.Assets.Blocks.ModelBlock.Nodes;
using SWE1R.Assets.Blocks.ModelBlock.Types;
using System;

namespace SWE1R.Assets.Blocks.ModelBlock
{
    public class FlaggedNodeOrGroup5066ChildReference
    {
        #region Properties

        [TypeHelper(typeof(TypeHelper))]
        [ReferenceHelper(typeof(ReferenceHelper))]
        [Order(0)]
        public object Value { get; private set; }

        #endregion

        #region Properties (C union style access)

        public FlaggedNode FlaggedNode
        {
            get => Value as FlaggedNode;
            set => Value = value;
        }

        public Group5066ChildReference Group5066ChildReference
        {
            get => Value as Group5066ChildReference;
            set => Value = value;
        }

        #endregion

        #region Classes (helper)

        private class TypeHelper : ITypeHelper
        {
            public Type GetPropertyType(RecordComponent recordNode) => 
                recordNode.Root.Value is PoddModel ?
                    typeof(Group5066ChildReference) : typeof(FlaggedNode);
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
                new ReferenceConfiguration() { Handling = ReferenceHandling.HighPriority };

        }

        #endregion

        #region Methods (: object)

        public override string ToString()
        {
            if (FlaggedNode != null)
                return FlaggedNode.ToString();
            else
                return Group5066ChildReference.ToString();
        }

        #endregion
    }
}
