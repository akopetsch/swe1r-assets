// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization.Attributes;
using System.Collections.Generic;
using System.Linq;

namespace SWE1R.Assets.Blocks.ModelBlock
{
    public class HeaderData
    {
        private const int sizeMultiplier = 4;

        [Order(0)] public int Size { get; set; }

        [Sizeof(nameof(Size), Multiplier = sizeMultiplier)]
        [Order(1)] public List<LightStreakOrInteger> List { get; set; }

        public void UpdateSize() => // TODO: implement in BindingComponent
            Size = List.Sum(x => x.StructureSize) / sizeMultiplier;
    }
}
