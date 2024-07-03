// SPDX-License-Identifier: MIT

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;

namespace SWE1R.Assets.Blocks.Metadata
{
    /// <summary>
    /// See also:
    /// <para>
    /// <list type="bullet">
    ///   <item>
    ///     <see href="https://starwars.fandom.com/wiki/Star_Wars:_Episode_I_Racer#Tournament_structure">
    ///       starwars.fandom.com - 'Star Wars: Episode I Racer' - 'Tournament structure'</see></item>
    /// </list>
    /// </para>
    /// </summary>
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
