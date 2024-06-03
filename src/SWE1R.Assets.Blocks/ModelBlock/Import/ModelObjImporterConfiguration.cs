// SPDX-License-Identifier: MIT

using SWE1R.Assets.Blocks.ModelBlock.Materials;
using System.Numerics;

namespace SWE1R.Assets.Blocks.ModelBlock.Import
{
    public class ModelObjImporterConfiguration
    {
        #region Properties

        public float PositionScale { get; set; } = 1;
        public Vector3 PositionOffset { get; set; } = Vector3.Zero;
        public int? MaxVertexCountPerMesh { get; set; } = 1000;
        public int IndicesRangeMaxLength { get; set; } = byte.MaxValue / 8;
        public bool TryFirstMaterialAsFallback { get; set; } = false; // HACK: workaround for missing 'usemtl'
        public Material FallbackMaterial { get; set; } = null;
        public bool PrintDebugInfo { get; set; } = false;

        #endregion

        #region Constructor

        public ModelObjImporterConfiguration() { }

        #endregion
    }
}
