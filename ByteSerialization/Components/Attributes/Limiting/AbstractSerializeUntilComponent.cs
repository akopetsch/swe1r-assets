// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization.Attributes;
using ByteSerialization.Attributes.Limiting;
using ByteSerialization.Components.Values.Composites.Collections;
using ByteSerialization.Nodes;

namespace ByteSerialization.Components.Attributes.Limiting
{
    public abstract class AbstractSerializeUntilComponent<TAttribute> : 
        AttributeComponent<TAttribute>, ILimitingComponent
        where TAttribute : ByteSerializationAttribute
    {
        #region Methods

        protected override void OnInitialized()
        {
            base.OnInitialized();
            Node.AfterSerializing += AfterSerializing;
            Node.AfterDeserializing += AfterDeserializing;
        }

        protected abstract void AfterDeserializing();

        protected abstract void AfterSerializing();

        public abstract bool IsDeserializedUntilEnd(CollectionComponent collection);

        #endregion
    }
}
