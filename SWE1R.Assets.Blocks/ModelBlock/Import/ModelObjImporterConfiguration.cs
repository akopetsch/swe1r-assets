// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using System.Numerics;

namespace SWE1R.Assets.Blocks.ModelBlock.Import
{
    public class ModelObjImporterConfiguration
    {
        public float PositionScale { get; set; } = 1;
        public Vector3 PositionOffset { get; set; } = Vector3.Zero;
        
        public ModelObjImporterConfiguration() { }
    }
}
