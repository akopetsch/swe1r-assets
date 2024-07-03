// SPDX-License-Identifier: MIT

using ByteSerialization.Attributes;
using SWE1R.Assets.Blocks.Vectors;

namespace SWE1R.Assets.Blocks.SplineBlock
{
    /// <summary>
    /// <para>
    /// See also:
    /// <list type="bullet">
    ///   <item>
    ///     <see href="https://github.com/louriccia/blender-swe1r/blob/b0d4019addb97d51523019ca1233411fae0508d6/splineblock.py#L29">
    ///       github.com - louriccia/blender-swe1r - splineblock.py - SplinePoint</see></item>
    /// </list>
    /// </para>
    /// </summary>
    public class SplineSegmentData 
    {
        /// <summary>
        /// <para>Offset: 0x00</para>
        /// </summary>
        [Order(0)]
        public short Word_00 { get; set; }
        /// <summary>
        /// <para>Offset: 0x02</para>
        /// </summary>
        [Order(1)]
        public short Word_02 { get; set; }
        /// <summary>
        /// <para>Offset: 0x04</para>
        /// </summary>
        [Order(2)]
        public short Word_04 { get; set; }
        /// <summary>
        /// <para>Offset: 0x06</para>
        /// </summary>
        [Order(3)]
        public short Word_06 { get; set; }
        /// <summary>
        /// <para>Offset: 0x08</para>
        /// </summary>
        [Order(4)]
        public short PreviousNodeId { get; set; }
        /// <summary>
        /// <para>Offset: 0x0a</para>
        /// </summary>
        [Order(5)]
        public short Unk_0a { get; set; }
        /// <summary>
        /// <para>Offset: 0x0c</para>
        /// </summary>
        [Order(6)]
        public short Unk_0c { get; set; }
        /// <summary>
        /// <para>Offset: 0x0e</para>
        /// </summary>
        [Order(7)]
        public short Unk_0e { get; set; }
        /// <summary>
        /// <para>Offset: 0x10</para>
        /// </summary>
        [Order(8)]
        public Vector3Single KnotVector { get; set; }
        /// <summary>
        /// <para>Offset: 0x24</para>
        /// </summary>
        [Order(9), Offset(0x24)]
        public float Float_24 { get; set; }
        /// <summary>
        /// <para>Offset: 0x28</para>
        /// </summary>
        [Order(10)]
        public Vector3Single ControlPoint1 { get; set; } // before KnotVector
        /// <summary>
        /// <para>Offset: 0x34</para>
        /// </summary>
        [Order(11)]
        public Vector3Single ControlPoint2 { get; set; } // after KnotVector
        /// <summary>
        /// <para>Offset: 0x40</para>
        /// </summary>
        [Order(12)]
        public int Unk_40 { get; set; }
    }
}
