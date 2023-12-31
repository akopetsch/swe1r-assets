﻿// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;

namespace SWE1R.Assets.Blocks.Metadata
{
    // https://www.mobygames.com/game/276/star-wars-episode-i-racer/releases/

    [DebuggerDisplay("{" + nameof(DebuggerDisplay) + ",nq}")]
    [Table("Release")]
    public class ReleaseMetadata
    {
        #region Properties (helper)

        private string DebuggerDisplay => Name;

        #endregion

        #region Properties (serialized)

        [Key] public int Id { get; set; }
        public string Name { get; set; }
        public int ModelBlockId { get; set; }
        public int SplineBlockId { get; set; }
        public int SpriteBlockId { get; set; }
        public int TextureBlockId { get; set; }

        #endregion

        #region Methods

        public int GetBlockId<TItem>() where TItem : BlockItem =>
            GetBlockId(BlockItemTypeAttributeHelper.GetBlockItemType(typeof(TItem)));

        public int GetBlockId(BlockItemType blockItemType)
        {
            switch (blockItemType)
            {
                case BlockItemType.ModelBlockItem: return ModelBlockId;
                case BlockItemType.SplineBlockItem: return SplineBlockId;
                case BlockItemType.SpriteBlockItem: return SpriteBlockId;
                case BlockItemType.TextureBlockItem: return TextureBlockId;
                default: throw new InvalidOperationException();
            }
        }

        #endregion
    }
}
