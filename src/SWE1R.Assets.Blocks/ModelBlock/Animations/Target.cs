// SPDX-License-Identifier: MIT

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

        public MeshMaterialReference MeshMaterialReference
        {
            get => Value as MeshMaterialReference;
            set => Value = value;
        }
        public MeshMaterial MeshMaterial
        {
            get => Value as MeshMaterial;
            set => Value = value;
        }
        public TransformedWithPivotNode TransformedWithPivotNode
        {
            get => Value as TransformedWithPivotNode;
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
                    return typeof(MeshMaterialReference);

                if (anim.BitmaskNibble == 0b1011 || // 0x0b
                    anim.BitmaskNibble == 0b1100)   // 0x0c
                    return typeof(MeshMaterial);

                if (anim.BitmaskNibble == 0b1000 || // 0x08
                    anim.BitmaskNibble == 0b1001 || // 0x09
                    anim.BitmaskNibble == 0b1010)   // 0x0a
                    return typeof(TransformedWithPivotNode);

                throw new InvalidOperationException($"Unknown '{nameof(Animation.BitmaskNibble)}'.");
            }
        }

        #endregion
    }
}
