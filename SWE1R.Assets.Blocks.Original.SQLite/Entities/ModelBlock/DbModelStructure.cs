// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization.Attributes.Reference;
using ByteSerialization.Components.Values.Composites.Records;
using ByteSerialization.Nodes;
using SWE1R.Assets.Blocks.ModelBlock;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SWE1R.Assets.Blocks.Original.SQLite.Entities.ModelBlock
{
    public abstract class DbModelStructure
    {
        [Key, Column(Order = 0)] public int Model { get; set; }
        [Key, Column(Order = 1)] public int Offset { get; set; }

        public virtual void CopyFrom(Node node)
        {
            Model = (node.Root.Value as Header).Model.Index.Value;
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
            HashCode.Combine(Model, Offset);
    }

    public abstract class DbModelStructure<TSource> : DbModelStructure, IEquatable<DbModelStructure<TSource>>
    {
        public virtual bool Equals(DbModelStructure<TSource> other)
        {
            if (Model != other.Model) return false;
            if (Offset != other.Offset) return false;

            return true;
        }
    }
}
