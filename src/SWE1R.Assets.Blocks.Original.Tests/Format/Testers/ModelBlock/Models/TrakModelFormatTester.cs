// SPDX-License-Identifier: MIT

using ByteSerialization.Nodes;
using SWE1R.Assets.Blocks.Metadata;
using SWE1R.Assets.Blocks.ModelBlock.Materials;
using SWE1R.Assets.Blocks.ModelBlock.Meshes;
using SWE1R.Assets.Blocks.ModelBlock.Nodes;
using SWE1R.Assets.Blocks.ModelBlock.Types;
using SWE1R.Assets.Blocks.Textures;

namespace SWE1R.Assets.Blocks.Original.Tests.Format.Testers.ModelBlock.Models
{
    public class TrakModelFormatTester : ModelFormatTester<TrakModel>
    {
        private TrackMetadata _trackMetadata;

        public virtual void Init(TrakModel value, ByteSerializerGraph byteSerializerGraph, AnalyticsFixture analyticsFixture)
        {
            base.Init(value, byteSerializerGraph, analyticsFixture);
            base.Test();

            // TODO: fix the following lines (does not work for e.g. valueId = 1001)
            var metadataProvider = new MetadataProvider();
            BlockItemValueMetadata blockItemValueMetadata = metadataProvider.GetBlockItemValueByHash(Value.BlockItem);
            _trackMetadata = metadataProvider.Tracks.FirstOrDefault(t => t.Model == blockItemValueMetadata.Id);
        }

        public override void Test()
        {
            AssertHeader();

            if (_trackMetadata?.Planet == Planet.MonGazza)
                AssertSkybox();
        }

        private void AssertHeader()
        {
            Assert.True(Value.Nodes.Count == 6);
            Assert.True(
                Value.Animations.Count >= 3 &&
                Value.Animations.Count <= 75);
            Assert.True(Value.AltN == null);
        }

        private void AssertSkybox()
        {
            var meshMaterials = Value.Skybox.GetDescendants().OfType<Mesh>()
                .Select(m => m.MeshMaterial).ToList();
            foreach (MeshMaterial meshMaterial in meshMaterials)
                AssertSkyboxMeshMaterial(meshMaterial);
        }

        private void AssertSkyboxMeshMaterial(MeshMaterial meshMaterial)
        {
            Assert.True(meshMaterial.Flags == 12);
            Assert.True(meshMaterial.TextureOffsetX == 0);
            Assert.True(meshMaterial.TextureOffsetY == 0);
            Assert.NotNull(meshMaterial.MaterialTexture);
            AssertSkyboxMaterialTexture(meshMaterial.MaterialTexture);
            Assert.NotNull(meshMaterial.Material);
            AssertSkybox(meshMaterial.Material);
        }

        private void AssertSkyboxMaterialTexture(MaterialTexture materialTexture)
        {
            Assert.True(materialTexture.Mask_Unk == 1);
            Assert.True(materialTexture.Width4 == 128);
            Assert.True(materialTexture.Height4 == 256);
            Assert.True(materialTexture.Always0_08 == 0);
            Assert.True(materialTexture.Always0_0a == 0);
            Assert.True(materialTexture.Format == TextureFormat.RGBA5551_I8);
            Assert.True(materialTexture.Word_0e == 0);
            Assert.True(materialTexture.Width == 32);
            Assert.True(materialTexture.Height == 64);
            Assert.True(materialTexture.Width_Unk == 16384); // 16384 = 32 * 512
            Assert.True(materialTexture.Width_Unk == 16384); // 32768 = 64 * 512
            Assert.True(materialTexture.TextureIndex != -1);
            Assert.True(materialTexture.Flags == 512);
            Assert.True(materialTexture.Mask == 1023);

            Assert.NotNull(materialTexture.Children[0]);
            Assert.Null(materialTexture.Children[1]);
            Assert.Null(materialTexture.Children[2]);
            Assert.Null(materialTexture.Children[3]);
            Assert.Null(materialTexture.Children[4]);
            Assert.Null(materialTexture.Children[5]);
            AssertSkyboxMaterialTextureChild(materialTexture.Children.First());
        }

        private void AssertSkyboxMaterialTextureChild(MaterialTextureChild materialTextureChild)
        {
            Assert.True(materialTextureChild.Byte_0 == 0);
            Assert.True(materialTextureChild.Byte_1 == 0);
            Assert.True(materialTextureChild.Byte_2 == 4);
            Assert.True(
                materialTextureChild.DimensionsBitmask == 0 ||
                materialTextureChild.DimensionsBitmask ==
                    (DimensionsBitmask.FlippedVertically | DimensionsBitmask.FlippedHorizontally));
            Assert.True(materialTextureChild.Byte_4 == 5);
            Assert.True(materialTextureChild.Byte_5 == 6);
            Assert.True(materialTextureChild.Byte_6 == 0);
            Assert.True(materialTextureChild.Byte_7 == 0);
            Assert.True(materialTextureChild.Byte_c == 0);
            Assert.True(materialTextureChild.Byte_d == 124);
            Assert.True(materialTextureChild.Byte_e == 0);
            Assert.True(materialTextureChild.Byte_f == 252);
        }

        private void AssertSkybox(Material material)
        {
            Assert.True(material.AlphaBpp == 0);
            Assert.True(material.Word_4 == 1);
            Assert.True(material.Ints_6[0] == 0x011f041f);
            Assert.True(material.Ints_6[1] == 0x07070704);
            Assert.True(material.Ints_e[0] == 0x011f041f);
            Assert.True(material.Ints_e[1] == 0x07070704);
            Assert.True(material.Unk_16 == 0);
            Assert.True(material.Bitmask1 == 0x0c082008);
            Assert.True(material.Bitmask2 == 0x03022008);
            Assert.True(material.Byte_22 == 0);
            Assert.True(material.Byte_23 == 0);
            Assert.True(material.Byte_24 == 0);
            Assert.True(material.Byte_25 == 0);
            Assert.True(material.Unk_26 == 0);
            Assert.True(material.Unk_28 == 0);
            Assert.True(material.Unk_2a == 0);
            Assert.True(material.Unk_2c == 0);
            Assert.True(material.Byte_2e == 0);
            Assert.True(material.Byte_2f == 0);
            Assert.True(material.Byte_30 == 0);
            Assert.True(material.Byte_31 == 0);
            Assert.True(material.Unk_32 == 0);
        }
    }
}
