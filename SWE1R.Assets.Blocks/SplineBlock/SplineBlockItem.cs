// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization;
using ByteSerialization.IO;
using System.IO;

namespace SWE1R.Assets.Blocks.SplineBlock
{
    public class SplineBlockItem : BlockItem<SplinePart>
    {
        #region Properties

        public SplineData Data { get; set; }

        #endregion

        #region Constructor

        public SplineBlockItem() : base() { }
        public SplineBlockItem(SplineBlockItem source) : base(source) { }

        #endregion

        #region Methods

        public override void Load(out ByteSerializerContext context)
        {
            using var s = new MemoryStream(Bytes);
            Data = new ByteSerializer().Deserialize<SplineData>(s, Endianness.BigEndian, out context);
        }

        public override void Unload() => Data = null;

        public override void Save(out ByteSerializerContext context)
        {
            using var ms = new MemoryStream();
            new ByteSerializer().Serialize(ms, Data, Endianness.BigEndian, out context);
            Part.Load(ms.ToArray());
        }

        public override BlockItem Clone() => new SplineBlockItem(this);

        #endregion
    }
}
