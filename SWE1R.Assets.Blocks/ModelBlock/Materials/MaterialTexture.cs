// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization.Attributes;
using SWE1R.Assets.Blocks.ModelBlock.Meshes;
using SWE1R.Assets.Blocks.Textures;
using System.Diagnostics;

namespace SWE1R.Assets.Blocks.ModelBlock.Materials
{
    /// <summary>
    /// <see href="https://github.com/akopetsch/Sw_Racer/blob/76c8ad9cea549ea18457846a135a7f25d48b3813/include/Swr_Model.h#L241">SWR_MODEL_Section5</see>
    /// </summary>
    [DebuggerDisplay("Id = {" + nameof(TextureIndex) + ",nq}")]
    [Sizeof(0x40)]
    public class MaterialTexture
    {
        #region Constants

        public const int ChildrenCount = 6;

        #endregion

        #region Properties (serialized)

        [Order(0)] public int Mask_Unk { get; set; }
        /// <summary>
        /// Always four times <see cref="Width">Width</see>.
        /// </summary>
        [Order(1)] public short Width4 { get; set; }
        /// <summary>
        /// Always four times <see cref="Height">Height</see>.
        /// </summary>
        [Order(2)] public short Height4 { get; set; }
        /// <summary>
        /// Always zero.
        /// </summary>
        [Order(3)] public short Always0_08 { get; set; } = 0;
        /// <summary>
        /// Always zero.
        /// </summary>
        [Order(4)] public short Always0_0a { get; set; } = 0;
        [Order(5)] public TextureFormat TextureFormat { get; set; }
        [Order(6)] public short Word_0e { get; set; }
        [Order(7)] public short Width { get; set; }
        [Order(8)] public short Height { get; set; }
        /// <summary>
        /// Always 128, 256 or 512 times <see cref="Width">Width</see>.
        /// </summary>
        [Order(9)] public ushort Width_Unk { get; set; }
        /// <summary>
        /// Always 128, 256 or 512 times <see cref="Height">Height</see>.
        /// </summary>
        [Order(10)] public ushort Height_Unk { get; set; }
        [Order(11)] public short Flags { get; set; }
        [Order(12)] public short Mask { get; set; }

        [Length(ChildrenCount)]
        [ElementReference]
        [Order(13)] public MaterialTextureChild[] Children { get; set; }

        [Offset(0x38)]
        [Order(14)] public TextureIndex TextureIndex { get; set; }

        #endregion

        #region Constructor

        public MaterialTexture() =>
            Children = new MaterialTextureChild[ChildrenCount];

        #endregion
    }
}
