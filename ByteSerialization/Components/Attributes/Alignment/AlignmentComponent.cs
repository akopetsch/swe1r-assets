// Copyright 2024 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization.Components.Values.Composites.Records;

namespace ByteSerialization.Attributes.Alignment
{
    public class AlignmentComponent : AttributeComponent<AlignmentAttribute>
    {
        protected override void OnInitialized()
        {
            base.OnInitialized();

            Node.OnSerializing += EnsureAlignment;
        }

        private void EnsureAlignment()
        {
            int alignment = Attribute.Value ?? 
                Attribute.Helper.GetAlignment((RecordComponent)Target);
            Context.EnsureAlignment(alignment);
        }
    }
}
