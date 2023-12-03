// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

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
            Offset = (int)node.Position;

        protected static int GetValuePosition(ByteSerializerGraph graph, object value) =>
            (int)(graph.GetValueComponent(value)?.Position.Value ?? 0);

        protected static int GetPropertyPointer(Node node, string propertyName)
        {
            var record = node.Get<RecordComponent>();
            var property = record?.Properties[propertyName];
            var reference = property?.Get<ReferenceComponent>();
            return reference?.Pointer ?? 0;
        }

        public override int GetHashCode() =>
            HashCode.Combine(BlockItemValueId, Offset);
    }

    public abstract class DbBlockItemStructure<TSource> : DbBlockItemStructure, IEquatable<DbBlockItemStructure<TSource>>
    {
        public virtual bool Equals(DbBlockItemStructure<TSource> other)
        {
            if (BlockItemValueId != other.BlockItemValueId) return false;
            if (Offset != other.Offset) return false;

            return true;
        }
    }
}
