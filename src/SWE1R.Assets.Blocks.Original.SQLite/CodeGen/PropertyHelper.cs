// SPDX-License-Identifier: GPL-2.0-only

using ByteSerialization.Attributes;
using System.Reflection;

namespace SWE1R.Assets.Blocks.Original.SQLite.CodeGen
{
    public class PropertyHelper
    {
        #region Properties (input)

        public PropertyInfo PropertyInfo { get; }

        #endregion

        #region Properties (output)

        public bool IsReference { get; }
        public Type Type { get; }
        public string TypeName { get; }
        public string PropertyName { get; }

        #endregion

        #region Constructor

        public PropertyHelper(PropertyInfo propertyInfo)
        {
            PropertyInfo = propertyInfo;

            Type = PropertyInfo.PropertyType;
            TypeName = Type.Name;
            PropertyName = PropertyInfo.Name;

            if (Type.IsEnum)
            {

            }

            if (Type.IsPrimitive)
                TypeName = TypeKeywordMapper.GetKeywordFromType(Type);
            else
            {
                List<ByteSerializationAttribute> byteSerializerAttributes = PropertyInfo.GetAttributes();

                // reference?
                IsReference = byteSerializerAttributes.OfType<ReferenceAttribute>().SingleOrDefault() != null;
                if (IsReference)
                {
                    TypeName = TypeKeywordMapper.GetKeywordFromType(typeof(int));
                    PropertyName = $"P_{PropertyName}";
                }
                else
                {
                    
                }
            }

            // see ValueComponentFactory
        }

        #endregion
    }
}
