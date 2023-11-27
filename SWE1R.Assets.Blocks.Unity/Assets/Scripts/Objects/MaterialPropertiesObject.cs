// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using System;
using Swe1rMaterialProperties = SWE1R.Assets.Blocks.ModelBlock.Materials.MaterialProperties;

namespace SWE1R.Assets.Blocks.Unity.Objects
{
    [Serializable]
    public class MaterialPropertiesObject
    {
        #region Fields (serialized)

        public int alphaBpp;
        public short word_4;

        public int[] ints_6;

        public int[] ints_e;

        public short unk_16;

        public int bitmask1;
        public int bitmask2;

        public short unk_20;

        public byte byte_22;
        public byte byte_23;
        public byte byte_24;
        public byte byte_25;

        public short unk_26;
        public short unk_28;
        public short unk_2a;
        public short unk_2c;

        public byte byte_2e;
        public byte byte_2f;
        public byte byte_30;
        public byte byte_31;

        public short unk_32;

        #endregion

        public MaterialPropertiesObject(Swe1rMaterialProperties materialProperties, ModelImporter modelImporter)
        {
            alphaBpp = materialProperties.AlphaBpp;
            word_4 = materialProperties.Word_4;
            ints_6 = materialProperties.Ints_6;
            ints_e = materialProperties.Ints_e;
            unk_16 = materialProperties.Unk_16;
            bitmask1 = materialProperties.Bitmask1;
            bitmask2 = materialProperties.Bitmask2;
            unk_20 = materialProperties.Unk_20;
            byte_22 = materialProperties.Byte_22;
            byte_23 = materialProperties.Byte_23;
            byte_24 = materialProperties.Byte_24;
            byte_25 = materialProperties.Byte_25;
            unk_26 = materialProperties.Unk_26;
            unk_28 = materialProperties.Unk_28;
            unk_2a = materialProperties.Unk_2a;
            unk_2c = materialProperties.Unk_2c;
            byte_2e = materialProperties.Byte_2e;
            byte_2f = materialProperties.Byte_2f;
            byte_30 = materialProperties.Byte_30;
            byte_31 = materialProperties.Byte_31;
            unk_32 = materialProperties.Unk_32;
        }

        public Swe1rMaterialProperties Export() =>
            new Swe1rMaterialProperties() {
                AlphaBpp = alphaBpp,
                Word_4 = word_4,
                Ints_6 = ints_6,
                Ints_e = ints_e,
                Unk_16 = unk_16,
                Bitmask1 = bitmask1,
                Bitmask2 = bitmask2,
                Unk_20 = unk_20,
                Byte_22 = byte_22,
                Byte_23 = byte_23,
                Byte_24 = byte_24,
                Byte_25 = byte_25,
                Unk_26 = unk_26,
                Unk_28 = unk_28,
                Unk_2a = unk_2a,
                Unk_2c = unk_2c,
                Byte_2e = byte_2e,
                Byte_2f = byte_2f,
                Byte_30 = byte_30,
                Byte_31 = byte_31,
                Unk_32 = unk_32,
            };
    }
}
