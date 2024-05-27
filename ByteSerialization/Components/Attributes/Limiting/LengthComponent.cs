// Copyright 2024 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization.Components.Values.Composites.Collections;
using System.Collections;

namespace ByteSerialization.Attributes.Limiting.Length
{
    public class LengthComponent : BindingComponent<LengthAttribute>, ILimitingComponent
    {
        protected override void OnInitialized()
        {
            base.OnInitialized();

            // TODO: call SetValue
            //Node.BeforeSerializing += SetBindingValue;
        }

        private void SetBindingValue()
        {
            if (Value is ICollection collection)
            {
                int length = collection.Count;
                SetBindingValue(length);
            }
        }

        public bool IsDeserializedUntilEnd(CollectionComponent collection)
        {
            int actual = collection.Elements.Count;
            int target = GetBindingValue();
            return actual >= target;
        }
    }
}
