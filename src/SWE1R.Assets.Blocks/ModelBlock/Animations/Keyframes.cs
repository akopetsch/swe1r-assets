// SPDX-License-Identifier: MIT

using ByteSerialization.Attributes;
using ByteSerialization.Attributes.Helpers;
using ByteSerialization.Components.Attributes.Reference;
using ByteSerialization.Components.Values;
using ByteSerialization.Components.Values.Composites.Collections;
using ByteSerialization.Components.Values.Composites.Records;
using SWE1R.Assets.Blocks.ModelBlock.Materials;
using SWE1R.Assets.Blocks.Utils;
using SWE1R.Assets.Blocks.Vectors;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public List<Vector3Single> KeyframeTranslations
        {
            get => Floats?.AsEnumerable().Chunk(3).Select(x => new Vector3Single(x[0], x[1], x[2])).ToList();
            set => Floats = value.SelectMany(x => new float[] { x.X, x.Y, x.Z }).ToList();
        }

        #endregion

        #region Classes (serialization)

        private class TypeHelper : ITypeHelper
        {
            public Type GetPropertyType(RecordComponent c)
            {
                Animation anim = c.GetAncestorValue<Animation>();

                if (anim.AnimationType == AnimationType.TextureFlipbook)
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
                switch (anim.AnimationType)
                {
                    case AnimationType._1:
                    case AnimationType.TextureScrollX:
                    case AnimationType.TextureScrollY:
                        multiplier = 1;
                        break;
                    case AnimationType._4:
                        multiplier = 2;
                        break;
                    case AnimationType._6:
                    case AnimationType.AxisAngle:
                        multiplier = 4;
                        break;
                    case AnimationType._7:
                    case AnimationType.Translate:
                    case AnimationType.Scale:
                        multiplier = 3;
                        break;
                    default:
                        break;
                }

                return multiplier == 0 ?
                    (int)anim.KeyframesCount :
                    (int)anim.KeyframesCount * multiplier;
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
