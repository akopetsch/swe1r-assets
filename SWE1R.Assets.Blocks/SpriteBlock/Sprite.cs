// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization;
using ByteSerialization.IO;
using System.IO;

namespace SWE1R.Assets.Blocks.SpriteBlock
{
    public class Sprite : BlockItem<SpritePart>
    {
        public SpriteData Data { get; set; }

        public Sprite() : base() { }
        public Sprite(Sprite source) : base(source) { }

        public override void Load(out ByteSerializerContext context)
        {
            using var ms = new MemoryStream(Bytes);
            Data = new ByteSerializer().Deserialize<SpriteData>(ms, Endianness.BigEndian, out context);
        }

        public override void Unload() => Data = null;

        public override void Save(out ByteSerializerContext context)
        {
            using var ms = new MemoryStream();
            new ByteSerializer().Serialize(ms, Data, Endianness.BigEndian, out context);
            Part.Load(ms.ToArray());
        }

        public override BlockItem Clone() => new Sprite(this);
    }
}
