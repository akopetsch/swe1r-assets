// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization.Attributes;
using System.Reflection;

namespace FiddleApp
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
                List<ByteSerialization.Attributes.Attribute> byteSerializerAttributes = PropertyInfo.GetAttributes();

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
