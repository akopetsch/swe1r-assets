// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization.Components.Values.Composites.Collections;

namespace ByteSerialization.Attributes.Limiting.SerializeUntil
{
    public class SerializeUntilComponent : AttributeComponent<SerializeUntilAttribute>, ILimitingComponent
    {
        #region Properties

        public CollectionComponent Collection { get; private set; }

        #endregion

        #region Methods

        protected override void OnInitialized()
        {
            base.OnInitialized();

            Collection = GetAncestor<CollectionComponent>();

            Node.AfterSerializing += AfterSerializing;
            Node.AfterDeserializing += AfterDeserializing;
        }

        public bool IsDeserializedUntilEnd(CollectionComponent collection)
        {
            var delimiter = Attribute.Value;
            if (delimiter == null)
                return Reader.Peek<int>() == 0; // null pointer
            else
                return Reader.Peek(delimiter.GetType()).Equals(delimiter);
        }

        private void AfterSerializing() =>
            Writer.Write(Attribute.Value);

        private void AfterDeserializing() =>
            Reader.Read(Attribute.Value.GetType());

        #endregion
    }
}
