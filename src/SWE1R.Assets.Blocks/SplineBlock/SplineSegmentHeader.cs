// SPDX-License-Identifier: MIT

using ByteSerialization.Attributes;

namespace SWE1R.Assets.Blocks.SplineBlock
{
    /// <summary>
    /// <para>
    /// See also:
    /// <list type="bullet">
    ///   <item>
    ///     <see href="https://github.com/louriccia/blender-swe1r/blob/b0d4019addb97d51523019ca1233411fae0508d6/splineblock.py#L103">
    ///       github.com - louriccia/blender-swe1r - splineblock.py - Spline</see></item>
    /// </list>
    /// </para>
    /// </summary>
    public class SplineSegmentHeader 
    {
        /// <summary>
        /// <para>Offset: 0x00</para>
        /// </summary>
        [Order(0)]
        public short Word_0 { get; set; }
        /// <summary>
        /// <para>Offset: 0x02</para>
        /// </summary>
        [Order(1)]
        public short Word_2 { get; set; }
        /// <summary>
        /// <para>Offset: 0x04</para>
        /// </summary>
        [Order(2)]
        public int ElementsCount { get; set; }
        /// <summary>
        /// <para>Offset: 0x08</para>
        /// </summary>
        [Order(3)]
        public int UnknownCount { get; set; }
        /// <summary>
        /// <para>Offset: 0x0c</para>
        /// </summary>
        [Order(4)]
        public short Word_c { get; set; }
        /// <summary>
        /// <para>Offset: 0x0e</para>
        /// </summary>
        [Order(5)]
        public short Word_e { get; set; }
    }
}
