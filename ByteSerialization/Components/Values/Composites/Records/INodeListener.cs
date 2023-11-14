// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization.Components.Values.Composites.Records;
using ByteSerialization.Nodes;

namespace ByteSerialization
{
    // TODO: INodeListener only for RecordComponent?

    public interface INodeListener
    {
        void OnSerializing(RecordComponent record);
        void OnSerialized(RecordComponent record);

        void OnDeserializing(RecordComponent record);
        void OnDeserialized(RecordComponent record);
    }

    internal static class INodeListenerExtensions
    {
        public static void SubscribeTo(this INodeListener nodeListener, RecordComponent record)
        {
            Node n = record.Node;
            n.OnSerializing += () => nodeListener.OnSerializing(record);
            n.AfterSerializing += () => nodeListener.OnSerialized(record);
            n.OnDeserializing += () => nodeListener.OnDeserializing(record);
            n.AfterDeserializing += () => nodeListener.OnDeserialized(record);
        }

        public static void UnsubscribeFrom(this INodeListener nodeListener, RecordComponent record)
        {
            Node n = record.Node;
            n.OnSerializing -= () => nodeListener.OnSerializing(record);
            n.AfterSerializing -= () => nodeListener.OnSerialized(record);
            n.OnDeserializing -= () => nodeListener.OnDeserializing(record);
            n.AfterDeserializing -= () => nodeListener.OnDeserialized(record);
            // TODO: TEST: UnsubscribeFrom
        }
    }
}
