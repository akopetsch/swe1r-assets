// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization.Components;
using ByteSerialization.Components.Attributes.Reference;
using ByteSerialization.Components.Values;
using ByteSerialization.Extensions;
using ByteSerialization.IO.Extensions;
using ByteSerialization.Nodes;
using System;

namespace ByteSerialization.Attributes.Reference
{
    public class ReferenceComponent : AttributeComponent<ReferenceAttribute>, ISerializableComponent
    {
        #region Members (debug)

        public override string GetDebuggerDisplay()
        {
            if (Pointer.HasValue)
            {
                string type = Type.GetFriendlyName();
                string pointer = HasNullPointer ? "NULL" : $"0x{Pointer.Value.ToHexString()}";
                return $"{type} @ {pointer}";
            }
            else
                return base.GetDebuggerDisplay();
        }

        #endregion

        #region Properties

        public int? Pointer;
        public bool HasNullPointer => Pointer.HasValue && Pointer.Value == 0;
        public bool IsNullReference => Mode == ByteSerializerMode.Serializing ? (Value == null) : HasNullPointer;

        public ValueComponent ValueComponent { get; private set; }

        #endregion

        #region Methods

        public void Serialize()
        {
            if (Value == null)
                Pointer = 0;
            else if (Attribute.Configuration.Handling != ReferenceHandling.ForceReuse)
            {
                ValueComponent = Graph.GetValueComponent(Value) ?? CreateValueComponent();
                if (ValueComponent.Node.IsSerialized)
                    Pointer = (int)ValueComponent.Position.Value;
                else
                {
                    ValueComponent.Node.AfterSerializing += () =>
                    {
                        Pointer = (int)ValueComponent.Position.Value;
                        WriteBackPointer();
                    };
                }
            }
            Writer.Write(Pointer ?? 0);
        }

        private void WriteBackPointer() =>
            Writer.AtPosition(Position.Value, w => w.Write(Pointer.Value));

        public void ReuseSerializedValueComponent()
        {
            ValueComponent = Graph.GetValueComponent(Value);
            Pointer = (int)ValueComponent.Position.Value;
            WriteBackPointer();
        }

        public void Deserialize()
        {
            Pointer = Reader.ReadInt32();

            if (!HasNullPointer && Attribute.Configuration.Handling != ReferenceHandling.ForceReuse)
            {
                // try to get ValueComponent by type/position
                ValueComponent = Graph.GetValueComponent(Type, Pointer.Value);

                // if it does not exist...
                if (ValueComponent == null)
                {
                    // create new
                    ValueComponent = CreateValueComponent();
                    ValueComponent.Node.Position = Pointer.Value;
                }
                else if (ValueComponent.Value != null)
                {
                    // copy value
                    Node.Value = ValueComponent.Value;
                }

                // TODO: update parent
                // copy value later
                ValueComponent.Node.ValueChanged += (vOld, vNew) => Node.Value = vNew;
            }
        }

        public void ReuseDeserializedValueComponent()
        {
            if (!HasNullPointer)
            {
                // get ValueComponent by type/position
                ValueComponent = Graph.GetValueComponent(Type, Pointer.Value);

                // copy value
                Node.Value = ValueComponent.Value;
            }
        }

        private ValueComponent CreateValueComponent()
        {
            Node valueNode = Node.AddChild();
            valueNode.Type = Type;
            valueNode.Value = Value;
            valueNode.Add<ReferencesCollectorComponent>();
            return valueNode.AddValueComponent(Type);
        }

        #endregion
    }
}
