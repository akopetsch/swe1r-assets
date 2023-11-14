// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using System;
using System.Collections;
using System.Linq;

namespace ByteSerialization.Components.Values.Composites.Collections
{
    public class ListComponent : CollectionComponent
    {
        #region Members (debug)

        public override string GetDebuggerDisplay() => TypeName;
        public override string TypeName => $"List<{ElementType?.Name}>";
        
        #endregion

        #region Properties

        public override Type ElementType =>
            Node.Type?.GetGenericArguments().Single();

        #endregion

        #region Methods

        protected override object CreateValue()
        {
            var list = (IList)Activator.CreateInstance(Node.Type);
            var dummyElement = ElementType.IsValueType ? Activator.CreateInstance(ElementType) : null;
            for (int i = 0; i < Children.Count; i++)
                list.Add(dummyElement);
            return list;
        }

        public override void SetElementValue(int index, object value) =>
            (Node.Value as IList)[index] = value;

        #endregion
    }
}
