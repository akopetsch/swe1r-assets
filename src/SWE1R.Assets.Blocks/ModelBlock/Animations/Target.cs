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

        [Reference(ReferenceHandling.HighPriority)]
        [TypeHelper(typeof(TypeHelper))]
        [Order(0)]
        internal object Value { get; set; }

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

                if (anim.AnimationType == AnimationType._2)
                    return typeof(MeshMaterialReference);

                if (anim.AnimationType == AnimationType._B ||
                    anim.AnimationType == AnimationType._C)
                    return typeof(MeshMaterial);

                if (anim.AnimationType == AnimationType._8 ||
                    anim.AnimationType == AnimationType._9 ||
                    anim.AnimationType == AnimationType._A)
                    return typeof(TransformedWithPivotNode);

                throw new InvalidOperationException($"Unknown '{nameof(Animation.AnimationType)}'.");
            }
        }

        #endregion
    }
}
