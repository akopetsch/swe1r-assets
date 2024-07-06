// SPDX-License-Identifier: MIT

using ByteSerialization.Nodes;
using SWE1R.Assets.Blocks.ModelBlock;
using SWE1R.Assets.Blocks.ModelBlock.F3DEX2;
using System.ComponentModel.DataAnnotations.Schema;

namespace SWE1R.Assets.Blocks.Original.SQLite.Entities.ModelBlock.F3DEX2
{
    [Table($"{nameof(Model)}_{nameof(GspVertexCommand)}")]
    public class DbGspVertexCommand : DbBlockItemStructure<GspVertexCommand>
    {
        #region Properties

        public int I { get; set; }
        public int N { get; set; }
        public int V0 { get; set; }

        #endregion

        #region Methods

        public override void CopyFrom(Node node)
        {
            base.CopyFrom(node);

            var x = (GspVertexCommand)node.Value;

            I = x.I;
            N = x.N;
            V0 = x.V0;
        }

        public override bool Equals(DbBlockItemStructure<GspVertexCommand> other)
        {
            var x = (DbGspVertexCommand)other;

            if (!base.Equals(x))
                return false;

            if (I != x.I) return false;
            if (N != x.N) return false;
            if (V0 != x.V0) return false;

            return true;
        }

        public override bool Equals(object obj)
        {
            if (obj is DbGspVertexCommand x)
                return Equals(x);
            else
                return base.Equals(obj);
        }

        public override int GetHashCode() =>
            CombineHashCodes(base.GetHashCode(),
                I, N, V0);

        #endregion
    }
}
