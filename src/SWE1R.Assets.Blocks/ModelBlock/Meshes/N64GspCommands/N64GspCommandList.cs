// SPDX-License-Identifier: MIT

using ByteSerialization.Attributes;
using SWE1R.Assets.Blocks.ModelBlock.Meshes.Geometry;
using System.Collections;
using System.Collections.Generic;

namespace SWE1R.Assets.Blocks.ModelBlock.Meshes.VertexIndices
{
    [Alignment(8)]
    public class N64GspCommandList : IList<N64GspCommand>
    {
        #region Properties (serialized)

        [SerializeUntil(0xDF00000000000000)]
        [Order(0)] private List<N64GspCommand> List { get; set; }

        #endregion

        #region Constructor

        public N64GspCommandList() =>
            List = new List<N64GspCommand>();

        public N64GspCommandList(List<N64GspCommand> list) =>
            List = list;

        #endregion

        #region Members (: IList<N64GspCommand>)

        public int Count => List.Count;
        public bool IsReadOnly => false;

        public N64GspCommand this[int index]
        {
            get => List[index];
            set => List[index] = value;
        }

        public int IndexOf(N64GspCommand item) => List.IndexOf(item);
        public void Insert(int index, N64GspCommand item) => List.Insert(index, item);
        public void RemoveAt(int index) => List.RemoveAt(index);
        public void Add(N64GspCommand item) => List.Add(item);
        public void Clear() => List.Clear();
        public bool Contains(N64GspCommand item) => List.Contains(item);
        public void CopyTo(N64GspCommand[] array, int arrayIndex) => List.CopyTo(array, arrayIndex);
        public bool Remove(N64GspCommand item) => List.Remove(item);
        public IEnumerator<N64GspCommand> GetEnumerator() => List.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => List.GetEnumerator();

        #endregion

        #region Methods (helper)

        public void AddRange(IEnumerable<N64GspCommand> commands) =>
            List.AddRange(commands);

        #endregion

        #region Methods (export)

        public List<Triangle> GetTriangles() // TODO: move to N64GspVertexBuffer
        {
            var triangles = new List<Triangle>();
            int baseIndex = 0;
            int stepIndex = 0;
            foreach (N64GspCommand command in List)
            {
                var triangleCommands = new List<Triangle>();
                if (command is N64GspVertexCommand vertexCommand)
                {
                    stepIndex = vertexCommand.V0PlusN / 2;
                    baseIndex += stepIndex;
                }
                else if (command is N64Gsp1TriangleCommand triangleCommand)
                {
                    triangleCommands.Add(triangleCommand.Triangle);
                }
                else if (command is N64Gsp2TrianglesCommand trianglesCommand)
                {
                    triangleCommands.Add(trianglesCommand.Triangle0);
                    triangleCommands.Add(trianglesCommand.Triangle1);
                }
                triangleCommands.ForEach(x => x.DivideIndicesBy(2));
                triangleCommands.ForEach(x => x.AddToIndices(baseIndex - stepIndex));
                triangles.AddRange(triangleCommands);
            }
            return triangles;
        }

        #endregion
    }
}
