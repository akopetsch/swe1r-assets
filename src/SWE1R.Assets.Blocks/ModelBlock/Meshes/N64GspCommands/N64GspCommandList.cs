﻿// SPDX-License-Identifier: MIT

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

        #region Members (: IList<IndicesChunk>)

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

        public void AddRange(IEnumerable<N64GspCommand> indicesChunks) =>
            List.AddRange(indicesChunks);

        #endregion

        #region Methods (export)

        public List<Triangle> GetTriangles() // TODO: move to IndicesRange
        {
            var triangles = new List<Triangle>();
            int baseIndex = 0;
            int stepIndex = 0;
            foreach (N64GspCommand gpsCommand in List)
            {
                var commandTriangles = new List<Triangle>();
                if (gpsCommand is N64GspVertexCommand chunk01)
                {
                    stepIndex = chunk01.NextIndicesBase / 2;
                    baseIndex += stepIndex;
                }
                else if (gpsCommand is N64Gsp1TriangleCommand gsp1TriangleCommand)
                {
                    commandTriangles.Add(gsp1TriangleCommand.Triangle);
                }
                else if (gpsCommand is N64Gsp2TrianglesCommand gsp2TrianglesCommand)
                {
                    commandTriangles.Add(gsp2TrianglesCommand.Triangle0);
                    commandTriangles.Add(gsp2TrianglesCommand.Triangle1);
                }
                commandTriangles.ForEach(x => x.DivideIndicesBy(2));
                commandTriangles.ForEach(x => x.AddToIndices(baseIndex - stepIndex));
                triangles.AddRange(commandTriangles);
            }
            return triangles;
        }

        #endregion
    }
}
