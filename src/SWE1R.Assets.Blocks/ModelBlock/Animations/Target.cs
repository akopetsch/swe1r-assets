// SPDX-License-Identifier: GPL-2.0-only

using ByteSerialization.Attributes;
using ByteSerialization.Attributes.Helpers;
using ByteSerialization.Components.Values;
using ByteSerialization.Components.Values.Composites.Records;
using SWE1R.Assets.Blocks.ModelBlock.Materials;
using SWE1R.Assets.Blocks.ModelBlock.Nodes;
using System;

namespace SWE1R.Assets.Blocks.ModelBlock.Animations
{
    public class Target
    {
        #region Properties (serialized)

        [TypeHelper(typeof(TypeHelper))]
        [Reference(ReferenceHandling.HighPriority)]
        [Order(0)] internal object Value { get; set; }

        #endregion

        #region Properties (C union style access)

        public MaterialReference MaterialReference
        {
            get => Value as MaterialReference;
            set => Value = value;
        }
        public Material Material
        {
            get => Value as Material;
            set => Value = value;
        }
        public TransformableD065 TransformableD065
        {
            get => Value as TransformableD065;
            set => Value = value;
        }

        #endregion

        #region Classes (serialization)

        private class TypeHelper : ITypeHelper
        {
            public Type GetPropertyType(RecordComponent c)
            {
                Animation anim = c.GetAncestorValue<Animation>();

                if (anim.BitmaskNibble == Animation.MaterialBitmaskNibble)
                    return typeof(MaterialReference);

                if (anim.BitmaskNibble == 0b1011 || // 0x0b
                    anim.BitmaskNibble == 0b1100)   // 0x0c
                    return typeof(Material);

                if (anim.BitmaskNibble == 0b1000 || // 0x08
                    anim.BitmaskNibble == 0b1001 || // 0x09
                    anim.BitmaskNibble == 0b1010)   // 0x0a
                    return typeof(TransformableD065);

                throw new InvalidOperationException($"Unknown '{nameof(Animation.BitmaskNibble)}'.");
            }
        }

        #endregion
    }
}
