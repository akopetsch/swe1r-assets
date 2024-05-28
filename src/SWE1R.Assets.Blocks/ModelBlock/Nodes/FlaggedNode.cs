// Copyright 2024 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

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
    /// <see href="https://github.com/akopetsch/Sw_Racer/blob/76c8ad9cea549ea18457846a135a7f25d48b3813/include/Swr_Model.h#L85">SWR_AltN_Header</see>
    /// </summary>
    [Alignment(typeof(AlignmentHelper))]
    public abstract class FlaggedNode : INode
    {
        #region Properties (serialized)

        [RecordTypeIdentifier(NodeFlags.MeshGroup3064, typeof(MeshGroup3064))]
        [RecordTypeIdentifier(NodeFlags.Group5064, typeof(Group5064))]
        [RecordTypeIdentifier(NodeFlags.Group5065, typeof(Group5065))]
        [RecordTypeIdentifier(NodeFlags.Group5066, typeof(Group5066))]
        [RecordTypeIdentifier(NodeFlags.TransformableD064, typeof(TransformableD064))]
        [RecordTypeIdentifier(NodeFlags.TransformableD065, typeof(TransformableD065))]
        [RecordTypeIdentifier(NodeFlags.UnknownD066, typeof(UnknownD066))]
        // TODO: use NodeFlags.GetFlaggedNodeType()
        [Order(0)]
        public NodeFlags Flags { get; protected set; }
        [Order(1)]
        public int Bitfield1 { get; set; }
        [Order(2)]
        public int Bitfield2 { get; set; }
        /// <summary>
        /// Always is 0, 3, 11 19, 27, 35 or 43 and thus seems to be a bit field.
        /// </summary>
        [Order(3)]
        public short Number { get; set; }
        [Order(4)]
        public short Padding1 { get; set; }
        [Order(5)]
        public int Padding2 { get; set; }
        [Order(6)]
        public int ChildrenCount { get; set; }
        [Length(nameof(ChildrenCount))]
        [Reference(ReferenceHandling.HighPriority)]
        [ElementReference, ElementTypeHelper(typeof(ElementTypeHelper))]
        [Order(7)]
        public List<INode> Children { get; set; }

        #endregion

        #region Properties (serialization)

        protected virtual Type ChildType => typeof(FlaggedNode);

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
