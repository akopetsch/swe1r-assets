// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization.IO;

namespace SWE1R.Assets.Blocks.Common.Vectors
{
    public class Vector3Int16 : Vector3<short>
    {
        public Vector3Int16() :
            base()
        { }

        public Vector3Int16(short x, short y, short z) :
            base(x, y, z)
        { }

        public override void Serialize(EndianBinaryWriter writer)
        {
            writer.Write(X);
            writer.Write(Y);
            writer.Write(Z);
        }

        public override void Deserialize(EndianBinaryReader reader)
        {
            X = reader.ReadInt16();
            Y = reader.ReadInt16();
            Z = reader.ReadInt16();
        }

        public static implicit operator Vector3Single(Vector3Int16 v) =>
            new Vector3Single(v.X, v.Y, v.Z);
    }
}
