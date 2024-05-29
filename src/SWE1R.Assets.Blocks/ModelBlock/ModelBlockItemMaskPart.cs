// SPDX-License-Identifier: GPL-2.0-only

using ByteSerialization;
using ByteSerialization.Attributes.Reference;
using ByteSerialization.Components.Values;
using ByteSerialization.Components.Values.Composites.Collections;
using ByteSerialization.Components.Values.Composites.Records;
using ByteSerialization.Extensions;
using ByteSerialization.IO;
using ByteSerialization.IO.Extensions;
using ByteSerialization.IO.Utils;
using ByteSerialization.Nodes;
using SWE1R.Assets.Blocks.ModelBlock.Materials;
using SWE1R.Assets.Blocks.ModelBlock.Meshes;
using SWE1R.Assets.Blocks.ModelBlock.Meshes.VertexIndices;
using SWE1R.Assets.Blocks.ModelBlock.Types;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SWE1R.Assets.Blocks.ModelBlock
{
    public class ModelBlockItemMaskPart : BlockItemPart
    {
        #region Fields

        private const int BytesPerInt32 = sizeof(int);

        #endregion

        #region Properties

        private ModelBlockItem ModelBlockItem =>
            (ModelBlockItem)Item;

        #endregion

        #region Constructor

        public ModelBlockItemMaskPart() :
            base()
        { }

        private ModelBlockItemMaskPart(ModelBlockItemMaskPart source) :
            base(source)
        { }

        #endregion

        #region Methods (: BlockItemPart)

        public override BlockItemPart Clone() =>
            new ModelBlockItemMaskPart(this);

        #endregion

        #region Methods

        public void GenerateFromData(ByteSerializerContext context)
        {
            Model model = ModelBlockItem.Model;

            // create byte array
            Bytes = new byte[CalculatePaddedBytesLength()];

            // references
            SetMaskBits(
                context.Graph.References.Where(IsMaskBitRequired));

            // TextureIndex
            SetMaskBits(
                context.Graph.GetValueComponents<TextureIndex>());

            // IndicesChunk01.StartVertex
            SetMaskBits(
                GetStartVertexComponents(context.Graph).Where(IsMaskBitRequired));

            // AltN
            if (model is PoddModel)
                SetMaskBits(
                    context.Graph.GetCollectionComponent(model.AltN).Elements);
        }

        private int CalculatePaddedBytesLength()
        {
            int dataBytesLength = ModelBlockItem.Data.Length;
            int bytesLength = (int)Math.Ceiling(
                (float)GetBitIndex(dataBytesLength) / 
                BitsHelper.BitsPerByte);
            int paddedBytesLength = bytesLength.Ceiling(BytesPerInt32);
            return paddedBytesLength;
        }

        private int GetByteIndex(int dataByteIndex) =>
            GetBitIndex(dataByteIndex) / BitsHelper.BitsPerByte;

        private int GetBitIndex(int dataByteIndex) =>
            dataByteIndex / BytesPerInt32; // one bit per int32

        private void SetMaskBits<TComponent>(IEnumerable<TComponent> components) where TComponent : Component
        {
            if (components != null)
            {
                foreach (TComponent component in components)
                {
                    if (component.Node.Position.HasValue)
                    {
                        int dataByteIndex = Convert.ToInt32(component.Node.Position);
                        SetMaskBit(dataByteIndex);
                    }
                }
            }
        }

        private void SetMaskBit(int dataByteIndex)
        {
            int bitIndex = GetBitIndex(dataByteIndex);
            int bitIndexInByte = bitIndex % BitsHelper.BitsPerByte;
            int byteIndex = GetByteIndex(dataByteIndex);
            byte bitMask = BitsHelper.GetBitMask(bitIndexInByte, BitOrder.MsbFirst);
            Bytes[byteIndex] |= bitMask;
        }

        private PropertyComponent[] GetStartVertexComponents(ByteSerializerGraph graph) =>
            graph.GetPropertyComponents<IndicesChunk01>(nameof(IndicesChunk01.StartVertex));

        private bool IsMaskBitRequired(ReferenceComponent r)
        {
            if (IsSpecialScenReference(r))
                return false;

            if (r.Value == null)
            {
                if (r.Has<PropertyComponent>() && 
                    r.Parent.Get<ValueComponent>().Type.IsOneOf<Material, Mesh>())
                    return false;
                if (r.Parent.Type == typeof(MeshGroupOrShorts))
                    return false;
                if (r.Has<CollectionElementComponent>() && 
                    r.Type == typeof(MaterialTextureChild))
                    return false;
            }

            return true;
        }

        private bool IsMaskBitRequired(PropertyComponent startVertexPropertyComponent)
        {
            var startVertexPropertyValue = (ReferenceByIndex<Vertex>)startVertexPropertyComponent.Value;
            return startVertexPropertyValue.Value != null;
        }

        private bool IsSpecialScenReference(ReferenceComponent referenceComponent)
        {
            if (ModelBlockItem.Model is ScenModel && 
                referenceComponent.Type == typeof(MappingChild))
            {
                MappingSub mappingSub = referenceComponent.GetAncestorValue<MappingSub>();
                List<MappingSub> list = referenceComponent.GetAncestorValue<List<MappingSub>>();
                if (list.IndexOf(mappingSub) == 0)
                    return true;
            }
            return false;
        }

        #endregion
    }
}
