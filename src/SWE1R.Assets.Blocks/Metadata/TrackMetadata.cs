// SPDX-License-Identifier: GPL-2.0-only

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;

namespace SWE1R.Assets.Blocks.Metadata
{
    [DebuggerDisplay("{" + nameof(DebuggerDisplay) + ",nq}")]
    [Table("Track")]
    public class TrackMetadata
    {
        #region Properties (helper)

        private string DebuggerDisplay => Name;

        #endregion

        #region Properties

        [Key] public Track Id { get; set; }
        public string Name { get; set; }
        public Planet Planet { get; set; }
        public int Model { get; set; }

        #endregion
    }
}
