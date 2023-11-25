﻿// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization.Attributes.Reference;
using ByteSerialization.Components.Values.Composites.Records;
using ByteSerialization.Nodes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SWE1R.Assets.Blocks.Original.SQLite.Entities
{
    public abstract class DbBlockItemStructure
    {
        [Key, Column(Order = 0)] public int BlockItem { get; set; }
        [Key, Column(Order = 1)] public int Offset { get; set; }

        public virtual void CopyFrom(Node node)
        {
            BlockItem = (node.Root.Value as BlockItemValue).BlockItem.Index.Value;
            Offset = (int)node.Position;
        }

        protected static int GetValuePosition(Graph graph, object value) =>
            (int)(graph.GetValueComponent(value)?.Position.Value ?? 0);

        protected static int GetPropertyPointer(Node node, string propertyName)
        {
            var record = node.Get<RecordComponent>();
            var property = record?.Properties[propertyName];
            var reference = property?.Get<ReferenceComponent>();
            return reference?.Pointer ?? 0;
        }

        public override int GetHashCode() =>
            HashCode.Combine(BlockItem, Offset);
    }

    public abstract class DbBlockItemStructure<TSource> : DbBlockItemStructure, IEquatable<DbBlockItemStructure<TSource>>
    {
        public virtual bool Equals(DbBlockItemStructure<TSource> other)
        {
            if (BlockItem != other.BlockItem) return false;
            if (Offset != other.Offset) return false;

            return true;
        }
    }
}