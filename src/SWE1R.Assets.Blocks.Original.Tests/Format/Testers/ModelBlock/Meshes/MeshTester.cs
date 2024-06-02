// SPDX-License-Identifier: MIT

using SWE1R.Assets.Blocks.ModelBlock;
using SWE1R.Assets.Blocks.ModelBlock.Meshes;
using SWE1R.Assets.Blocks.ModelBlock.Meshes.N64GspCommands;
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
            TestDisplayList();
        }

        #region Methods (bounds)

        private void AssertBounds()
        {
            var visibleVertices = Value.Vertices?.Select(x => (Vector3Single)x.Position).ToArray() ?? [];
            var collisionVerticesInt16 = Value.CollisionVertices?.ShortVectors?.Select(x => (Vector3Single)x).ToArray() ?? [];
            var collisionVerticesSingle = Value.CollisionVertices?.FloatVectors?.ToArray() ?? [];
            var allVectors = visibleVertices.Concat(collisionVerticesSingle).Concat(collisionVerticesInt16).ToArray();
            var computedBounds = new Bounds3Single(allVectors);

            //Assert.True(new Bounds3Single(Value.Bounds0, Value.Bounds1).IsValid); // sometimes fails
            //Assert.True(Value.FixedBounds.Equals(computedBounds)); // sometimes fails
            //Assert.True(computedBounds.Contains(Value.FixedBounds)); // sometimes fails
            //Assert.True(Value.FixedBounds.Contains(computedBounds)); // sometimes fails
        }

        #endregion

        #region Methods (DisplayList)

        private void TestDisplayList()
        {
            TestCommandList0();

            var model = (Model)ByteSerializerGraph.Root.Value;

            if (Value.CommandList != null)
            {
                var vertexBuffers = GetVertexBuffers(Value.CommandList);
                Assert.True(vertexBuffers.Count >= 1);
                Assert.True(vertexBuffers.Count <= 50);

                //var list1 = Value.CommandList.ToList();
                //var list2 = vertexBuffers.SelectMany(r => r.AllCommands).ToList();
                //bool foo = Enumerable.SequenceEqual(list1, list2);
                //Assert.True(foo); // commented-out for perf reasons

                if (vertexBuffers.Count > 1)
                    Assert.True(vertexBuffers.All(r => r.VertexCommand != null));

                // assert ascending StartVertex
                var vertexBuffersWithVertexCommands = vertexBuffers.Where(r => r.VertexCommand != null).ToList();
                for (int i = 0; i < vertexBuffersWithVertexCommands.Count; i++)
                {
                    N64GspVertexBuffer vertexBuffer = vertexBuffersWithVertexCommands[i];
                    if (i > 0)
                    {
                        N64GspVertexBuffer vertexBufferBefore = vertexBuffersWithVertexCommands[i - 1];
                        int startVertex = vertexBuffer.VertexCommand.StartVertex.Index.Value;
                        int startVertexBefore = vertexBufferBefore.VertexCommand.StartVertex.Index.Value;
                        Assert.True(startVertex > startVertexBefore);
                    }
                }

                for (int i = 0; i < vertexBuffers.Count; i++)
                {
                    N64GspVertexBuffer vertexBuffer = vertexBuffers[i];
                    //AnalyticsFixture.IncreaseCounter(vertexBuffer.ToString());

                    // triangle commands
                    Assert.True(vertexBuffer.TrianglesCommands.Count >= 1);
                    Assert.True(vertexBuffer.TrianglesCommands.Count <= 20);

                    // indices
                    Assert.True(vertexBuffer.Indices.Count() <= 117);
                    Assert.True(vertexBuffer.Indices.Min() == 0);
                    Assert.True(vertexBuffer.Indices.Max() >= 1);
                    Assert.True(vertexBuffer.Indices.All(x => x % 2 == 0));

                    N64GspCullDisplayListCommand cullDisplayListCommand = vertexBuffer.CullDisplayListCommand;
                    if (cullDisplayListCommand != null)
                    {
                        Assert.True(cullDisplayListCommand.VN == vertexBuffer.Indices.Max());
                        Assert.True(cullDisplayListCommand.VN != 0);

                        bool isLast = i == vertexBuffers.Count - 1;
                        Assert.True(isLast);
                        // but the last range does not necessarliy have N64GspCullDisplayListCommand
                    }

                    N64GspVertexCommand vertexCommand = vertexBuffer.VertexCommand;
                    if (vertexCommand != null)
                    {
                        int startVertexIndex = vertexCommand.StartVertex.Index.Value;
                        int distinctIndicesCount = vertexBuffer.Indices.Distinct().Count();

                        // VerticesCount
                        Assert.True(vertexCommand.VerticesCount != 0);
                        if (i != 0)
                            Assert.True(vertexCommand.VerticesCount == distinctIndicesCount);
                        else
                            Assert.True(vertexCommand.VerticesCount == distinctIndicesCount - startVertexIndex);
                        Assert.True(vertexCommand.VerticesCountPadded >= (1 << N64GspVertexCommand.VerticesCountPadding));

                        // NextIndicesBase
                        Assert.True(vertexCommand.NextIndicesBase == vertexBuffer.NextIndicesBase);

                        // TODO: StartVertex
                        Assert.True(vertexCommand.StartVertex.Collection == Value.Vertices);
                        Assert.True(vertexCommand.StartVertex.Index.Value != -1);

                        if (i != 0)
                        {
                            Assert.True(vertexBuffers.Count > 1);
                            Assert.True(startVertexIndex ==
                                vertexBuffers.Take(i).Select(r => r.NextIndicesBase / 2).Sum());
                            // N64GspCullDisplayListCommand can be null
                        }
                        else // i == 0
                        {
                            // rarely failing:
                            //Assert.True(startVertexIndex == vertexBuffer.TriangleCommands.First().Triangles.First().Indices.Min());
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
                            Assert.True(vertexBuffer.CullDisplayListCommand == null);
                            Assert.True(vertexBuffers.Count == 1);

                        }
                        else
                        {
                            // 11903
                        }
                    }

                    if (i > 0)
                    {

                    }

                    if (vertexBuffer.VertexCommand != null)
                        if (i == 0)
                        {
                            // only true 397/433 models:
                            // Assert.True(range.VertexCommand.StartVertex.Index == 0);
                        }
                }
            }
        }

        private List<N64GspVertexBuffer> GetVertexBuffers(N64GspCommandList commands)
        {
            // TODO: should be moved to SWE1R.Assets.Blocks

            var ranges = new List<N64GspVertexBuffer>();
            N64GspVertexBuffer vertexBuffer = null;
            for (int i = 0; i < commands.Count; i++)
            {
                N64GspCommand command = commands[i];

                // if first range or new range
                if (vertexBuffer == null || command.Byte == 01)
                {
                    // create/add new range
                    vertexBuffer = new N64GspVertexBuffer();
                    ranges.Add(vertexBuffer);
                    if (command.Byte == 1)
                        vertexBuffer.VertexCommand = (N64GspVertexCommand)command;
                }
                else if (command.Byte == 3)
                    vertexBuffer.CullDisplayListCommand = (N64GspCullDisplayListCommand)command;

                if (command is IN64GspTrianglesCommand trianglesCommand)
                    vertexBuffer.TrianglesCommands.Add(trianglesCommand);
            }
            return ranges;
        }

        private void TestCommandList0()
        {
            N64GspCommandList commandList = Value.CommandList;
            if (commandList != null)
            {
                int n = commandList.Count;
                Assert.True(n <= 590);

                N64GspCommand firstCommand = commandList[0];
                if (n == 1)
                    Assert.True(
                        firstCommand.Byte == 05 || 
                        firstCommand.Byte == 06);
                else
                    if (firstCommand.Byte == 06)
                {
                    Assert.Equal(6, n);
                    Assert.True(commandList.All(x => x.Byte == 06));
                }
                else
                {
                    // n != 1
                    // most of the time (in 429 of 433 models)

                    Assert.True(firstCommand.Byte == 01);
                    if (n == 2)
                    {
                        N64GspCommand secondCommand = commandList[1];
                        Assert.True(
                            secondCommand.Byte == 05 || 
                            secondCommand.Byte == 06);
                    }
                    else
                        for (int i = 0; i < n; i++)
                        {
                            N64GspCommand command = commandList[i];
                            if (command.Byte == 03)
                            {
                                N64GspCommand commandBefore = commandList[i - 1];
                                Assert.True(commandBefore.Byte == 01);
                            }
                        }
                }
            }
        }

        private void WriteCommandsString() =>
            Debug.WriteLine(GetCommandsString());

        private string GetCommandsString() =>
            string.Join(' ', Value.CommandList.Select(x => x.Byte.ToString("d2")));

        #endregion
    }
}
