// SPDX-License-Identifier: MIT

using ByteSerialization.Attributes;

namespace SWE1R.Assets.Blocks.ModelBlock.Nodes
{
    /// <summary>
    /// <para>
    /// See also:
    /// <list type="bullet">
    ///   <item>
    ///     <see href="https://github.com/tim-tim707/SW_RACER_RE/blob/d2d15c27d81e51e91996563795643c91439147aa/src/types.h#L1280">
    ///       github.com - tim-tim707/SW_RACER_RE - types.h - swrModel_NodeSelector</see></item>
    ///   <item>
    ///     <see href="https://github.com/Olganix/Sw_Racer/blob/76c8ad9cea549ea18457846a135a7f25d48b3813/include/Swr_Model.h#L104">
    ///       github.com - Olganix/Sw_Racer - Swr_Model.h - SWR_AltN_0x5065</see></item>
    /// </list>
    /// </para>
    /// </summary>
    public class SelectorNode : FlaggedNode
    {
        #region Fields (const)

        private const int AllChildrenDisabled = -2;
        private const int AllChildrenEnabled = -1; 

        #endregion

        #region Properties (serialized)

        /// <summary>
        /// Always 0 or -1.
        /// </summary>
        [Order(0)]
        public int SelectionValue { get; set; }

        #endregion

        #region Properties (logical)

        /// <summary>
        /// Don't render any child node?
        /// </summary>
        public bool AreAllChildrenDisabled =>
            SelectionValue == AllChildrenDisabled;

        /// <summary>
        /// Render all child nodes?
        /// </summary>
        public bool AreAllChildrenEnabled =>
            SelectionValue == AllChildrenEnabled;

        /// <summary>
        /// Render selected child node only?
        /// </summary>
        public int? SelectionIndex
        {
            get => 
                SelectionValue >= 0 ? (int?)SelectionValue : null;
            set
            {
                if (value.HasValue)
                    SelectionValue = value.Value;
            }
        }

        #endregion

        #region Constructor

        public SelectorNode() : 
            base(NodeFlags.SelectorNode)
        { }

        #endregion

        #region Methods (logical)

        public void DisableAllChildren() =>
            SelectionValue = AllChildrenDisabled;

        public void EnableAllChildren() =>
            SelectionValue = AllChildrenEnabled;

        #endregion
    }
}
