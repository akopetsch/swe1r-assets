// Copyright 2024 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization.Attributes.Reference;
using ByteSerialization.Components;
using ByteSerialization.Components.Attributes.Conditional;
using ByteSerialization.Components.Values.Composites.Collections;
using ByteSerialization.Components.Values.Composites.Records;
using ByteSerialization.Extensions;
using ByteSerialization.IO;
using ByteSerialization.Pooling;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Context = ByteSerialization.ByteSerializerContext;

namespace ByteSerialization.Nodes
{
    public delegate void NodeEventHandler();
    public delegate void TypeChangedEventHandler(Type before, Type after);
    public delegate void ValueChangedEventHandler(object before, object after);
    public delegate void PositionChangedEventHandler(Node node, long? before, long? after);
    public delegate void SizeChangedEventHandler(Node node, long? before, long? after);

    [DebuggerDisplay("{" + nameof(DebuggerDisplay) + ",nq}")]
    public sealed class Node : IPoolable, INotifyPropertyChanged
    {
        #region Members (debug)

        private string DebuggerDisplay => GetDebugString();

        #endregion

        #region Members (: IPoolable)

        public event ReleaseEventHandler OnRelease;

        void IPoolable.Release()
        {
            ReleaseEvents();
            ReleaseProperties();

            OnRelease();
        }

        private void ReleaseProperties()
        {
            Context = null;
            Parent = null;
            Root = null;
            Children = null;
            Components = null;

            Position = null;
            Size = null;

            Value = null;
            Type = null;
        }

        private void ReleaseEvents()
        {
            BeforeSerializing = null;
            OnSerializing = null;
            AfterSerializing = null;

            BeforeDeserializing = null;
            OnDeserializing = null;
            AfterDeserializing = null;
        }

        #endregion

        #region Members (: INotifyPropertyChanged)

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName, object before, object after)
        {
            switch (propertyName)
            {
                case nameof(Type):
                    TypeChanged?.Invoke((Type)before, (Type)after); break;
                case nameof(Value):
                    ValueChanged?.Invoke(before, after); break;
                case nameof(Position):
                    PositionChanged?.Invoke(this, (long?)before, (long?)after); break;
                default:
                    break;
            }
        }

        #endregion

        #region Events

        public event NodeEventHandler BeforeSerializing;
        public event NodeEventHandler OnSerializing;
        public event NodeEventHandler OnSerialized;
        public event NodeEventHandler AfterSerializing;

        public event NodeEventHandler BeforeDeserializing;
        public event NodeEventHandler OnDeserializing;
        public event NodeEventHandler OnDeserialized;
        public event NodeEventHandler AfterDeserializing;

        public event TypeChangedEventHandler TypeChanged;
        public event ValueChangedEventHandler ValueChanged;
        public event PositionChangedEventHandler PositionChanged;

        #endregion

        #region Properties

        [DoNotNotify] public Context Context { get; private set; }
        [DoNotNotify] public Node Parent { get; private set; }
        [DoNotNotify] public Node Root { get; private set; }
        [DoNotNotify] public NodeList Children { get; private set; }
        [DoNotNotify] public ComponentList Components { get; private set; }

        public long? Position { get; set; }
        [DoNotNotify] public long? Size { get; set; }

        public Type Type { get; set; }
        public object Value { get; set; }

        // TODO: improve cancellation mechanism
        [DoNotNotify] public bool IsSerializationCancelled { get; set; } = false;

        [DoNotNotify] private ISerializableComponent SerializableComponent =>
            Get<ReferenceComponent>() ?? Components.OfType<ISerializableComponent>().SingleOrDefault();
        [DoNotNotify] private IConditionalComponent ConditionalComponent =>
            Components.OfType<IConditionalComponent>().SingleOrDefault();

        #endregion

        #region Properties (state)

        public bool IsSerialized { get; private set; } = false;
        public bool IsDeserialized { get; private set; } = false;

        #endregion

        #region Properties (dynamic)

        [DoNotNotify] public ByteSerializerGraph Graph =>
            Context.Graph;

        [DoNotNotify] public bool IsRoot => 
            this == Root;

        [DoNotNotify] public bool IsLeaf => 
            Children.Count == 0;

        [DoNotNotify] public IEnumerable<Node> Ancestors
        {
            get
            {
                Node n = Parent;
                do
                    yield return n;
                while ((n = n.Parent) != null);
            }
        }

        [DoNotNotify] public IEnumerable<Node> Siblings => 
            Parent?.Children.Except(this);

        [DoNotNotify] public int Depth => (Parent?.Depth + 1) ?? 0;

        #endregion

        #region Methods (creation)

        public static Node CreateRoot(EndianBinaryReader r, Type type) =>
            CreateRoot(new Context(r), type);

        public static Node CreateRoot(EndianBinaryWriter w, object value) =>
            CreateRoot(new Context(w), value.GetType(), value);

        private static Node CreateRoot(Context context, Type type, object value = null)
        {
            Node n = UniversalPool.Instance.Get<Node>();
            n.Init(context, null, n);

            n.Type = type;
            n.Value = value;

            context.Graph.Root = n.Add<RootComponent>();

            return n;
        }

        public Node AddChild()
        {
            Node n = UniversalPool.Instance.Get<Node>();
            n.Init(Context, this, Root);
            Children.Add(n);
            return n;
        }

        private void Init(Context context, Node parent, Node root)
        {
            Context = context;
            Parent = parent;
            Root = root;
            Children = UniversalPool.Instance.Get<NodeList>();
            Components = UniversalPool.Instance.Get<ComponentList>();

            Context.Graph.AddNode(this);
        }

        #endregion

        #region Methods (components)

        public Component Add(Type t)
        {
            // requirements
            foreach (var requiredType in Component.GetRequiredTypes(t))
                Add(requiredType);

            // create
            var c = (Component)Activator.CreateInstance(t, true); // TODO: Node.Add: use pooling
            c.Node = this;
            Components.Add(c);

            Context.Graph.AddComponent(c);

            // OnAdded
            c.OnAdded();

            return c;
        }

        public T Add<T>() where T : 
            Component => (T)Add(typeof(T));

        public Component Get(Type t) =>
            Components.OfType(t).FirstOrDefault();
        public T Get<T>() where T : Component => 
            Components.OfType<T>().FirstOrDefault();

        #endregion

        #region Methods (serialize / deserialize)

        public void Serialize()
        {
            // before
            BeforeSerializing?.Invoke();

            // work
            OnSerializing?.Invoke();
            Position = Context.Position;

            Context.Log.Append(GetDebugLine());
            Context.Log.Append(Value);
            Context.Log.AppendLine();

            SerializableComponent.Serialize();

            OnSerialized?.Invoke();
            Size = Context.Position - Position.Value;

            if (!IsSerializationCancelled)
            {
                // after
                IsSerialized = true; // TODO: move out of if-clause (top)?
                AfterSerializing?.Invoke();
            }
        }

        public void Deserialize()
        {
            // before
            BeforeDeserializing?.Invoke();

            if (ConditionalComponent?.IsSerialized(this) == false)
                return;

            // work
            OnDeserializing?.Invoke();
            Position = Context.Position;

            Context.Log.Append(GetDebugLine());
            Context.Log.AppendLine();
            
            SerializableComponent.Deserialize();

            OnDeserialized?.Invoke();
            Size = Context.Position - Position.Value;

            // after
            IsDeserialized = true;
            AfterDeserializing?.Invoke();
        }

        #endregion

        #region Methods (debug)

        private string GetDebugLine() =>
            $"{Position:x6} {new string(' ', Depth)}> {GetDebugString()}"; // TODO: fix 'x6'

        private string GetDebugString()
        {
            var sb = new StringBuilder();

            // element of CompositeComponent?
            var p = Get<PropertyComponent>();
            if (p != null)
                sb.Append($"{p.PropertyInfo.DeclaringType.GetFriendlyName()}.{p.Name} ");
            var e = Get<CollectionElementComponent>();
            if (e != null)
                sb.Append($"[{e.Index}] ");

            // type
            if (Type != null)
                sb.Append($"{Type.GetFriendlyName()} ");

            // reference?
            if (Get<ReferenceComponent>() != null)
                sb.Append("* ");

            // referencee?
            var parentReference = Parent?.Get<ReferenceComponent>();
            if (parentReference != null)
                sb.Append($"from {parentReference.Position:x6} "); // TODO: fix 'x6'

            return sb.ToString();
        }

        #endregion
    }
}
