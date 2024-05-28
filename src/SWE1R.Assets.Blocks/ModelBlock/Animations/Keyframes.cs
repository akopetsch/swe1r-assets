// Copyright 2024 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

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

        [Reference]
        [TypeHelper(typeof(TypeHelper))]
        [Length(typeof(LengthHelper))]
        [ElementReferenceHelper(typeof(ElementReferenceHelper))]
        [Order(0)] internal object Value { get; set; }

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
                    case 0b0001:
                    case 0b1011:
                    case 0b1100:
                        multiplier = 1;
                        break;
                    case 0b0100:
                        multiplier = 2;
                        break;
                    case 0b0110:
                    case 0b1000:
                        multiplier = 4;
                        break;
                    case 0b0111:
                    case 0b1001:
                    case 0b1010:
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
