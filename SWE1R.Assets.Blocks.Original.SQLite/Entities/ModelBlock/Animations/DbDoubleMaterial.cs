// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization.Nodes;
using SWE1R.Assets.Blocks.ModelBlock.Animations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SWE1R.Assets.Blocks.Original.SQLite.Entities.ModelBlock.Anims
{
    [Table("Model_DoubleMaterial")]
    public class DbDoubleMaterial : DbModelStructure<DoubleMaterial>
    {
        public int P_Foo1 { get; set; }
        public int P_Foo2 { get; set; }

        public override void CopyFrom(Node node)
        {
            base.CopyFrom(node);

            var dm = (DoubleMaterial)node.Value;

            P_Foo1 = GetPropertyPointer(node, nameof(dm.Foo1));
            P_Foo2 = GetPropertyPointer(node, nameof(dm.Foo2));
        }

        public override bool Equals(DbModelStructure<DoubleMaterial> other)
        {
            var _other = (DbDoubleMaterial)other;

            if (!base.Equals(_other))
                return false;

            if (P_Foo1 != _other.P_Foo1) return false;
            if (P_Foo2 != _other.P_Foo2) return false;

            return true;
        }

        public override bool Equals(object obj)
        {
            if (obj is DbDoubleMaterial)
                return this.Equals((DbDoubleMaterial)obj);
            else
                return base.Equals(obj);
        }

        public override int GetHashCode() =>
            HashCode.Combine(base.GetHashCode(), P_Foo1, P_Foo2);
    }
}
