// SPDX-License-Identifier: MIT

using ByteSerialization;
using ByteSerialization.Attributes.Reference;
using ByteSerialization.Components.Values;
using ByteSerialization.Components.Values.Composites.Collections;
using ByteSerialization.Components.Values.Composites.Records;
using ByteSerialization.IO;
using ByteSerialization.Utils;
using SWE1R.Assets.Blocks.ModelBlock.Materials;
using SWE1R.Assets.Blocks.ModelBlock.Meshes;
using SWE1R.Assets.Blocks.ModelBlock.N64Sdk;
using SWE1R.Assets.Blocks.ModelBlock.N64Sdk.GraphicsCommands;
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

            // N64GspVertexCommand.V
            SetMaskBits(
                context.Graph.GetPropertyComponents<GSpVertexCommand>(nameof(GSpVertexCommand.V))
                .Where(IsMaskBitRequired));

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
            int paddedBytesLength = CeilingHelper.Ceiling(bytesLength, BytesPerInt32);
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

        private bool IsMaskBitRequired(ReferenceComponent r)
        {
            if (IsSpecialScenReference(r))
                return false;

            if (r.Value == null)
            {
                if (IsPropertyComponentOf<Mesh>(r) || 
                    IsPropertyComponentOf<MeshMaterial>(r))
                    return false;
                if (r.Parent.Type == typeof(MeshGroupNodeOrShorts))
                    return false;
                if (r.Has<CollectionElementComponent>() && 
                    r.Type == typeof(MaterialTextureChild))
                    return false;
            }

            return true;
        }

        private bool IsPropertyComponentOf<TRecordValue>(ReferenceComponent r) =>
            r.Get<PropertyComponent>()?.Record.Type == typeof(TRecordValue);

        private bool IsMaskBitRequired(PropertyComponent vPropertyComponent)
        {
            var vPropertyValue = (ReferenceByIndex<Vtx>)vPropertyComponent.Value;
            return vPropertyValue.Value != null;
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
