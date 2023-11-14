// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization.Components.Values.Composites.Records;
using ByteSerialization.Utilities;
using System;
using System.Linq;

namespace ByteSerialization.Attributes
{
    public class BindingComponent<TAttribute> : AttributeComponent<TAttribute> 
        where TAttribute : BindingAttribute
    {
        #region Properties

        private int? BindingValue { get; set; }

        #endregion

        #region Methods

        public int GetBindingValue()
        {
            if (BindingValue == null)
            {
                switch (Attribute.Mode)
                {
                    case BindingMode.Value:
                        BindingValue = Attribute.Value; break;
                    case BindingMode.PropertyName:
                        BindingValue = GetBindingValueByPropertyName(); break;
                    case BindingMode.HelperType:
                        BindingValue = Attribute.Helper.GetValue(Get<PropertyComponent>()); break;
                }
            }
            return BindingValue.Value;
        }

        private int GetBindingValueByPropertyName()
        {
            PropertyComponent boundProperty = GetBoundProperty();
            int i;
            Type t = boundProperty.Type;
            if (t == typeof(int))
                i = (int)boundProperty.Value;
            else if (t == typeof(short))
                i = (short)boundProperty.Value;
            else
                throw new NotImplementedException();
            return i;
        }

        public void SetBindingValue(int value)
        {
            switch (Attribute.Mode)
            {
                case BindingMode.Value:
                    break;
                case BindingMode.PropertyName:
                    SetBindingValueByPropertyName(value); break;
                case BindingMode.HelperType:
                    /*Attribute.Helper.SetValue(Get<PropertyComponent>(), value);*/ break;
            }
        }

        private void SetBindingValueByPropertyName(int value)
        {
            PropertyComponent boundProperty = GetBoundProperty();
            object convertedValue = new NumberConverter(boundProperty.Type).Convert(value);
            boundProperty.Node.Value = convertedValue;
        }

        private PropertyComponent GetBoundProperty() =>
            GetSiblings<PropertyComponent>().First(p => p.Name.Equals(Attribute.PropertyName));

        #endregion
    }
}
