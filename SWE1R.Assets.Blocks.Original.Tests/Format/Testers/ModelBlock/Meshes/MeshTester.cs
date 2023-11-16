// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization.Nodes;
using SWE1R.Assets.Blocks.ModelBlock.Meshes;
using SWE1R.Assets.Blocks.ModelBlock.Meshes.VertexIndices;
using SWE1R.Assets.Blocks.Original.Tests.Format.Testers.ModelBlock.Meshes.Tmp;
using System.Diagnostics;
using System.Linq;

namespace SWE1R.Assets.Blocks.Original.Tests.Format.Testers.ModelBlock.Meshes
{
    public class MeshTester : Tester<Mesh>
    {
        public MeshTester(
            Mesh value, Graph byteSerializationGraph, AnalyticsFixture analyticsFixture) : 
            base(value, byteSerializationGraph, analyticsFixture)
        { }

        public override void Test()
        {
            TestVisibleIndicesChunks();

            IndicesChunks chunks = Value.VisibleIndicesChunks;
            if (chunks != null)
            {
                var ranges = GetRanges(chunks);
                Assert.True(ranges.Count >= 1);
                Assert.True(ranges.Count <= 50);

                // assert ascending StartVertex
                var ranges01 = ranges.Where(r => r.Chunk01 != null).ToList();
                for (int i = 0; i < ranges01.Count; i++)
                {
                    IndicesRange range01 = ranges01[i];
                    if (i > 0)
                    {
                        IndicesRange range01Before = ranges01[i - 1];
                        int startVertex = range01.Chunk01.StartVertex.Index.Value;
                        int startVertexBefore = range01Before.Chunk01.StartVertex.Index.Value;
                        Assert.True(startVertex > startVertexBefore);
                    }
                }

                for (int i = 0; i < ranges.Count; i++)
                {
                    IndicesRange range = ranges[i];

                    // chunks
                    Assert.True(range.Chunks.Count >= 1);
                    Assert.True(range.Chunks.Count <= 20);

                    // indices
                    Assert.True(range.Indices.Count() <= 117);
                    Assert.True(range.Indices.Min() == 0);
                    Assert.True(range.Indices.Max() >= 1);
                    Assert.True(range.Indices.All(x => x % 2 == 0));

                    IndicesChunk03 chunk03 = range.Chunk03;
                    if (chunk03 != null)
                    {
                        Assert.True(chunk03.Index == range.Indices.Max());
                        Assert.True(chunk03.Index != 0);
                    }

                    IndicesChunk01 chunk01 = range.Chunk01;
                    if (chunk01 != null)
                    {
                        // MaxIndex
                        Assert.True(chunk01.MaxIndex == range.NextIndicesBase);

                        // TODO: Length
                        Assert.True(chunk01.Length != 0);
                        int computedLength1 =
                            range.Indices.Distinct().Count() * 16;
                        int computedLength2 = 
                            range.Indices.Distinct().Count() * 16 - 
                            range.Chunk01.StartVertex.Index.Value * 16;
                        
                        Assert.True(
                            chunk01.Length == computedLength1 ||
                            chunk01.Length == computedLength2);

                        // TODO: StartVertex
                        int startVertexIndex = chunk01.StartVertex.Index.Value;
                        Assert.True(startVertexIndex != -1);
                        if (i != 0)
                        {
                            Assert.True(startVertexIndex == 
                                ranges.Take(i).Select(r => r.NextIndicesBase / 2).Sum());
                        }

                        if (startVertexIndex == 0)
                        {
                            if (i == 0)
                            {
                                // 17025
                            }
                            else
                            {
                                // 0
                            }
                        }
                        else
                        {
                            if (i == 0)
                            {
                                // 435
                                // models: 106...
                                Assert.True(range.Chunk03 == null);
                                Assert.True(ranges.Count == 1);
                                long meshPosition = ByteSerializationGraph.GetValueComponent(Value).Position.Value;
                            }
                            else
                            {
                                // 11903
                                // Chunk03 can be null
                                Assert.True(ranges.Count > 1);
                            }
                        }
                    }
                    
                    if (i > 0)
                    {

                    }

                    if (range.Chunk01 != null)
                    {
                        if (i == 0)
                        {
                            // only true 397/433 models:
                            // Assert.True(range.Chunk01.StartVertex.Index == 0);
                        }

                        //Assert.True(range.Chunk01.StartVertex.Index == range.MinIndex);

                        //Assert.True(range.Chunk01.Length == range.Indices.Count() * 16);

                        // length
                        //Assert.Equal(range.ComputedLength, range.Chunk01.Length);
                        //Debug.WriteLine(
                        //    $"{i:d3} | " +
                        //    $"{range.Chunk01.Length:d4} | " +
                        //    $"{range}");
                    }
                }
            }
        }

        private List<IndicesRange> GetRanges(IndicesChunks chunks)
        {
            var ranges = new List<IndicesRange>();
            IndicesRange range = null;
            for (int i = 0; i < chunks.Count; i++)
            {
                IndicesChunk chunk = chunks[i];
                byte tag = chunk.Tag;

                // if first range or new range
                if (range == null || tag == 01)
                {
                    // create/add new range
                    range = new IndicesRange();
                    ranges.Add(range);
                    if (tag == 1)
                        range.Chunk01 = (IndicesChunk01)chunk;
                }
                else if (tag == 3)
                    range.Chunk03 = (IndicesChunk03)chunk;

                if (tag == 5 || tag == 6)
                    range.Chunks.Add(chunk);
            }
            return ranges;
        }

        private void TestVisibleIndicesChunks()
        {
            IndicesChunks chunks = Value.VisibleIndicesChunks;
            if (chunks != null)
            {
                int n = chunks.Count;
                Assert.True(n <= 590);

                IndicesChunk chunk0 = chunks[0];
                if (n == 1)
                {
                    Assert.True(chunk0.Tag == 05 || chunk0.Tag == 06);
                }
                else
                {
                    if (chunk0.Tag == 06)
                    {
                        Assert.Equal(6, n);
                        Assert.True(chunks.All(x => x.Tag == 06));
                    }
                    else
                    {
                        // n != 1 && n != 6
                        // most of the time (in 429 of 433)

                        Assert.True(chunk0.Tag == 01);
                        if (n == 2)
                        {
                            IndicesChunk chunk1 = chunks[1];
                            Assert.True(chunk1.Tag == 05 || chunk1.Tag == 06);
                        }
                        else
                        {
                            for (int i = 0; i < n; i++)
                            {
                                IndicesChunk chunk = chunks[i];
                                if (chunk.Tag == 03)
                                {
                                    IndicesChunk chunkBefore = chunks[i - 1];
                                    Assert.True(chunkBefore.Tag == 01);
                                }
                            }
                        }
                    }
                }
            }
        }
        
        private void WriteChunksString() =>
            Debug.WriteLine(GetChunksString());

        private string GetChunksString() =>
            string.Join(' ', Value.VisibleIndicesChunks.Select(x => x.Tag.ToString("d2")));
    }
}
