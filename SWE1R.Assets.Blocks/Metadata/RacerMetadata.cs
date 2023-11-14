// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;

namespace SWE1R.Assets.Blocks.Metadata
{
    [DebuggerDisplay("{Name}")]
    [Table("Racer")]
    public class RacerMetadata
    {
        [Key] public Racer Id { get; set; }
        public string Name { get; set; }
        public int Podd { get; set; }
        public int MAlt { get; set; }
        public int Pupp { get; set; }
        public int Lod1 { get; set; }
        public int? Lod2 { get; set; }
    }
}
