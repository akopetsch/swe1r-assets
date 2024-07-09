// SPDX-License-Identifier: MIT

using ByteSerialization.Attributes;
using ByteSerialization.Attributes.Helpers;
using ByteSerialization.Components.Attributes.Reference;
using ByteSerialization.Components.Values;
using ByteSerialization.Components.Values.Composites.Collections;
using ByteSerialization.Components.Values.Composites.Records;
using SWE1R.Assets.Blocks.ModelBlock.Materials;
using System;
using System.Collections.Generic;

namespace SWE1R.Assets.Blocks.ModelBlock.Animations
{
    public class Keyframes
    {
        #region Properties (serialized)

        [ElementReferenceHelper(typeof(ElementReferenceHelper))]
        [Length(typeof(LengthHelper))]
        [Reference]
        [TypeHelper(typeof(TypeHelper))]
        [Order(0)]
        internal object Value { get; set; }

        #endregion

        #region Properties (C union style access)

        public List<MaterialTexture> MaterialTextures
        {
            get => Value as List<MaterialTexture>;
            set => Value = value;
        }
        public List<float> Floats
        {
            get => Value as List<float>;
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
                    return typeof(List<MaterialTexture>);
                else
                    return typeof(List<float>);
            }
        }

        private class LengthHelper : IBindingHelper
        {
            public int GetValue(PropertyComponent c)
            {
                Animation anim = c.GetAncestorValue<Animation>();

                int multiplier = 0;
                switch (anim.BitmaskNibble)
                {
                    case 0b0001: // 0x1
                    case 0b1011: // 0xB
                    case 0b1100: // 0xC
                        multiplier = 1;
                        break;
                    case 0b0100: // 0x4
                        multiplier = 2;
                        break;
                    case 0b0110: // 0x6
                    case 0b1000: // 0x8
                        multiplier = 4;
                        break;
                    case 0b0111: // 0x7
                    case 0b1001: // 0x9
                    case 0b1010: // 0xA
                        multiplier = 3;
                        break;
                    default:
                        break;
                }

                return multiplier == 0 ?
                    anim.FramesCount :
                    anim.FramesCount * multiplier;
            }
        }

        private class ElementReferenceHelper : IElementReferenceHelper
        {
            public ReferenceConfiguration GetReferenceConfiguration(CollectionComponent c, Type elementType)
            {
                if (elementType == typeof(MaterialTexture))
                    return new ReferenceConfiguration();
                else
                    return null;
            }
        }

        #endregion
    }
}
