// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization.Extensions;
using ByteSerialization.Nodes;
using System;
using System.Linq;

namespace ByteSerialization.Components.Values
{
    public abstract class ValueComponent : Component, ISerializableComponent
    {
        #region Properties

        public override string GetDebuggerDisplay() => $"{Type.GetFriendlyName()}";

        #endregion

        #region Methods

        public abstract void Serialize();
        public abstract void Deserialize();

        #endregion
    }

    public static class ValueComponentExtensions
    {
        public static TValue GetAncestorValue<TValue>(this Component component) => 
            (TValue)component.GetAncestorValueComponent<TValue>().Node.Value;

        public static ValueComponent GetAncestorValueComponent<TValue>(this Component component) =>
            component.GetAncestors<ValueComponent>(c => typeof(TValue).IsAssignableFrom(c.Node.Type)).FirstOrDefault();

        public static ValueComponent AddValueComponent(this Node node, Type type)
        {
            Type tvc = ValueComponentFactory.Instance.GetComponentType(type);
            return (ValueComponent)node.Add(tvc);
        }
    }
}
