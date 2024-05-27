// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization.Attributes;
using ByteSerialization.Extensions;
using ByteSerialization.Nodes;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using Mode = ByteSerialization.ByteSerializerMode;
using Reader = ByteSerialization.IO.EndianBinaryReader;
using Writer = ByteSerialization.IO.EndianBinaryWriter;

namespace ByteSerialization
{
    public delegate void ComponentEventHandler<in T>(T component) where T : Component;

    [DebuggerDisplay("{" + nameof(DebuggerDisplay) + ",nq}")]
    public abstract class Component
    {
        #region Members (debug)

        private string DebuggerDisplay => GetDebuggerDisplay();
        public virtual string GetDebuggerDisplay() => $"{{{GetType().Name}}}";

        public virtual string TypeName => Type.GetFriendlyName();

        #endregion

        #region Fields (required types)

        private static readonly ConcurrentDictionary<Type, IList<Type>> requiredTypes =
            new ConcurrentDictionary<Type, IList<Type>>();
        
        #endregion

        #region Properties

        public Node Node { get; internal set; }
        public NodeList Children => Node.Children;
        public Node Parent => Node.Parent;
        public Node Root => Node.Root;

        #endregion

        #region Properties (dynamic)

        public ByteSerializerContext Context => Node.Context;
        public Reader Reader => Context.Reader;
        public Writer Writer => Context.Writer;
        public Mode Mode => Context.Mode;
        public ByteSerializerGraph Graph => Context.Graph;

        public long? Position => Node.Position;
        
        public Type Type => Node.Type;
        public object Value => Node.Value;

        #endregion

        #region Constructor

        protected Component() { }

        #endregion

        #region Methods (events)

        public virtual void OnAdded() { }

        #endregion

        #region Methods (component-component)

        public Component Add(Type t) => 
            Node.Add(t);
        public T Add<T>() where T : Component => 
            Node.Add<T>();

        public Component AddChild(Type t) => 
            Node.AddChild().Add(t);
        public T AddChild<T>() where T : Component =>
            Node.AddChild().Add<T>();

        public Component Get(Type t) => 
            Node.Components.OfType(t).FirstOrDefault();
        public T Get<T>() where T : Component => 
            Node.Components.OfType<T>().FirstOrDefault();

        public bool Has(Type t) => 
            Node.Components.Any(c => c.GetType().Is(t));
        public bool Has<T>() where T : Component =>
            Node.Components.Any(c => c is T);

        public Component GetChild(Type t) =>
            GetChildren(t).FirstOrDefault();
        public T GetChild<T>() where T : Component =>
            GetChildren<T>().FirstOrDefault();

        public IEnumerable<Component> GetChildren(Type t) =>
            Node.Children.SelectMany(n => n.Components.OfType(t));
        public IEnumerable<T> GetChildren<T>() where T : Component =>
            Node.Children.SelectMany(n => n.Components.OfType<T>());

        public Component GetAncestor(Type t) => 
            GetAncestors(t).FirstOrDefault();
        public T GetAncestor<T>() where T : Component =>
            GetAncestors<T>().FirstOrDefault();

        public Component GetAncestor(Type t, Func<Component, bool> predicate) => 
            GetAncestors(t).FirstOrDefault(predicate);
        public T GetAncestor<T>(Func<T, bool> predicate) where T : Component =>
            GetAncestors<T>().FirstOrDefault(predicate);

        public IEnumerable<Component> GetAncestors(Type t) => 
            Node.Ancestors.SelectMany(n => n.Components.OfType(t));
        public IEnumerable<T> GetAncestors<T>() where T : Component =>
            Node.Ancestors.SelectMany(n => n.Components.OfType<T>());

        public IEnumerable<Component> GetAncestors(Type t, Func<Component, bool> predicate) =>
            GetAncestors(t).Where(predicate);
        public IEnumerable<T> GetAncestors<T>(Func<T, bool> predicate) where T : Component =>
            GetAncestors<T>().Where(predicate);

        public Component GetSibling(Type t) =>
            GetSiblings(t).FirstOrDefault();
        public T GetSibling<T>() where T : Component =>
            GetSiblings<T>().FirstOrDefault();

        public IEnumerable<Component> GetSiblings(Type t) =>
            Node.Siblings.SelectMany(n => n.Components.OfType(t));
        public IEnumerable<T> GetSiblings<T>() where T : Component =>
            Node.Siblings.SelectMany(n => n.Components.OfType<T>());

        #endregion

        #region Methods (required types)

        public static IList<Type> GetRequiredTypes(Type componentType) =>
            requiredTypes.GetOrAdd(componentType, x => x.GetCustomAttributes()
                .OfType<RequireAttribute>().Select(a => a.ComponentType).ToList().AsReadOnly());

        #endregion

        #region Methods (attributes)

        protected IEnumerable<AttributeComponent> AddAttributeComponents(List<ByteSerializationAttribute> attributes)
        {
            foreach (IGrouping<Type, ByteSerializationAttribute> grouping in attributes.GroupBy(x => x.GetType()))
            {
                Type t = AttributeComponentFactory.Instance.GetComponentType(grouping.Key);

                var c = (AttributeComponent)Add(t);
                c.Init(this, grouping);

                yield return c;
            }
        }

        #endregion
    }
}
