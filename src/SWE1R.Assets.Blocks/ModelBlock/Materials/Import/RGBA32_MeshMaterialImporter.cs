// SPDX-License-Identifier: MIT

using SWE1R.Assets.Blocks.Images;
using SWE1R.Assets.Blocks.TextureBlock;
using SWE1R.Assets.Blocks.Textures;

namespace SWE1R.Assets.Blocks.ModelBlock.Materials.Import
{
    public class RGBA32_MeshMaterialImporter : MeshMaterialImporter
    {
        #region Constructor

        public RGBA32_MeshMaterialImporter(ImageRgba32 image, Block<TextureBlockItem> textureBlock) : 
            base(image, TextureFormat.RGBA32, textureBlock)
        { }

        #endregion

        #region Methods

        // mt_w: 16, 32, 64
        // mt_h: 16, 32

        protected override MeshMaterial CreateMeshMaterial()
        {
            MeshMaterial mm = base.CreateMeshMaterial();
            mm.Bitmask = 12; // 4, 6, c, e
            return mm;
        }

        protected override MaterialTexture CreateMaterialTexture()
        {
            MaterialTexture mt = base.CreateMaterialTexture();
            mt.Mask_Unk = 0; // 0
            mt.Format = TextureFormat.RGBA32;
            mt.Flags = 256; // 64, 128, 256 // TODO: 512?
            mt.Mask = 0x03ff; // 0x01ff, 0x03ff
            return mt;
        }

        protected override MaterialTextureChild CreateMaterialTextureChild() =>
            new MaterialTextureChild() {
                Byte_2 = 8, // 4, 8, 16
                DimensionsBitmask = 0x00, // 0x00, 0x02, 0x10, 0x11, 0x22
                Byte_4 = 6, // 4, 5, 6
                Byte_5 = 6, // 4, 5
                Byte_d = 252, // 60, 124, 252
                Byte_f = 252, // 60, 124
            };

        protected override MaterialProperties CreateMaterialProperties()
        {
            MaterialProperties mp = base.CreateMaterialProperties();
            mp.Word_4 = 2; // 2
            mp.Ints_6 = new int[] {
                0x011f041f, // 0x011f031f, 0x011f041f, 0x1f1f1f01, 0x1f1f1f03, 0x031f011f
                0x07070701, // 0x01070307, 0x03070107, 0x07070701
            };
            mp.Ints_e = new int[] {
                0x1f1f1f00, // 0x1f1f1f00
                0x07070700, // 0x00070407, 0x07070700
            };
            mp.Byte_22 = 0; // 0, 255
            mp.Byte_23 = 0; // 0, 255
            mp.Byte_24 = 0; // 0, 255
            mp.Byte_25 = 0; // 0, 64, 255
            mp.Byte_2e = 0; // 0, 128
            mp.Byte_2f = 0; // 0, 128
            mp.Byte_30 = 0; // 0, 128
            mp.Byte_31 = 0; // 0, 16
            return mp;
        }

        #endregion
    }
}
