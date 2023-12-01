// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;

namespace SWE1R.Assets.Blocks.Metadata
{
    [DebuggerDisplay("{" + nameof(DebuggerDisplay) + ",nq}")]
    [Table("Release")]
    public class ReleaseMetadata
    {
        #region Properties (helper)

        private string DebuggerDisplay => Name;

        #endregion

        [Key] public int Id { get; set; }
        public string Name { get; set; }
        public int ModelBlockId { get; set; }
        public int SplineBlockId { get; set; }
        public int SpriteBlockId { get; set; }
        public int TextureBlockId { get; set; }
    }
}
