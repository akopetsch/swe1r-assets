// SPDX-License-Identifier: MIT

using SWE1R.Assets.Blocks.ModelBlock.N64Sdk.GraphicsCommands;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SWE1R.Assets.Blocks.ModelBlock.Meshes
{
    public class VertexBuffer
    {
        #region Properties

        public GSpVertexCommand VertexCommand { get; set; }
        public GSpCullDisplayListCommand CullDisplayListCommand { get; set; }
        public List<ITrianglesGraphicsCommand> TrianglesCommands { get; } =
            new List<ITrianglesGraphicsCommand>();

        #endregion

        #region Properties (helper)

        public int NextIndicesBase =>
            Indices.Any() ? Indices.Max() + 1 : 1;

        public IEnumerable<byte> Indices =>
            TrianglesCommands.SelectMany(c => c.Indices);

        public IEnumerable<GraphicsCommand> AllCommands
        {
            get
            {
                if (VertexCommand != null)
                    yield return VertexCommand;
                if (CullDisplayListCommand != null)
                    yield return CullDisplayListCommand;
                foreach (GraphicsCommand triangleCommand in TrianglesCommands)
                    yield return triangleCommand;
            }
        }

        #endregion

        #region Methods (: object)

        public override string ToString()
        {
            var sb = new StringBuilder();
            if (VertexCommand != null)
                sb.Append(GraphicsCommandByte.G_VTX);
            if (CullDisplayListCommand != null)
                sb.Append(GraphicsCommandByte.G_CULLDL);
            sb.Append(' ');
            sb.Append(string.Join(string.Empty, TrianglesCommands.Select(x => (byte)x.Byte)));
            return sb.ToString();
        }

        #endregion
    }
}
