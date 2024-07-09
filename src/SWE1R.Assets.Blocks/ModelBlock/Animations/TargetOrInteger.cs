// SPDX-License-Identifier: MIT

using ByteSerialization.Attributes;
using ByteSerialization.Attributes.Helpers;
using ByteSerialization.Components.Values;
using ByteSerialization.Components.Values.Composites.Records;
using System;

namespace SWE1R.Assets.Blocks.ModelBlock.Animations
{
    public class TargetOrInteger
    {
        #region Properties (serialized)

        [TypeHelper(typeof(TypeHelper))]
        [Order(0)]
        public object Value { get; private set; }

        #endregion

        #region Properties (C union style access)

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

        #endregion

        #region Classes (serialization)

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

        #endregion
    }
}
