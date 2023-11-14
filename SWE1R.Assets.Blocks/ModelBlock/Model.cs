// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization;
using ByteSerialization.IO;
using System.IO;

namespace SWE1R.Assets.Blocks.ModelBlock
{
    public class Model : BlockItem<ModelMask, ModelData>
    {
        public ModelMask Bitmask => Part1;
        public ModelData Data => Part2;
        public Header Header { get; set; }
        
        public Model() : base() { }
        public Model(Model source) : base(source) { }
        
        public override void Load(out ByteSerializerContext context)
        {
            using (var ms = new MemoryStream(Data.Bytes))
                Header = new ByteSerializer().Deserialize<Header>(ms, Endianness.BigEndian, out context);
            Header.Model = this;
        }

        public override void Unload() => Header = null;

        public override void Save(out ByteSerializerContext context)
        {
            // Data
            using (var ms = new MemoryStream())
            {
                new ByteSerializer().Serialize(ms, Header, Endianness.BigEndian, out context);
                Data.Load(ms.ToArray());
            }

            // Bitmask
            Bitmask.GenerateFromData(context);
        }

        public override BlockItem Clone() => new Model(this);
    }
}
