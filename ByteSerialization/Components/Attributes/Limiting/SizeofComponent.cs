// Copyright 2024 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization.Components.Values.Composites.Collections;
using System;

namespace ByteSerialization.Attributes.Limiting.Sizeof
{
    public class SizeofComponent : BindingComponent<SizeofAttribute>, ILimitingComponent
    {
        #region Methods

        protected override void OnInitialized()
        {
            base.OnInitialized();

            Node.OnSerialized += EnsureSize;
            Node.AfterDeserializing += EnsureSize;
            // TODO: call SetValue
        }

        private void EnsureSize()
        {
            int size = GetSize();
            Context.EnsureOffsetFrom(size, Node);
            Node.Size = size;
        }

        private int GetSize() =>
            GetBindingValue() * Attribute.Multiplier;

        public bool IsDeserializedUntilEnd(CollectionComponent collection)
        {
            long actualSize = Context.Position - Node.Position.Value;
            long targetSize = GetSize();
            long delta = targetSize - actualSize;
            if (delta > 0)
                return false;
            else if (delta == 0)
                return true;
            else
                throw new InvalidOperationException();
        }

        #endregion
    }
}
