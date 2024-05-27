// Copyright 2024 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization.Attributes;
using ByteSerialization.Attributes.Reference;
using ByteSerialization.Nodes;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ByteSerialization.Components.Values.Composites.Records
{
    public class PropertyComponent : Component
    {
        #region Members (debug)

        public override string GetDebuggerDisplay() => $"{PropertyInfo.DeclaringType.Name}.{Name}";

        #endregion

        #region Properties

        public RecordComponent Record { get; private set; }
        public PropertyInfo PropertyInfo { get; private set; }
        public List<AttributeComponent> AttributeComponents { get; private set; }
        
        public string Name => PropertyInfo.Name;

        #endregion

        #region Methods

        public void Init(RecordComponent record, PropertyInfo info)
        {
            // init
            Record = record;
            PropertyInfo = info;

            // type, value
            if (Mode == ByteSerializerMode.Serializing)
            {
                object value = PropertyInfo.GetValue(Record.Value);
                if (value != null)
                    Node.Type = value.GetType();
                else
                    Node.Type = PropertyInfo.PropertyType;
                Node.Value = value;
            }
            else
                Node.Type = PropertyInfo.PropertyType;

            AttributeComponents = AddAttributeComponents(PropertyInfo.GetAttributes()).ToList();

            // value
            if (!Has<ReferenceComponent>() && Node.Type != typeof(object))
                Node.AddValueComponent(Node.Type);

            // update composite
            Node.ValueChanged += (v0, v1) => UpdateRecord();
        }

        public void UpdateRecord()
        {
            if (Record.Value != null)
                PropertyInfo.SetValue(Record.Value, Value);
        }

        #endregion
    }    
}
