// Copyright 2024 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using System.Collections.Generic;
using System.Linq;

namespace ByteSerialization.Attributes
{
    public abstract class AttributeComponent : Component
    {
        public abstract void Init(Component target, IEnumerable<ByteSerializationAttribute> attributes);
        protected virtual void OnInitialized() { }
    }

    public class AttributeComponent<TAttribute> : AttributeComponent where TAttribute : ByteSerializationAttribute
    {
        public Component Target { get; protected set; }
        public TAttribute Attribute { get; private set; }

        public void Init(Component target, TAttribute attribute)
        {
            Target = target;
            Attribute = attribute;
            OnInitialized();
        }

        public override void Init(Component target, IEnumerable<ByteSerializationAttribute> attributes) =>
            Init(target, (TAttribute)attributes.Single());
    }

    public class AttributesComponent<TAttribute> : AttributeComponent where TAttribute : ByteSerializationAttribute
    {
        public Component Target { get; protected set; }
        public List<TAttribute> Attributes { get; private set; }

        public override void Init(Component target, IEnumerable<ByteSerializationAttribute> attributes)
        {
            Target = target;
            Attributes = attributes.Cast<TAttribute>().ToList();
            OnInitialized();
        }
    }
}
