// SPDX-License-Identifier: MIT

using ByteSerialization.Attributes;
using ByteSerialization.Attributes.Helpers;
using ByteSerialization.Components.Values;
using ByteSerialization.Components.Values.Composites.Records;
using SWE1R.Assets.Blocks.ModelBlock.Nodes;
using SWE1R.Assets.Blocks.ModelBlock.Types;
using System;

namespace SWE1R.Assets.Blocks.ModelBlock.Meshes
{
    public class MeshGroupOrShorts
    {
        // TODO: reorder properties
        [Length(typeof(LengthHelper))]
        [TypeHelper(typeof(TypeHelper))]
        [Reference(ReferenceHandling.HighPriority)]
        [Order(0)] public object Value { get; private set; }

        public MeshGroup3064 MeshGroup
        {
            get => Value as MeshGroup3064;
            set => Value = value;
        }

        public short[] Shorts
        {
            get => Value as short[];
            set => Value = value;
        }

        private class LengthHelper : IBindingHelper
        {
            public int GetValue(PropertyComponent p)
            {
                var m = p.GetAncestorValue<Mesh>();
                switch (m.PrimitiveType)
                {
                    case PrimitiveType.Triangles:
                        return 3 * m.FacesCount;
                    case PrimitiveType.Quads:
                        return 4 * m.FacesCount;
                    default:
                        return 0;
                }
            }
        }

        private class TypeHelper : ITypeHelper
        {
            public Type GetPropertyType(RecordComponent record)
            {
                // UnkCount
                // TODO: use property value Mesh.UnkCount instead of reading manually
                ValueComponent meshValueComponent = record.GetAncestorValueComponent<Mesh>();
                long unkCountPosition = meshValueComponent.Position.Value + 0x3e;
                short unkCount = meshValueComponent.Reader.AtPosition(unkCountPosition, r => r.ReadInt16());

                // model
                var model = (Model)record.Root.Value;

                if (unkCount != 0 && (model is ScenModel || model is PuppModel))
                    return typeof(MeshGroup3064);
                else
                    return typeof(short[]);
            }
        }
    }
}
