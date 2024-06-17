// SPDX-License-Identifier: MIT

using ByteSerialization.Attributes.Reference;
using ByteSerialization.Components.Values.Composites.Records;
using ByteSerialization.Nodes;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SWE1R.Assets.Blocks.Original.SQLite.Entities
{
    [PrimaryKey(nameof(BlockItemValueId), nameof(Offset))]
    public abstract class DbBlockItemStructure
    {
        [Key, Column(Order = 0)] public int BlockItemValueId { get; set; }
        [Key, Column(Order = 1)] public int Offset { get; set; }

        public virtual void CopyFrom(Node node) =>
            Offset = Convert.ToInt32(node.Position);

        protected static int GetValuePosition(ByteSerializerGraph graph, object value) =>
            Convert.ToInt32(graph.GetValueComponent(value)?.Position.Value ?? 0);

        protected static int GetPropertyPointer(Node node, string propertyName)
        {
            var record = node.Get<RecordComponent>();
            var property = record?.Properties[propertyName];
            var reference = property?.Get<ReferenceComponent>();
            return reference?.Pointer ?? 0;
        }

        public override int GetHashCode() =>
            CombineHashCodes(BlockItemValueId, Offset);

        internal static int CombineHashCodes(params object[] objects)
        {
            // TODO: use ByteSerialization.IO.HashCodeHelper
            var hashCode = new HashCode();
            foreach (var obj in objects)
                hashCode.Add(obj);
            return hashCode.ToHashCode();
        }
    }

    public abstract class DbBlockItemStructure<TSource> : DbBlockItemStructure, IEquatable<DbBlockItemStructure<TSource>>
    {
        public virtual bool Equals(DbBlockItemStructure<TSource> x)
        {
            if (BlockItemValueId != x.BlockItemValueId) return false;
            if (Offset != x.Offset) return false;

            return true;
        }
    }
}
