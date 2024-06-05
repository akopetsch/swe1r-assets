// SPDX-License-Identifier: MIT

using ByteSerialization.Nodes;
using SWE1R.Assets.Blocks.ModelBlock;
using System.ComponentModel.DataAnnotations.Schema;

namespace SWE1R.Assets.Blocks.Original.SQLite.Entities.ModelBlock
{
    [Table(nameof(Model))]
    public class DbModel : DbBlockItemStructure<Model>
    {
        #region Properties

        public string Type { get; set; }
        public int? Nodes_Count { get; set; }
        public int? Data_Size { get; set; }
        public int? Animations_Count { get; set; }
        public int? AltN_Count { get; set; }

        #endregion

        public override void CopyFrom(Node node)
        {
            base.CopyFrom(node);

            var x = (Model)node.Value;

            Type = Enum.GetName(typeof(ModelType), x.Type);
            Nodes_Count = x.Nodes.Count;
            Data_Size = x.Data?.Size;
            Animations_Count = x.Animations?.Count;
            AltN_Count = x.AltN?.Count;
        }

        public override bool Equals(DbBlockItemStructure<Model> other)
        {
            var x = (DbModel)other;

            if (!base.Equals(x))
                return false;

            if (Type != x.Type) return false;
            if (Nodes_Count != x.Nodes_Count) return false;
            if (Data_Size != x.Data_Size) return false;
            if (Animations_Count != x.Animations_Count) return false;
            if (AltN_Count != x.AltN_Count) return false;

            return true;
        }

        public override bool Equals(object obj)
        {
            if (obj is DbModel x)
                return Equals(x);
            else
                return base.Equals(obj);
        }

        public override int GetHashCode() =>
            CombineHashCodes(base.GetHashCode(), 
                Type, Nodes_Count, Data_Size, Animations_Count, AltN_Count);
    }
}
