﻿// SPDX-License-Identifier: MIT

using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SWE1R.Assets.Blocks.ModelBlock.Meshes.VertexIndices
{
    public class N64GspVertexBuffer
    {
        #region Properties

        public N64GspVertexCommand VertexCommand { get; set; }
        public N64GspCullDisplayListCommand CullDisplayListCommand { get; set; }
        public List<N64GspCommand> TriangleCommands { get; } = new List<N64GspCommand>();

        #endregion

        #region Properties (helper)

        public int NextIndicesBase =>
            Indices.Any() ? (Indices.Max() + 2) : 2;
            // (+)2 because value is double the actual index 
            // TODO: move comment, do not hardcode number 2

        public IEnumerable<int> Indices => 
            TriangleCommands.SelectMany(c => c.Indices);

        public IEnumerable<N64GspCommand> AllCommands
        {
            get
            {
                if (VertexCommand != null)
                    yield return VertexCommand; 
                if (CullDisplayListCommand != null)
                    yield return CullDisplayListCommand;
                foreach (N64GspCommand triangleCommand in TriangleCommands)
                    yield return triangleCommand;
            }
        }

        #endregion

        #region Constructor

        public N64GspVertexBuffer()
        {

        }

        #endregion

        #region Methods

        public override string ToString()
        {
            var sb = new StringBuilder();
            if (VertexCommand != null)
                sb.Append("1");
            if (CullDisplayListCommand != null)
                sb.Append("3");
            sb.Append(' ');
            sb.Append(string.Join(string.Empty, TriangleCommands.Select(x => x.Byte)));
            return sb.ToString();
        }

        #endregion
    }
}
