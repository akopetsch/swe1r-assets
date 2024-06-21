﻿// SPDX-License-Identifier: MIT

using ByteSerialization;
using System.IO;

namespace SWE1R.Assets.Blocks.ModelBlock
{
    [BlockItemType(BlockItemType.ModelBlockItem)]
    public class ModelBlockItem : BlockItem<ModelBlockItemMaskPart, ModelBlockItemDataPart>
    {
        #region Properties

        public ModelBlockItemMaskPart Bitmask => Part1;
        public ModelBlockItemDataPart Data => Part2;
        public Model Model { get; set; }

        #endregion

        #region Constructor

        public ModelBlockItem() : base() { }
        public ModelBlockItem(ModelBlockItem source) : base(source) { }

        #endregion

        #region Methods

        public override void Load(out ByteSerializerContext context)
        {
            using var ser = new ByteSerializer();
            using var ms = new MemoryStream(Data.Bytes);
            Model = ser.Deserialize<Model>(ms, Endianness, out context);
            Model.BlockItem = this;
        }

        public override void Unload() => Model = null;

        public override void Save(out ByteSerializerContext context)
        {
            // Data
            using var ser = new ByteSerializer();
            using var ms = new MemoryStream();
            ser.Serialize(ms, Model, Endianness, out context);
            Data.Load(ms.ToArray());

            // Bitmask
            Bitmask.GenerateFromData(context);
        }

        public override BlockItem Clone() => new ModelBlockItem(this);

        #endregion
    }
}
