// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization.IO;

namespace SWE1R.Assets.Blocks.Common.Vectors
{
    public class Vector3SByte : Vector3<sbyte>
    {
        public Vector3SByte() :
            base()
        { }

        public Vector3SByte(sbyte x, sbyte y, sbyte z) :
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
            X = reader.ReadSByte();
            Y = reader.ReadSByte();
            Z = reader.ReadSByte();
        }
    }
}
