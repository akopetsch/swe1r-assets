// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using System.Numerics;

namespace SWE1R.Assets.Blocks.CommandLine
{
    public class ObjImporterConfiguration
    {
        public float PositionScale { get; set; } = 1;
        public Vector3 PositionOffset { get; set; } = Vector3.Zero;
        public bool OverrideNormals { get; set; } = false;

        public ObjImporterConfiguration() { }
    }
}
