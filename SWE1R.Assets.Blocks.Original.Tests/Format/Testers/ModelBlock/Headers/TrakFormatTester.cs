// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization.Nodes;
using SWE1R.Assets.Blocks.Metadata;
using SWE1R.Assets.Blocks.ModelBlock.Meshes;
using SWE1R.Assets.Blocks.ModelBlock.Nodes;
using SWE1R.Assets.Blocks.ModelBlock.Types;

namespace SWE1R.Assets.Blocks.Original.Tests.Format.Testers.ModelBlock.Headers
{
    public class TrakFormatTester : HeaderFormatTester<TrakHeader>
    {
        private TrackMetadata TrackMetadata { get; }

        public TrakFormatTester(TrakHeader value, Graph byterSerializationGraph, AnalyticsFixture analyticsFixture) :
            base(value, byterSerializationGraph, analyticsFixture)
        {
            TrackMetadata = MetadataProvider.Tracks.First(t => t.Model == Value.Model.Index.Value);
        }

        public override void Test()
        {
            AssertHeader();

            if (TrackMetadata.Planet == Planet.MonGazza)
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
            var materials = Value.Skybox.GetDescendants().OfType<MeshGroup3064>()
                .SelectMany(mg => mg.Meshes)
                .Select(m => m.Material).ToList();
            foreach (Material material in materials)
                AssertSkyboxMaterial(material);
        }

        private void AssertSkyboxMaterial(Material material)
        {
            Assert.True(material.Int == 12);
            Assert.True(material.Width_Unk_Dividend == 0);
            Assert.True(material.Height_Unk_Dividend == 0);

            if (TrackMetadata.Planet == Planet.MonGazza)
            {
                Assert.NotNull(material.Texture);
                AssertSkyboxMaterialTexture(material.Texture);
            }

            Assert.NotNull(material.Properties);
            AssertSkyboxMaterialProperties(material.Properties);
        }

        private void AssertSkyboxMaterialTexture(MaterialTexture materialTexture)
        {
            Assert.True(materialTexture.Mask_Unk == 1);
            Assert.True(materialTexture.Width4 == 128);
            Assert.True(materialTexture.Height4 == 256);
            Assert.True(materialTexture.Always0_08 == 0);
            Assert.True(materialTexture.Always0_0a == 0);
            Assert.True(materialTexture.Byte_0c == 2);
            Assert.True(materialTexture.Byte_0d == 1);
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

        private void AssertSkyboxMaterialProperties(MaterialProperties materialProperties)
        {
            Assert.True(materialProperties.AlphaBpp == 0);
            Assert.True(materialProperties.Word_4 == 1);
            Assert.True(materialProperties.Ints_6[0] == 0x011f041f); // = 18809887
            Assert.True(materialProperties.Ints_6[1] == 0x07070704);
            Assert.True(materialProperties.Ints_e[0] == 0x011f041f); // = 18809887
            Assert.True(materialProperties.Ints_e[1] == 0x07070704);
            Assert.True(materialProperties.Unk_16 == 0);
            Assert.True(materialProperties.Bitmask1 == 0x0c082008);
            Assert.True(materialProperties.Bitmask2 == 0x03022008);
            Assert.True(materialProperties.Byte_22 == 0);
            Assert.True(materialProperties.Byte_23 == 0);
            Assert.True(materialProperties.Byte_24 == 0);
            Assert.True(materialProperties.Byte_25 == 0);
            Assert.True(materialProperties.Unk_26 == 0);
            Assert.True(materialProperties.Unk_28 == 0);
            Assert.True(materialProperties.Unk_2a == 0);
            Assert.True(materialProperties.Unk_2c == 0);
            Assert.True(materialProperties.Byte_2e == 0);
            Assert.True(materialProperties.Byte_2f == 0);
            Assert.True(materialProperties.Byte_30 == 0);
            Assert.True(materialProperties.Byte_31 == 0);
            Assert.True(materialProperties.Unk_32 == 0);
        }
    }
}
