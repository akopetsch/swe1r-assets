// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization;
using ByteSerialization.Attributes.Reference;
using ByteSerialization.Components.Values;
using ByteSerialization.Components.Values.Composites.Collections;
using ByteSerialization.Components.Values.Composites.Records;
using ByteSerialization.Extensions;
using ByteSerialization.IO.Extensions;
using SWE1R.Assets.Blocks.ModelBlock.Meshes;
using SWE1R.Assets.Blocks.ModelBlock.Meshes.VertexIndices;
using SWE1R.Assets.Blocks.ModelBlock.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using SerializerNode = ByteSerialization.Nodes.Node;

namespace SWE1R.Assets.Blocks.ModelBlock
{
    public class ModelBlockItemMaskPart : BlockItemPart
    {
        private ModelBlockItem ModelBlockItem => (ModelBlockItem)Item;

        public ModelBlockItemMaskPart() : base() { }
        private ModelBlockItemMaskPart(ModelBlockItemMaskPart source) : base(source) { }

        public void GenerateFromData(ByteSerializerContext context)
        {
            var size = (int)Math.Ceiling(GetBitNumber(ModelBlockItem.Data.Length) / 8f);
            Bytes = new byte[size.Ceiling(4)];

            Mask(context.Graph.References.Where(IsMasked));
            Mask(context.Graph.GetValueComponents<TextureId>());

            List<PropertyComponent> startVertexProperties = context.Graph.GetRecordComponents<IndicesChunk01>()
                .Select(rc => rc.Properties[nameof(IndicesChunk01.StartVertex)]).ToList();
            Mask(startVertexProperties.Where(pc => ((ReferenceByIndex<Vertex>)pc.Value).Value != null));

            // mask special altN
            var modelRecordComponent = context.Graph.GetRecordComponent<Model>();
            if (ModelBlockItem.Model is PoddModel)
            {
                // TODO: ugly hack (why ugly?)
                PropertyComponent altNPropertyComponent = modelRecordComponent.Properties[nameof(Model.AltN)];
                Mask(altNPropertyComponent.Children);
            }
            // mask null-pointers that mark the end of collections
            MaskNext(modelRecordComponent.Properties[nameof(Model.Animations)]);
            MaskNext(modelRecordComponent.Properties[nameof(Model.AltN)]);
            // TODO: instead of using 'MaskNext', a feature should be implemented in 'ByteSerializer'
        }

        private bool IsMasked(ReferenceComponent r)
        {
            if (IsSpecialScenReference(r))
                return false;

            if (r.Value == null)
            {
                if (r.Has<PropertyComponent>() && r.Parent.Get<ValueComponent>().Type.IsOneOf<Material, Mesh>())
                    return false;
                if (r.Parent.Type == typeof(MeshGroupOrShorts))
                    return false;
                if (r.Has<CollectionElementComponent>() && r.Type == typeof(MaterialTextureChild))
                    return false;
            }

            return true;
        }

        private bool IsSpecialScenReference(ReferenceComponent c)
        {
            if (ModelBlockItem.Model is ScenModel && c.Type == typeof(MappingChild))
            {
                var sub = c.GetAncestorValue<MappingSub>();
                var list = c.GetAncestorValue<List<MappingSub>>();
                if (list.IndexOf(sub) == 0)
                    return true;
            }
            return false;
        }
        
        private void Mask<TComponent>(IEnumerable<TComponent> components) where TComponent : Component
        {
            foreach (TComponent component in components)
                Mask(component.Node.Position);
        }

        private void Mask(IEnumerable<SerializerNode> nodes)
        {
            foreach (SerializerNode node in nodes)
                Mask(node.Position);
        }

        private void MaskNext(PropertyComponent p)
        {
            if (p.Children.Any())
            {
                SerializerNode last = p.Children.Last();
                Mask(last.Position + last.Size);
            }
        }

        private void Mask(long? position)
        {
            if (position.HasValue)
            {
                int i = (int)position.Value;
                int n = 7 - GetBitNumber(i) % 8;
                Bytes[GetByteNumber(i)] |= (byte)(1 << n);
            }
        }

        private int GetBitNumber(int position) => position / sizeof(int);
        private int GetByteNumber(int position) => GetBitNumber(position) / 8;

        public override BlockItemPart Clone() => new ModelBlockItemMaskPart(this);
    }
}
