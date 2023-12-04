// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SWE1R.Assets.Blocks.ModelBlock.Meshes.VertexIndices
{
    public class IndicesRange
    {
        #region Properties

        public IndicesChunk01 Chunk01 { get; set; }
        public IndicesChunk03 Chunk03 { get; set; }
        public List<IndicesChunk> Chunks0506 { get; } = new List<IndicesChunk>();

        #endregion

        #region Properties (helper)

        public int NextIndicesBase =>
            Indices.Any() ? (Indices.Max() + 2) : 2;
            // (+)2 because value is double the actual index 
            // TODO: move comment, do not hardcode number 2

        public IEnumerable<int> Indices => 
            Chunks0506.SelectMany(c => c.Indices);

        public IEnumerable<IndicesChunk> AllChunks
        {
            get
            {
                if (Chunk01 != null)
                    yield return Chunk01; 
                if (Chunk03 != null)
                    yield return Chunk03;
                foreach (IndicesChunk chunk0506 in Chunks0506)
                    yield return chunk0506;
            }
        }

        #endregion

        #region Constructor

        public IndicesRange()
        {

        }

        #endregion

        #region Methods

        public override string ToString()
        {
            var sb = new StringBuilder();
            if (Chunk01 != null)
                sb.Append("1");
            if (Chunk03 != null)
                sb.Append("3");
            sb.Append(' ');
            sb.Append(string.Join(string.Empty, Chunks0506.Select(x => x.Tag)));
            return sb.ToString();
        }

        #endregion
    }
}
