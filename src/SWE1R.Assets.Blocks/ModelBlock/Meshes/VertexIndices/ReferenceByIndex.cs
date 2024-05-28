// Copyright 2024 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization;
using ByteSerialization.Attributes;
using ByteSerialization.Attributes.Reference;
using ByteSerialization.Components.Values;
using ByteSerialization.Components.Values.Composites.Records;
using System.Collections.Generic;

namespace SWE1R.Assets.Blocks.ModelBlock.Meshes.VertexIndices
{
    public class ReferenceByIndex<T> : INodeListener
    {
        #region Properties (serialized)

        [Order(0), Reference(ReferenceHandling.ForceReuse)]
        private T Reference { get; set; }

        #endregion

        #region Members (logical)

        private IList<T> collection = null;
        private int? index = null;

        public IList<T> Collection
        {
            get => collection;
            set { collection = value; UpdateReference(); }
        }

        public int? Index
        {
            get => index;
            set { index = value; UpdateReference(); }
        }

        public T Value
        {
            get => UpdateReference();
            set { Reference = value; Index = Collection.IndexOf(value); }
        }

        #endregion

        #region Methods (helper)

        public T UpdateReference() // TODO: call automatically before serialization
        {
            if (Collection != null && index.HasValue)
                return Reference = Collection[Index.Value];
            else
                return default;
        }

        #endregion

        #region Methods (: INodeListener)

        public void OnSerializing(RecordComponent record) { }

        public void OnSerialized(RecordComponent record) { }

        public void OnDeserializing(RecordComponent record) { }

        public void OnDeserialized(RecordComponent record)
        {
            ReferenceComponent referenceComponent = record.Properties[nameof(Reference)].Get<ReferenceComponent>();
            // TODO: see ReferenceComponent.ReuseDeserializedValueComponent
            referenceComponent.Node.ValueChanged += (before, after) => {
                var element = (T)referenceComponent.Value;
                Collection = referenceComponent.ValueComponent.GetAncestorValue<IList<T>>();
                Index = Collection.IndexOf(element);
            };
        }

        #endregion

        #region  Methods (: object)

        public override string ToString() =>
            Index.HasValue ? Index.Value.ToString() : "null";

        #endregion
    }
}
