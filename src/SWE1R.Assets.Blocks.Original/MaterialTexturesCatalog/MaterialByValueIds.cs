// SPDX-License-Identifier: GPL-2.0-only

using SWE1R.Assets.Blocks.Metadata;
using SWE1R.Assets.Blocks.ModelBlock.Materials;

namespace SWE1R.Assets.Blocks.Original.MaterialTexturesCatalog
{
    public class MaterialByValueIds
    {
        public ReleaseMetadata ReleaseMetadata { get; set; }
        public BlockMetadata ModelBlockMetadata { get; set; }
        public BlockMetadata TextureBlockMetadata { get; set; }
        public Material Material { get; set; }
        public int ModelValueId { get; set; }
        public int? TextureValueId { get; set; }
    }
}
