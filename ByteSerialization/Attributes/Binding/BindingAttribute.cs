// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using System;

namespace ByteSerialization.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public abstract class BindingAttribute : Attribute
    {
        #region Properties

        public int Value { get; }
        public string PropertyName { get; }
        public IBindingHelper Helper { get; }

        public BindingMode Mode { get; }

        #endregion

        #region Constructor

        protected BindingAttribute(int value)
        {
            Value = value;
            Mode = BindingMode.Value;
        }

        protected BindingAttribute(string propertyName)
        {
            PropertyName = propertyName;
            Mode = BindingMode.PropertyName;
        }

        protected BindingAttribute(Type helperType)
        {
            Helper = helperType.GetBindingHelper();
            Mode = BindingMode.HelperType;
        }

        #endregion
    }
}
