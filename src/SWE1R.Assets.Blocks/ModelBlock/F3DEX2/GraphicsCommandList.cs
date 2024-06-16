// SPDX-License-Identifier: MIT

using ByteSerialization.Attributes;
using SWE1R.Assets.Blocks.ModelBlock.Meshes.Geometry;
using System.Collections;
using System.Collections.Generic;

namespace SWE1R.Assets.Blocks.ModelBlock.F3DEX2
{
    [Alignment(8)]
    public class GraphicsCommandList : IList<GraphicsCommand>
    {
        #region Properties (serialized)

        [SerializeUntil(0xDF00000000000000)]
        [Order(0)] private List<GraphicsCommand> List { get; set; }

        #endregion

        #region Constructor

        public GraphicsCommandList() =>
            List = new List<GraphicsCommand>();

        public GraphicsCommandList(List<GraphicsCommand> list) =>
            List = list;

        #endregion

        #region Members (: IList<N64GspCommand>)

        public int Count => List.Count;
        public bool IsReadOnly => false;

        public GraphicsCommand this[int index]
        {
            get => List[index];
            set => List[index] = value;
        }

        public int IndexOf(GraphicsCommand item) => List.IndexOf(item);
        public void Insert(int index, GraphicsCommand item) => List.Insert(index, item);
        public void RemoveAt(int index) => List.RemoveAt(index);
        public void Add(GraphicsCommand item) => List.Add(item);
        public void Clear() => List.Clear();
        public bool Contains(GraphicsCommand item) => List.Contains(item);
        public void CopyTo(GraphicsCommand[] array, int arrayIndex) => List.CopyTo(array, arrayIndex);
        public bool Remove(GraphicsCommand item) => List.Remove(item);
        public IEnumerator<GraphicsCommand> GetEnumerator() => List.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => List.GetEnumerator();

        #endregion

        #region Methods (helper)

        public void AddRange(IEnumerable<GraphicsCommand> commands) =>
            List.AddRange(commands);

        #endregion

        #region Methods (export)

        public List<Triangle> GetTriangles() // TODO: move to N64GspVertexBuffer
        {
            var triangles = new List<Triangle>();
            int baseIndex = 0;
            int stepIndex = 0;
            foreach (GraphicsCommand command in List)
            {
                var triangleCommands = new List<Triangle>();
                if (command is GSpVertexCommand vertexCommand)
                {
                    stepIndex = vertexCommand.V0PlusN;
                    baseIndex += stepIndex;
                }
                else if (command is GSp1TriangleCommand triangleCommand)
                {
                    triangleCommands.Add(triangleCommand.Triangle);
                }
                else if (command is GSp2TrianglesCommand trianglesCommand)
                {
                    triangleCommands.Add(trianglesCommand.Triangle0);
                    triangleCommands.Add(trianglesCommand.Triangle1);
                }
                triangleCommands.ForEach(x => x.AddToIndices(baseIndex - stepIndex));
                triangles.AddRange(triangleCommands);
            }
            return triangles;
        }

        #endregion
    }
}
