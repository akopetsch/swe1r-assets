﻿// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;

namespace SWE1R.Assets.Blocks.Metadata
{
    [DebuggerDisplay("{Name}")]
    [Table("Track")]
    public class TrackMetadata
    {
        [Key] public Track Id { get; set; }
        public string Name { get; set; }
        public Planet Planet { get; set; }
        public int Model { get; set; }
    }
}
