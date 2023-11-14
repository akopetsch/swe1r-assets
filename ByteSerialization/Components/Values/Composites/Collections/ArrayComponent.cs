// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using System;

namespace ByteSerialization.Components.Values.Composites.Collections
{
    public class ArrayComponent : CollectionComponent
    {
        #region Members (debug)

        public override string GetDebuggerDisplay() => TypeName;
        public override string TypeName => $"{ElementType?.Name}[]";
        
        #endregion

        #region Properties

        public override Type ElementType => 
            Node.Type.GetElementType();

        #endregion

        #region Methods

        protected override object CreateValue() =>
            Array.CreateInstance(ElementType, Children.Count);
        
        public override void SetElementValue(int index, object value) =>
            (Node.Value as Array).SetValue(value, index);

        #endregion
    }
}
