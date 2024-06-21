// SPDX-License-Identifier: MIT

using ByteSerialization;
using System.IO;

namespace SWE1R.Assets.Blocks.SplineBlock
{
    [BlockItemType(BlockItemType.SplineBlockItem)]
    public class SplineBlockItem : BlockItem<SplineBlockItemPart>
    {
        #region Properties

        public Spline Spline { get; set; }

        #endregion

        #region Constructor

        public SplineBlockItem() :
            base()
        { }

        public SplineBlockItem(SplineBlockItem source) :
            base(source)
        { }

        #endregion

        #region Methods

        public override void Load(out ByteSerializerContext context)
        {
            using var ms = new MemoryStream(Bytes);
            Spline = new ByteSerializer().Deserialize<Spline>(ms, Endianness, out context);
            Spline.BlockItem = this;
        }

        public override void Unload() => 
            Spline = null;

        public override void Save(out ByteSerializerContext context)
        {
            using var ms = new MemoryStream();
            new ByteSerializer().Serialize(ms, Spline, Endianness, out context);
            Part.Load(ms.ToArray());
        }

        public override BlockItem Clone() => 
            new SplineBlockItem(this);

        #endregion
    }
}
