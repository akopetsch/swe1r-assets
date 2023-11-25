// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization;
using ByteSerialization.IO;
using System.IO;

namespace SWE1R.Assets.Blocks.SplineBlock
{
    public class SplineBlockItem : BlockItem<SplineBlockItemPart>
    {
        #region Properties

        public Spline Spline { get; set; }

        #endregion

        #region Constructor

        public SplineBlockItem() : base() { }
        public SplineBlockItem(SplineBlockItem source) : base(source) { }

        #endregion

        #region Methods

        public override void Load(out ByteSerializerContext context)
        {
            using var ms = new MemoryStream(Bytes);
            Spline = new ByteSerializer().Deserialize<Spline>(ms, Endianness.BigEndian, out context);
            Spline.BlockItem = this;
        }

        public override void Unload() => Spline = null;

        public override void Save(out ByteSerializerContext context)
        {
            using var ms = new MemoryStream();
            new ByteSerializer().Serialize(ms, Spline, Endianness.BigEndian, out context);
            Part.Load(ms.ToArray());
        }

        public override BlockItem Clone() => new SplineBlockItem(this);

        #endregion
    }
}
