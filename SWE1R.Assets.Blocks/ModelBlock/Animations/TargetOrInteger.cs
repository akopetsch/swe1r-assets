// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization.Attributes;
using ByteSerialization.Attributes.Helpers;
using ByteSerialization.Components.Values;
using ByteSerialization.Components.Values.Composites.Records;
using System;

namespace SWE1R.Assets.Blocks.ModelBlock.Animations
{
    public class TargetOrInteger
    {
        [TypeHelper(typeof(TypeHelper))]
        [Order(0)] public object Value { get; private set; }

        public Target Target
        {
            get => Value as Target;
            set => Value = value;
        }

        public int? Integer
        {
            get => Value as int?;
            set => Value = value;
        }
        
        private class TypeHelper : ITypeHelper
        {
            public Type GetPropertyType(RecordComponent c)
            {
                Animation anim = c.GetAncestorValue<Animation>();

                if (anim.BitmaskNibble == Animation.SpecialBitmaskNibble)
                    return typeof(int?);
                else
                    return typeof(Target);
            }
        }
    }
}
