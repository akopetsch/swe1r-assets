// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization.Attributes;
using SWE1R.Assets.Blocks.ModelBlock.Meshes.Geometry;
using System.Collections;
using System.Collections.Generic;

namespace SWE1R.Assets.Blocks.ModelBlock.Meshes.VertexIndices
{
    [Alignment(8)]
    public class IndicesChunks : IList<IndicesChunk>
    {
        #region Properties (serialization)

        [SerializeUntil(0xDF00000000000000)]
        [Order(0)] private List<IndicesChunk> List { get; set; }

        #endregion

        #region Constructor

        public IndicesChunks() { }

        public IndicesChunks(List<IndicesChunk> list) =>
            List = list;

        #endregion

        #region Members (: IList<IndicesChunk>)

        public int Count => List.Count;
        public bool IsReadOnly => false;

        public IndicesChunk this[int index]
        {
            get => List[index];
            set => List[index] = value;
        }

        public int IndexOf(IndicesChunk item) => List.IndexOf(item);
        public void Insert(int index, IndicesChunk item) => List.Insert(index, item);
        public void RemoveAt(int index) => List.RemoveAt(index);
        public void Add(IndicesChunk item) => List.Add(item);
        public void Clear() => List.Clear();
        public bool Contains(IndicesChunk item) => List.Contains(item);
        public void CopyTo(IndicesChunk[] array, int arrayIndex) => List.CopyTo(array, arrayIndex);
        public bool Remove(IndicesChunk item) => List.Remove(item);
        public IEnumerator<IndicesChunk> GetEnumerator() => List.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => List.GetEnumerator();

        #endregion

        #region Methods (export)

        public List<Triangle> GetTriangles()
        {
            var triangles = new List<Triangle>();
            int baseIndex = 0;
            int stepIndex = 0;
            foreach (IndicesChunk chunk in List)
            {
                var chunkTriangles = new List<Triangle>();
                if (chunk is IndicesChunk01 chunk01)
                {
                    stepIndex = chunk01.MaxIndex / 2;
                    baseIndex += stepIndex;
                }
                else if (chunk is IndicesChunk05 chunk05)
                {
                    chunkTriangles.Add(chunk05.Triangle);
                }
                else if (chunk is IndicesChunk06 chunk06)
                {
                    chunkTriangles.Add(chunk06.Triangle0);
                    chunkTriangles.Add(chunk06.Triangle1);
                }
                chunkTriangles.ForEach(x => x.DivideIndicesBy(2));
                chunkTriangles.ForEach(x => x.AddToIndices(baseIndex - stepIndex));
                triangles.AddRange(chunkTriangles);
            }
            return triangles;
        }

        #endregion
    }
}
