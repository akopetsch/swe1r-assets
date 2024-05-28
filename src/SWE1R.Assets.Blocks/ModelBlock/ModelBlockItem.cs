// Copyright 2024 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization;
using ByteSerialization.IO;
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

        public override BlockItemType BlockItemType =>
            BlockItemType.ModelBlockItem;

        #endregion

        #region Constructor

        public ModelBlockItem() : base() { }
        public ModelBlockItem(ModelBlockItem source) : base(source) { }

        #endregion

        #region Methods

        public override void Load(out ByteSerializerContext context)
        {
            using var ms = new MemoryStream(Data.Bytes);
            Model = new ByteSerializer().Deserialize<Model>(ms, Endianness.BigEndian, out context);
            Model.BlockItem = this;
        }

        public override void Unload() => Model = null;

        public override void Save(out ByteSerializerContext context)
        {
            // Data
            using (var ms = new MemoryStream())
            {
                new ByteSerializer().Serialize(ms, Model, Endianness.BigEndian, out context);
                Data.Load(ms.ToArray());
            }

            // Bitmask
            Bitmask.GenerateFromData(context);
        }

        public override BlockItem Clone() => new ModelBlockItem(this);

        #endregion
    }
}
