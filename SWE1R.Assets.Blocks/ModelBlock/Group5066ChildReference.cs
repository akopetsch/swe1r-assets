// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization;
using ByteSerialization.Attributes;
using ByteSerialization.Attributes.Reference;
using ByteSerialization.Components.Values;
using ByteSerialization.Components.Values.Composites.Collections;
using ByteSerialization.Components.Values.Composites.Records;
using SWE1R.Assets.Blocks.ModelBlock.Nodes;
using System.Linq;

namespace SWE1R.Assets.Blocks.ModelBlock
{
    public class Group5066ChildReference : INodeListener
    {
        #region Properties (serialized)

        [Order(0)]
        internal int Pointer { get; private set; }

        #endregion

        #region Properties (logical)

        public Group5066 Group5066 { get; set; }

        public int Index { get; set; }

        public FlaggedNode Child
        {
            get => (FlaggedNode)Group5066.Children[Index];
            set => Group5066.Children[Index] = value;
        }

        #endregion

        #region Methods (: INodeListener)

        public void OnSerializing(RecordComponent record)
        {
            
            record.Root.AfterSerializing += () => {
                long pointerPropertyPosition = record.Properties[nameof(Pointer)].Position.Value;
                var collectionComponent = (CollectionComponent)record.Graph.GetValueComponent(Group5066.Children);
                Pointer = (int)collectionComponent.Elements[Index].Position.Value;
                record.Writer.AtPosition(pointerPropertyPosition, w => w.Write(Pointer));
            };
        }

        public void OnSerialized(RecordComponent record) { }

        public void OnDeserializing(RecordComponent record) { }

        public void OnDeserialized(RecordComponent record)
        {
            record.Root.AfterDeserializing += () => {
                ReferenceComponent referenceComponent = record.Graph.References.First(rc => rc.Position == Pointer);
                Group5066 = (Group5066)referenceComponent.GetAncestorValue<FlaggedNode>();
                Index = referenceComponent.Get<CollectionElementComponent>().Index;
            };
        }

        #endregion
    }
}
