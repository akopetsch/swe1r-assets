// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using SWE1R.Assets.Blocks.ModelBlock;
using SWE1R.Assets.Blocks.ModelBlock.Meshes;
using SWE1R.Assets.Blocks.ModelBlock.Meshes.VertexIndices;
using SWE1R.Assets.Blocks.Original.Tests.Format.Testers;
using System.Diagnostics;

namespace SWE1R.Assets.Blocks.Original.Tests.Format.ModelBlock.Testers.Meshes
{
    public class MeshTester : Tester<Mesh>
    {
        // TODO: clean-up

        public override void Test()
        {
            TestIndices();
        }

        private void TestIndices()
        {
            TestVisibleIndicesChunks();

            var header = (Model)ByteSerializerGraph.Root.Value;

            IndicesChunks chunks = Value.VisibleIndicesChunks;
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
                    Assert.True(ranges.All(r => r.Chunk01 != null));

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
                    //AnalyticsFixture.IncreaseCounter(range.ToString());

                    // chunks
                    Assert.True(range.Chunks0506.Count >= 1);
                    Assert.True(range.Chunks0506.Count <= 20);

                    // indices
                    Assert.True(range.Indices.Count() <= 117);
                    Assert.True(range.Indices.Min() == 0);
                    Assert.True(range.Indices.Max() >= 1);
                    Assert.True(range.Indices.All(x => x % 2 == 0));

                    IndicesChunk03 chunk03 = range.Chunk03;
                    if (chunk03 != null)
                    {
                        Assert.True(chunk03.MaxIndex == range.Indices.Max());
                        Assert.True(chunk03.MaxIndex != 0);

                        bool isLast = i == ranges.Count - 1;
                        Assert.True(isLast);
                        // but the last range does not necessarliy have 03
                    }

                    IndicesChunk01 chunk01 = range.Chunk01;
                    if (chunk01 != null)
                    {
                        // NextIndicesBase
                        Assert.True(chunk01.NextIndicesBase == range.NextIndicesBase);

                        int startVertexIndex = chunk01.StartVertex.Index.Value;

                        // Length candidate:
                        int previousNextIndicesBase = 0;
                        if (i > 0)
                            previousNextIndicesBase = ranges[i - 1].NextIndicesBase / 2;
                        int computedLength =
                            range.Indices.Distinct().Count() * Vertex.StructureSize -
                            (startVertexIndex - previousNextIndicesBase) * Vertex.StructureSize;
                        //Assert.True(chunk01.Length == computedLength); // sometimes failing


                        // TODO: Length
                        Assert.True(chunk01.Length != 0);
                        int computedLength1 =
                            range.Indices.Distinct().Count() * Vertex.StructureSize;
                        int computedLength2 =
                            range.Indices.Distinct().Count() * Vertex.StructureSize -
                            range.Chunk01.StartVertex.Index.Value * Vertex.StructureSize;

                        bool true1 = chunk01.Length == computedLength1;
                        bool true2 = chunk01.Length == computedLength2;

                        if (true1 & !true2)
                        {
                            Assert.True(range.Chunk01.StartVertex.Index.Value != 0);

                            // if there was a 01 range before:
                            Assert.True(ranges.Count > 1);
                            Assert.True(i != 0);
                            Assert.True(ranges.All(r => r.Chunk01 != null)); // but chunk03 is null sometimes

                            var foo = ranges[i - 1].NextIndicesBase;
                        }
                        if (true2 && !true1)
                        {
                            Assert.True(range.Chunk01.StartVertex.Index.Value != 0);

                            // if this is a single (01) range:
                            Assert.True(ranges.Count == 1);
                            Assert.True(i == 0); // obviously

                            Assert.True(header.Animations != null);


                            // 086 - Pupp_Racer_Teemto_Pagalies | 7
                            // 087 - Pupp_Racer_Anakin_Skywalker | 1
                            // 089 - Pupp_Racer_Mawhonic | 21
                            // 090 - Pupp_Racer_Ody_Mandrell | 10
                            // 092 - Pupp_Racer_Mars_Guo | 7
                            // 093 - Pupp_Racer_Ratts_Tyerell | 12
                            // 094 - Pupp_Racer_Ben_Quadinaros | 12
                            // 095 - Pupp_Racer_Ebe_E_Endocott | 15
                            // 096 - Pupp_Racer_Ark_Roose | 14
                            // 097 - Pupp_Racer_Clegg_Holdfast | 3
                            // 098 - Pupp_Racer_Dud_Bolt | 8
                            // 099 - Pupp_Racer_Wan_Sandage | 10
                            // 100 - Pupp_Racer_Elan_Mak | 11
                            // 101 - Pupp_Racer_Toy_Dampner | 12
                            // 103 - Pupp_Racer_Neva_Kee | 4
                            // 104 - Pupp_Racer_Slide_Paramita | 5
                            // 105 - Pupp_Racer_Aldar_Beedo | 12
                            // 106 - Pupp_Racer_Bozzie_Baranta | 13
                            // 107 - Pupp_Racer_Boles_Roor | 27
                            // 108 - Pupp_Racer_Navior | 19
                            // 110 - Pupp_Watto | 1
                            // 111 - Pupp_Dewback | 12
                            // 113 - Pupp_Jabba | 35
                            // 149 - Scen_Jabba_Small__SCEN | 46
                            // 152 - Scen_Jabba_Mawhonic_Gasgano_Anakin_Sebulba_Anim126__SCEN | 51
                            // 155 - Pupp_OpeeSeaKiller | 14
                            // 289 - Scen_Jabba_Mawhonic_Gasgano_Anakin_Sebulba_Anim115__SCEN | 43
                            // 304 - Pupp_Racer_Jinn_Reeso | 5
                            // 305 - Pupp_Racer_Cy_Yunga | 5
                        }
                        if (true1 && true2)
                        {
                            Assert.True(range.Chunk01.StartVertex.Index.Value == 0);

                            // if this is the first (01) range and more follow:
                            Assert.True(i == 0);

                            var model = header.BlockItem;
                            //string key = $"{model.Index.Value:d3} - {_metadataProvider.GetName(model)}";
                            //AnalyticsFixture.IncreaseCounter("foo");
                        }

                        Assert.True(
                            chunk01.Length == computedLength1 ||
                            chunk01.Length == computedLength2);




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
                                Assert.True(range.Chunk03 == null);
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

                    if (range.Chunk01 != null)
                        if (i == 0)
                        {
                            // only true 397/433 models:
                            // Assert.True(range.Chunk01.StartVertex.Index == 0);
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
                    range.Chunks0506.Add(chunk);
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
                            IndicesChunk chunk1 = chunks[1];
                            Assert.True(chunk1.Tag == 05 || chunk1.Tag == 06);
                        }
                        else
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

        private void WriteChunksString() =>
            Debug.WriteLine(GetChunksString());

        private string GetChunksString() =>
            string.Join(' ', Value.VisibleIndicesChunks.Select(x => x.Tag.ToString("d2")));
    }
}
