﻿// SPDX-License-Identifier: MIT

using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SWE1R.Assets.Blocks.ModelBlock.Meshes.N64GspCommands
{
    public class N64GspVertexBuffer
    {
        #region Properties

        public N64GspVertexCommand VertexCommand { get; set; }
        public N64GspCullDisplayListCommand CullDisplayListCommand { get; set; }
        public List<IN64GspTrianglesCommand> TrianglesCommands { get; } =
            new List<IN64GspTrianglesCommand>();

        #endregion

        #region Properties (helper)

        public int NextIndicesBase =>
            Indices.Any() ? (Indices.Max() + 1) : 1;

        public IEnumerable<byte> Indices => 
            TrianglesCommands.SelectMany(c => c.Indices);

        public IEnumerable<N64GspCommand> AllCommands
        {
            get
            {
                if (VertexCommand != null)
                    yield return VertexCommand; 
                if (CullDisplayListCommand != null)
                    yield return CullDisplayListCommand;
                foreach (N64GspCommand triangleCommand in TrianglesCommands)
                    yield return triangleCommand;
            }
        }

        #endregion

        #region Methods (: object)

        public override string ToString()
        {
            var sb = new StringBuilder();
            if (VertexCommand != null)
                sb.Append("1");
            if (CullDisplayListCommand != null)
                sb.Append("3");
            sb.Append(' ');
            sb.Append(string.Join(string.Empty, TrianglesCommands.Select(x => x.Byte)));
            return sb.ToString();
        }

        #endregion
    }
}