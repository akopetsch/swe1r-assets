// SPDX-License-Identifier: MIT

using SWE1R.Assets.Blocks.ModelBlock;
using SWE1R.Assets.Blocks.ModelBlock.Meshes;
using SWE1R.Assets.Blocks.ModelBlock.Meshes.VertexIndices;
using SWE1R.Assets.Blocks.Vectors;
using System.Diagnostics;

namespace SWE1R.Assets.Blocks.Original.Tests.Format.Testers.ModelBlock.Meshes
{
    public class MeshTester : Tester<Mesh>
    {
        // TODO: clean-up

        public override void Test()
        {
            AssertBounds();
            TestIndices();
        }

        #region Methods (bounds)

        private void AssertBounds()
        {
            var visibleVertices = Value.VisibleVertices?.Select(x => (Vector3Single)x.Position).ToArray() ?? new Vector3Single[] { };
            var collisionVerticesInt16 = Value.CollisionVertices?.ShortVectors?.Select(x => (Vector3Single)x).ToArray() ?? new Vector3Single[] { };
            var collisionVerticesSingle = Value.CollisionVertices?.FloatVectors?.ToArray() ?? new Vector3Single[] { };
            var allVectors = visibleVertices.Concat(collisionVerticesSingle).Concat(collisionVerticesInt16).ToArray();
            var computedBounds = new Bounds3Single(allVectors);

            //Assert.True(new Bounds3Single(Value.Bounds0, Value.Bounds1).IsValid); // sometimes fails
            //Assert.True(Value.FixedBounds.Equals(computedBounds)); // sometimes fails
            //Assert.True(computedBounds.Contains(Value.FixedBounds)); // sometimes fails
            //Assert.True(Value.FixedBounds.Contains(computedBounds)); // sometimes fails
        }

        #endregion

        #region Methods (indices)

        private void TestIndices()
        {
            TestVisibleIndicesChunks();

            var model = (Model)ByteSerializerGraph.Root.Value;

            N64GspCommandList chunks = Value.VisibleIndicesChunks;
            if (chunks != null)
            {
                var ranges = GetRanges(chunks);
                Assert.True(ranges.Count >= 1);
                Assert.True(ranges.Count <= 50);

                //var list1 = chunks.ToList();
                //var list2 = ranges.SelectMany(r => r.AllChunks).ToList();
                //bool foo = Enumerable.SequenceEqual(list1, list2);
                //Assert.True(foo); // commented-out for perf reasons

                if (ranges.Count > 1)
                    Assert.True(ranges.All(r => r.GspVertexCommand != null));

                // assert ascending StartVertex
                var ranges01 = ranges.Where(r => r.GspVertexCommand != null).ToList();
                for (int i = 0; i < ranges01.Count; i++)
                {
                    N64GspVertexBuffer range01 = ranges01[i];
                    if (i > 0)
                    {
                        N64GspVertexBuffer range01Before = ranges01[i - 1];
                        int startVertex = range01.GspVertexCommand.StartVertex.Index.Value;
                        int startVertexBefore = range01Before.GspVertexCommand.StartVertex.Index.Value;
                        Assert.True(startVertex > startVertexBefore);
                    }
                }

                for (int i = 0; i < ranges.Count; i++)
                {
                    N64GspVertexBuffer range = ranges[i];
                    //AnalyticsFixture.IncreaseCounter(range.ToString());

                    // chunks
                    Assert.True(range.TriangleCommands.Count >= 1);
                    Assert.True(range.TriangleCommands.Count <= 20);

                    // indices
                    Assert.True(range.Indices.Count() <= 117);
                    Assert.True(range.Indices.Min() == 0);
                    Assert.True(range.Indices.Max() >= 1);
                    Assert.True(range.Indices.All(x => x % 2 == 0));

                    N64GspCullDisplayListCommand chunk03 = range.GspCullDisplayListCommand;
                    if (chunk03 != null)
                    {
                        Assert.True(chunk03.MaxIndex == range.Indices.Max());
                        Assert.True(chunk03.MaxIndex != 0);

                        bool isLast = i == ranges.Count - 1;
                        Assert.True(isLast);
                        // but the last range does not necessarliy have 03
                    }

                    N64GspVertexCommand chunk01 = range.GspVertexCommand;
                    if (chunk01 != null)
                    {
                        int startVertexIndex = chunk01.StartVertex.Index.Value;
                        int distinctIndicesCount = range.Indices.Distinct().Count();

                        // Length
                        Assert.True(chunk01.VerticesCount != 0);
                        if (i != 0)
                            Assert.True(chunk01.VerticesCount == distinctIndicesCount);
                        else
                            Assert.True(chunk01.VerticesCount == distinctIndicesCount - startVertexIndex);
                        Assert.True(chunk01.VerticesCountPadded >= (1 << N64GspVertexCommand.VerticesCountPadding));

                        // NextIndicesBase
                        Assert.True(chunk01.NextIndicesBase == range.NextIndicesBase);

                        // TODO: StartVertex
                        Assert.True(chunk01.StartVertex.Collection == Value.VisibleVertices);
                        Assert.True(chunk01.StartVertex.Index.Value != -1);

                        if (i != 0)
                        {
                            Assert.True(ranges.Count > 1);
                            Assert.True(startVertexIndex ==
                                ranges.Take(i).Select(r => r.NextIndicesBase / 2).Sum());
                            // Chunk03 can be null
                        }
                        else // i == 0
                        {
                            // rarely failing:
                            //Assert.True(startVertexIndex == range.Chunks0506.First().Triangles.First().Indices.Min());
                        }

                        if (startVertexIndex == 0)
                            if (i == 0)
                            {
                                // 17025
                            }
                            else
                            {
                                // 0
                            }
                        else
                            if (i == 0)
                        {
                            // 435
                            // models: 106...
                            Assert.True(range.GspCullDisplayListCommand == null);
                            Assert.True(ranges.Count == 1);

                        }
                        else
                        {
                            // 11903
                        }
                    }

                    if (i > 0)
                    {

                    }

                    if (range.GspVertexCommand != null)
                        if (i == 0)
                        {
                            // only true 397/433 models:
                            // Assert.True(range.Chunk01.StartVertex.Index == 0);
                        }
                }
            }
        }

        private List<N64GspVertexBuffer> GetRanges(N64GspCommandList chunks)
        {
            var ranges = new List<N64GspVertexBuffer>();
            N64GspVertexBuffer range = null;
            for (int i = 0; i < chunks.Count; i++)
            {
                N64GspCommand chunk = chunks[i];
                byte tag = chunk.Tag;

                // if first range or new range
                if (range == null || tag == 01)
                {
                    // create/add new range
                    range = new N64GspVertexBuffer();
                    ranges.Add(range);
                    if (tag == 1)
                        range.GspVertexCommand = (N64GspVertexCommand)chunk;
                }
                else if (tag == 3)
                    range.GspCullDisplayListCommand = (N64GspCullDisplayListCommand)chunk;

                if (tag == 5 || tag == 6)
                    range.TriangleCommands.Add(chunk);
            }
            return ranges;
        }

        private void TestVisibleIndicesChunks()
        {
            N64GspCommandList chunks = Value.VisibleIndicesChunks;
            if (chunks != null)
            {
                int n = chunks.Count;
                Assert.True(n <= 590);

                N64GspCommand chunk0 = chunks[0];
                if (n == 1)
                    Assert.True(chunk0.Tag == 05 || chunk0.Tag == 06);
                else
                    if (chunk0.Tag == 06)
                {
                    Assert.Equal(6, n);
                    Assert.True(chunks.All(x => x.Tag == 06));
                }
                else
                {
                    // n != 1
                    // most of the time (in 429 of 433 models)

                    Assert.True(chunk0.Tag == 01);
                    if (n == 2)
                    {
                        N64GspCommand chunk1 = chunks[1];
                        Assert.True(chunk1.Tag == 05 || chunk1.Tag == 06);
                    }
                    else
                        for (int i = 0; i < n; i++)
                        {
                            N64GspCommand chunk = chunks[i];
                            if (chunk.Tag == 03)
                            {
                                N64GspCommand chunkBefore = chunks[i - 1];
                                Assert.True(chunkBefore.Tag == 01);
                            }
                        }
                }
            }
        }

        private void WriteChunksString() =>
            Debug.WriteLine(GetChunksString());

        private string GetChunksString() =>
            string.Join(' ', Value.VisibleIndicesChunks.Select(x => x.Tag.ToString("d2")));

        #endregion
    }
}
