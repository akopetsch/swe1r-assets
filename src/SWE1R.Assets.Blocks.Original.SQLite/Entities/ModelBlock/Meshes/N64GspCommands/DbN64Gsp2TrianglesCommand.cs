// SPDX-License-Identifier: MIT

using ByteSerialization.Nodes;
using SWE1R.Assets.Blocks.ModelBlock;
using SWE1R.Assets.Blocks.ModelBlock.Meshes.N64GspCommands;
using System.ComponentModel.DataAnnotations.Schema;

namespace SWE1R.Assets.Blocks.Original.SQLite.Entities.ModelBlock.Meshes.N64GspCommands
{
    [Table($"{nameof(Model)}_{nameof(N64Gsp2TrianglesCommand)}")]
    public class DbN64Gsp2TrianglesCommand : DbBlockItemStructure<N64Gsp2TrianglesCommand>
    {
        #region Properties

        public byte V00 { get; set; }
        public byte V01 { get; set; }
        public byte V02 { get; set; }
        public byte V10 { get; set; }
        public byte V11 { get; set; }
        public byte V12 { get; set; }

        #endregion

        public override void CopyFrom(Node node)
        {
            base.CopyFrom(node);

            var x = (N64Gsp2TrianglesCommand)node.Value;

            V00 = x.V00;
            V01 = x.V01;
            V02 = x.V02;

            V10 = x.V10;
            V11 = x.V11;
            V12 = x.V12;
        }

        public override bool Equals(DbBlockItemStructure<N64Gsp2TrianglesCommand> other)
        {
            var x = (DbN64Gsp2TrianglesCommand)other;

            if (!base.Equals(x))
                return false;

            if (V00 != x.V00) return false;
            if (V01 != x.V01) return false;
            if (V02 != x.V02) return false;

            if (V10 != x.V10) return false;
            if (V11 != x.V11) return false;
            if (V12 != x.V12) return false;

            return true;
        }

        public override bool Equals(object obj)
        {
            if (obj is DbN64Gsp2TrianglesCommand x)
                return Equals(x);
            else
                return base.Equals(obj);
        }

        public override int GetHashCode() =>
            CombineHashCodes(base.GetHashCode(), 
                V00, V01, V02, 
                V10, V11, V12);
    }
}
