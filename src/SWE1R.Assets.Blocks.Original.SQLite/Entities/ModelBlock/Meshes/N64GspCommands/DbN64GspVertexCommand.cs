﻿// SPDX-License-Identifier: MIT

using ByteSerialization.Nodes;
using SWE1R.Assets.Blocks.ModelBlock;
using SWE1R.Assets.Blocks.ModelBlock.Meshes.N64GspCommands;
using System.ComponentModel.DataAnnotations.Schema;

namespace SWE1R.Assets.Blocks.Original.SQLite.Entities.ModelBlock.Meshes.N64GspCommands
{
    [Table($"{nameof(Model)}_{nameof(N64GspVertexCommand)}")]
    public class DbN64GspVertexCommand : DbBlockItemStructure<N64GspVertexCommand>
    {
        #region Properties

        public int P_V { get; set; }
        public int N { get; set; }
        public int V0 { get; set; }
        public byte V0PlusN { get; set; }

        #endregion

        public override void CopyFrom(Node node)
        {
            base.CopyFrom(node);

            var x = (N64GspVertexCommand)node.Value;

            P_V = GetValuePosition(node.Graph, x.V.Value);
            N = x.N;
            V0 = x.V0;
            V0PlusN = x.V0PlusN;
        }

        public override bool Equals(DbBlockItemStructure<N64GspVertexCommand> other)
        {
            var x = (DbN64GspVertexCommand)other;

            if (!base.Equals(x))
                return false;

            if (P_V != x.P_V) return false;
            if (N != x.N) return false;
            if (V0 != x.V0) return false;
            if (V0PlusN != x.V0PlusN) return false;

            return true;
        }

        public override bool Equals(object obj)
        {
            if (obj is DbN64GspVertexCommand x)
                return Equals(x);
            else
                return base.Equals(obj);
        }

        public override int GetHashCode() =>
            CombineHashCodes(base.GetHashCode(),
                P_V, N, V0, V0PlusN);
    }
}
