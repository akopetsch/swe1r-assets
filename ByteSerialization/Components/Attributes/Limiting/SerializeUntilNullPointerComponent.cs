// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization.Attributes;
using ByteSerialization.Attributes.Reference;
using ByteSerialization.Components.Values.Composites.Collections;

namespace ByteSerialization.Components.Attributes.Limiting
{
    public class SerializeUntilNullPointerComponent :
        AbstractSerializeUntilComponent<SerializeUntilNullPointerAttribute>
    {
        #region Methods

        protected override void AfterDeserializing() =>
            AddReferenceComponent().Node.Deserialize();

        protected override void AfterSerializing() =>
            AddReferenceComponent().Node.Serialize();

        private ReferenceComponent AddReferenceComponent()
        {
            var rc = AddChild<ReferenceComponent>();
            rc.Init(Get<CollectionComponent>(), new ReferenceAttribute(ReferenceHandling.Postpone));
            return rc;
        }

        public override bool IsDeserializedUntilEnd(CollectionComponent collection) =>
            Reader.Peek<int>() == 0;

        #endregion
    }
}
