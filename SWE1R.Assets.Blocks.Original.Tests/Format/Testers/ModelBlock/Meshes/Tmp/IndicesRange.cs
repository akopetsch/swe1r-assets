// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using SWE1R.Assets.Blocks.ModelBlock.Meshes.VertexIndices;
using System.Text;

namespace SWE1R.Assets.Blocks.Original.Tests.Format.Testers.ModelBlock.Meshes.Tmp
{
    public class IndicesRange
    {
        public IndicesChunk01 Chunk01 { get; set; }
        public IndicesChunk03 Chunk03 { get; set; }
        public List<IndicesChunk> Chunks { get; } = new List<IndicesChunk>();

        public int ComputedLength // FIXME: ComputedLength is wrong
        {
            get
            {
                int length = 0;
                if (Chunk01 != null)
                    length++;
                if (Chunk03 != null)
                    length++;
                length += Chunks.Count;
                return length * 0x24; // 0x24 = 36
            }
        }

        public int ComputedLength2
        {
            get
            {
                int length = 0;
                foreach (var chunk in Chunks)
                {
                    if (chunk.Tag == 05)
                        length += 1;
                    if (chunk.Tag == 06)
                        length += 2;
                }
                return length;
            }
        }

        public int NextIndicesBase =>
            Indices.Max() + 2; // TODO: +2?

        public IEnumerable<int> Indices => Chunks.SelectMany(c => c.Indices);
        public int MinIndex => Indices.Min();
        public int MaxIndex => Indices.Max();

        public IndicesRange()
        {

        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            if (Chunk01 != null)
                sb.Append("1");
            if (Chunk03 != null)
                sb.Append("3");
            sb.Append(' ');
            sb.Append(string.Join(string.Empty, Chunks.Select(x => x.Tag)));
            return sb.ToString();
        }
    }
}
