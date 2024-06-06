// SPDX-License-Identifier: MIT

using ByteSerialization.Attributes;
using ByteSerialization.Components.Values.Composites.Records;
using SWE1R.Assets.Blocks.ModelBlock.Types;
using SWE1R.Assets.Blocks.Vectors;
using System.Collections.Generic;

namespace SWE1R.Assets.Blocks.ModelBlock.Meshes.Behaviours
{
    /// <summary>
    /// <para>
    /// See also:
    /// <list type="bullet">
    ///   <item>
    ///     <see href="https://github.com/akopetsch/SW_RACER_RE/blob/d2d15c27d81e51e91996563795643c91439147aa/src/types.h#L1481">
    ///       github.com - akopetsch/SW_RACER_RE - types.h - swrModel_Mapping</see></item>
    ///   <item>
    ///     <see href="https://github.com/akopetsch/Sw_Racer/blob/76c8ad9cea549ea18457846a135a7f25d48b3813/include/Swr_Model.h#L389">
    ///       github.com - akopetsch/Sw_Racer - Swr_Model.h - SWR_MODEL_Section7</see></item>
    /// </list>
    /// </para>
    /// </summary>
    public class Mapping
    {
        #region Properties (serialized)

        [Order(0)] public short Word_00 { get; set; }

        [Order(1)] public byte FogFlags { get; set; }
        [Order(2)] public Vector3Byte FogColor { get; set; }
        [Order(3)] public ushort FogStart { get; set; }
        [Order(4)] public ushort FogEnd { get; set; }

        [Order(5)] public ushort LightFlags { get; set; }

        [Order(6)] public Vector3Byte AmbientColor { get; set; }
        [Order(7)] public Vector3Byte LightColor { get; set; }

        [Order(8)] public byte Byte_12 { get; set; }
        [Order(9)] public byte Byte_13 { get; set; }

        [Order(10)] public Vector3Single LightVector { get; set; }

        [Order(11)] public float Float_20 { get; set; }
        [Order(12)] public float Float_24 { get; set; }

        [Offset(0x2c)]
        [Order(13)] public VehicleReaction VehicleReaction { get; set; }

        [Order(14)] public short Word_30 { get; set; }
        [Order(15)] public short Word_32 { get; set; }

        [Length(typeof(SubLengthHelper))]
        [Order(16)] public List<MappingSub> Subs { get; set; }

        #endregion

        #region Classes (helpers)

        private class SubLengthHelper : IBindingHelper
        {
            public int GetValue(PropertyComponent p) =>
                p.Root.Value is ScenModel ? 2 : 1;
        }

        #endregion
    }
}
