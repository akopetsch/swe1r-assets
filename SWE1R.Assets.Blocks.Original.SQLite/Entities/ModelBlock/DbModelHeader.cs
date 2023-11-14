// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization.Nodes;
using SWE1R.Assets.Blocks.ModelBlock;
using System.ComponentModel.DataAnnotations.Schema;

namespace SWE1R.Assets.Blocks.Original.SQLite.Entities.ModelBlock
{
    [Table("Model_Header")]
    public class DbModelHeader : DbModelStructure<Header>
    {
        public string Type { get; set; }
        public int? Nodes_Count { get; set; }
        public int? Data_Size { get; set; }
        public int? Anims_Count { get; set; }
        public int? AltN_Count { get; set; }

        public override void CopyFrom(Node node)
        {
            base.CopyFrom(node);

            var h = (Header)node.Value;

            Type = Enum.GetName(typeof(ModelType), h.Type);
            Nodes_Count = h.Nodes.Count;
            Data_Size = h.Data?.Size;
            Anims_Count = h.Animations?.Count;
            AltN_Count = h.AltN?.Count;
        }

        public override bool Equals(DbModelStructure<Header> other)
        {
            var _other = (DbModelHeader)other;

            if (!base.Equals(_other))
                return false;

            if (Type != _other.Type) return false;
            if (Nodes_Count != _other.Nodes_Count) return false;
            if (Data_Size != _other.Data_Size) return false;
            if (Anims_Count != _other.Anims_Count) return false;
            if (AltN_Count != _other.AltN_Count) return false;

            return true;
        }

        public override bool Equals(object obj)
        {
            if (obj is DbModelHeader)
                return this.Equals((DbModelHeader)obj);
            else
                return base.Equals(obj);
        }

        public override int GetHashCode() =>
            HashCode.Combine(base.GetHashCode(), 
                Type, Nodes_Count, Data_Size, Anims_Count, AltN_Count);
    }
}
