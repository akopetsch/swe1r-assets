// SPDX-License-Identifier: MIT

using SWE1R.Assets.Blocks.ModelBlock;
using SWE1R.Assets.Blocks.ModelBlock.F3DEX2;
using SWE1R.Assets.Blocks.ModelBlock.Meshes;
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
            TestCommandList();
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

        #region Methods (CommandList)

        private void TestCommandList()
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

                // assert ascending V0
                var vertexBuffersWithVertexCommands = vertexBuffers.Where(r => r.VertexCommand != null).ToList();
                for (int i = 0; i < vertexBuffersWithVertexCommands.Count; i++)
                {
                    VertexBuffer currentVertexBuffer = vertexBuffersWithVertexCommands[i];
                    if (i > 0)
                    {
                        VertexBuffer previousVertexBuffer = vertexBuffersWithVertexCommands[i - 1];
                        int currentV0 = currentVertexBuffer.VertexCommand.V0;
                        int previousV0 = previousVertexBuffer.VertexCommand.V0;
                        Assert.True(currentV0 > previousV0);
                    }
                }

                for (int i = 0; i < vertexBuffers.Count; i++)
                {
                    VertexBuffer vertexBuffer = vertexBuffers[i];
                    //AnalyticsFixture.IncreaseCounter(vertexBuffer.ToString());

                    // triangle commands
                    Assert.True(vertexBuffer.TrianglesCommands.Count >= 1);
                    Assert.True(vertexBuffer.TrianglesCommands.Count <= 20);

                    // indices
                    Assert.True(vertexBuffer.Indices.Count() <= 117);
                    Assert.True(vertexBuffer.Indices.Min() == 0);
                    Assert.True(vertexBuffer.Indices.Max() >= 1);

                    GSpCullDisplayListCommand cullDisplayListCommand = vertexBuffer.CullDisplayListCommand;
                    if (cullDisplayListCommand != null)
                    {
                        Assert.True(cullDisplayListCommand.VN == vertexBuffer.Indices.Max());
                        Assert.True(cullDisplayListCommand.VN != 0);

                        bool isLast = i == vertexBuffers.Count - 1;
                        Assert.True(isLast);
                        // but the last range does not necessarliy have N64GspCullDisplayListCommand
                    }

                    GSpVertexCommand vertexCommand = vertexBuffer.VertexCommand;
                    if (vertexCommand != null)
                    {
                        int v0 = vertexCommand.V0;
                        int distinctIndicesCount = vertexBuffer.Indices.Distinct().Count();

                        // N
                        Assert.True(vertexCommand.N != 0);
                        if (i != 0)
                            Assert.True(vertexCommand.N == distinctIndicesCount);
                        else
                            Assert.True(vertexCommand.N == distinctIndicesCount - v0);

                        // NextIndicesBase
                        Assert.True(vertexCommand.V0PlusN == vertexBuffer.NextIndicesBase);

                        // TODO: V0
                        Assert.True(vertexCommand.V.Collection == Value.Vertices);
                        Assert.True(vertexCommand.V.Index.Value != -1);

                        if (i != 0)
                        {
                            Assert.True(vertexBuffers.Count > 1);
                            Assert.True(v0 ==
                                vertexBuffers.Take(i).Select(r => r.NextIndicesBase).Sum());
                            // N64GspCullDisplayListCommand can be null
                        }
                        else // i == 0
                        {
                            // rarely failing:
                            //Assert.True(v0 == vertexBuffer.TriangleCommands.First().Triangles.First().Indices.Min());
                        }

                        if (v0 == 0)
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
                            // Assert.True(range.VertexCommand.V.Index == 0);
                        }
                }
            }
        }

        private List<VertexBuffer> GetVertexBuffers(GraphicsCommandList commands)
        {
            // TODO: should be moved to SWE1R.Assets.Blocks

            var ranges = new List<VertexBuffer>();
            VertexBuffer vertexBuffer = null;
            for (int i = 0; i < commands.Count; i++)
            {
                GraphicsCommand command = commands[i];

                // if first range or new range
                if (vertexBuffer == null || command is GSpVertexCommand)
                {
                    // create/add new range
                    vertexBuffer = new VertexBuffer();
                    ranges.Add(vertexBuffer);
                    if (command is GSpVertexCommand vertexCommand)
                        vertexBuffer.VertexCommand = vertexCommand;
                }
                else if (command is GSpCullDisplayListCommand cullDisplayListCommand)
                    vertexBuffer.CullDisplayListCommand = cullDisplayListCommand;

                if (command is ITrianglesGraphicsCommand trianglesCommand)
                    vertexBuffer.TrianglesCommands.Add(trianglesCommand);
            }
            return ranges;
        }

        private void TestCommandList0()
        {
            GraphicsCommandList commandList = Value.CommandList;
            if (commandList != null)
            {
                int n = commandList.Count;
                Assert.True(n <= 590);

                GraphicsCommand firstCommand = commandList[0];
                if (n == 1)
                    Assert.True(firstCommand is ITrianglesGraphicsCommand);
                else if (firstCommand is GSp2TrianglesCommand)
                {
                    Assert.Equal(6, n);
                    Assert.True(commandList.All(x => x is GSp2TrianglesCommand));
                }
                else
                {
                    // n != 1
                    // most of the time (in 429 of 433 models)

                    Assert.True(firstCommand is GSpVertexCommand);
                    if (n == 2)
                    {
                        GraphicsCommand secondCommand = commandList[1];
                        Assert.True(secondCommand is ITrianglesGraphicsCommand);
                    }
                    else
                        for (int i = 0; i < n; i++)
                        {
                            GraphicsCommand command = commandList[i];
                            if (command is GSpCullDisplayListCommand)
                            {
                                GraphicsCommand commandBefore = commandList[i - 1];
                                Assert.True(commandBefore is GSpVertexCommand);
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
