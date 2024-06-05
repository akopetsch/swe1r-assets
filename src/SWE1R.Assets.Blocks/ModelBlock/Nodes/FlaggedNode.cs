// SPDX-License-Identifier: MIT

using ByteSerialization.Attributes;
using ByteSerialization.Attributes.Helpers;
using ByteSerialization.Components.Values;
using ByteSerialization.Components.Values.Composites.Collections;
using ByteSerialization.Components.Values.Composites.Records;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SWE1R.Assets.Blocks.ModelBlock.Nodes
{
    /// <summary>
    /// <para>
    /// See also:
    /// <list type="bullet">
    ///   <item>
    ///     <see href="https://github.com/akopetsch/SW_RACER_RE/blob/d2d15c27d81e51e91996563795643c91439147aa/src/types.h#L1263">
    ///       github.com - akopetsch/SW_RACER_RE - types.h - swrModel_Node</see></item>
    ///   <item>
    ///     <see href="https://github.com/akopetsch/Sw_Racer/blob/76c8ad9cea549ea18457846a135a7f25d48b3813/include/Swr_Model.h#L85">
    ///       github.com - akopetsch/Sw_Racer - Swr_Model.h - SWR_AltN_Header</see></item>
    /// </list>
    /// </para>
    /// </summary>
    [Alignment(typeof(AlignmentHelper))]
    public abstract class FlaggedNode : INode
    {
        #region Properties (serialized)

        [RecordTypeIdentifier(NodeFlags.MeshGroupNode, typeof(MeshGroupNode))]
        [RecordTypeIdentifier(NodeFlags.BasicNode, typeof(BasicNode))]
        [RecordTypeIdentifier(NodeFlags.SelectorNode, typeof(SelectorNode))]
        [RecordTypeIdentifier(NodeFlags.LodSelectorNode, typeof(LodSelectorNode))]
        [RecordTypeIdentifier(NodeFlags.TransformedNode, typeof(TransformedNode))]
        [RecordTypeIdentifier(NodeFlags.TransformedWithPivotNode, typeof(TransformedWithPivotNode))]
        [RecordTypeIdentifier(NodeFlags.TransformedComputedNode, typeof(TransformedComputedNode))]
        // TODO: use NodeFlags.GetFlaggedNodeType()
        [Order(0)]
        public NodeFlags Flags { get; private set; }
        /// <summary>
        /// 0x2: visible, 0x4 contains visuals (maybe)
        /// </summary>
        [Order(1)]
        public int Flags1 { get; set; }
        [Order(2)]
        public int Flags2 { get; set; }
        /// <summary>
        /// |= 3, if transform was changed. 
        /// If 0x10 is set, <see cref="TransformedWithPivotNode.Pivot"/> is used.
        /// <para>
        ///   Always is 0, 3, 11 19, 27, 35 or 43 and thus seems to be a bit field.
        /// </para>
        /// </summary>
        [Order(3)]
        public short Flags3 { get; set; }
        /// <summary>
        /// Only used if <see cref="Flags5">Flags5</see> &amp; 0x4. 
        /// Sets the selected light for all child nodes to <see cref="LightIndex">LightIndex</see> + 1. 
        /// (+ 1 because 0 is the default light that is always used).
        /// </summary>
        [Order(4)]
        public short LightIndex { get; set; }
        /// <summary>
        /// If 0x1 is set, the node is mirrored. This information is crucial for 
        /// backface culling because the transforms determinant is &lt; 0. 
        /// If 0x4 is set, <see cref="LightIndex">LightIndex</see> is valid.
        /// </summary>
        [Order(5)]
        public int Flags5 { get; set; }
        [Order(6)]
        public int ChildrenCount { get; set; }
        [ElementReference, ElementTypeHelper(typeof(ElementTypeHelper))]
        [Order(7), Length(nameof(ChildrenCount)), Reference(ReferenceHandling.HighPriority)]
        public List<INode> Children { get; set; }

        #endregion

        #region Properties (serialization)

        protected virtual Type ChildType => typeof(FlaggedNode);

        #endregion

        #region Constructor

        protected FlaggedNode(NodeFlags flags) =>
            Flags = flags;

        #endregion

        #region Methods (helper)

        public void UpdateChildrenCount() => // TODO: implement in BindingComponent
            ChildrenCount = Children?.Count() ?? 0;

        #endregion

        #region Classes (serialization)

        private class AlignmentHelper : IAlignmentHelper
        {
            public int GetAlignment(RecordComponent r)
            {
                var model = r.Root.Value as Model;
                return model.HasExtraAlignment(r.Value as FlaggedNode, r.Context.Graph) ? 8 : 0;
            }
        }

        private class ElementTypeHelper : IElementTypeHelper
        {
            public Type GetElementType(CollectionComponent c) => 
                c.GetAncestorValue<FlaggedNode>().ChildType;
        }
        
        #endregion
    }
}
