// SPDX-License-Identifier: GPL-2.0-only

using ByteSerialization.Attributes;
using SWE1R.Assets.Blocks.ModelBlock.Materials;
using SWE1R.Assets.Blocks.ModelBlock.Meshes.Geometry;
using SWE1R.Assets.Blocks.ModelBlock.Meshes.VertexIndices;
using SWE1R.Assets.Blocks.ModelBlock.Nodes;
using SWE1R.Assets.Blocks.Vectors;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SWE1R.Assets.Blocks.ModelBlock.Meshes
{
    /// <summary>
    /// <see href="https://github.com/akopetsch/Sw_Racer/blob/76c8ad9cea549ea18457846a135a7f25d48b3813/include/Swr_Model.h#L203">SWR_MODEL_Section3</see>
    /// </summary>
    public class Mesh : INode
    {
        #region Properties (serialized)

        /// <summary>
        /// Must have a value.
        /// <para>Offset: 0x00</para>
        /// </summary>
        [Order(0), Reference(2)]
        public Material Material { get; set; } // TODO: xml comment
        /// <summary>
        /// Can be <see langword="null"/>.
        /// <para>Offset: 0x04</para>
        /// </summary>
        [Order(1), Reference(5)]
        public Mapping Mapping { get; set; } // TODO: xml comment
        /// <summary>
        /// Both <see cref="Bounds0">Bounds0</see> and <see cref="Bounds1">Bounds1</see> can be <c>(0, 0, 0)</c>.
        /// <para>Offset: 0x08</para>
        /// </summary>
        [Order(2)]
        public Vector3Single Bounds0 { get; set; } // TODO: xml comment
        /// <summary>
        /// Both <see cref="Bounds0">Bounds0</see> and <see cref="Bounds1">Bounds1</see> can be <c>(0, 0, 0)</c>.
        /// <para>Offset: 0x14</para>
        /// </summary>
        [Order(3)]
        public Vector3Single Bounds1 { get; set; } // TODO: xml comment
        /// <summary>
        /// Gets or sets the number of faces. Must be greater than zero.
        /// <para>See also: <seealso cref="FacesVertexCounts">FacesVertexCounts</seealso></para>
        /// <para>Offset: 0x20</para>
        /// </summary>
        [Order(4)]
        public short FacesCount { get; set; } // TODO: xml comment
        /// <summary>
        /// <para>Offset: 0x22</para>
        /// </summary>
        [Order(5)]
        public PrimitiveType PrimitiveType { get; set; } = PrimitiveType.Triangles; // TODO: xml comment
        /// <summary>
        /// Gets or sets the vertex counts of the faces. 
        /// If not <see langword="null"/>, the list's length is <see cref="FacesCount">FacesCount</see>.
        /// Has a value if <see cref="PrimitiveType">PrimitiveType</see> is <see cref="PrimitiveType.Polygons"/>, otherwise is <see langword="null"/>.
        /// <para>Offset: 0x24</para>
        /// </summary>
        [Order(6), Reference(0), Length(nameof(FacesCount))]
        public List<int> FacesVertexCounts { get; set; } // TODO: xml comment
        /// <summary>
        /// Gets or sets a referenced <see cref="MeshGroup3064"/> or <see langword="short"/><c>[]</c>. 
        /// <list type="bullet">
        ///   <item><description>If <see cref="Unk_Count">Unk_Count</see> is greater than 0 (only in <see cref="ModelType.Scen">Scen</see> or <see cref="ModelType.Pupp">Pupp</see> models), has a <see cref="MeshGroup3064"/> value.</description></item>
        ///   <item><description>If it is 0, has a <see langword="short"/><c>[]</c> value (only once in <see cref="ModelType.Trak">Trak</see> model 1) or is <see langword="null"/>.</description></item>
        /// </list>
        /// <para>Offset: 0x28</para>
        /// </summary>
        [Order(7)]
        public MeshGroupOrShorts MeshGroupOrShorts { get; set; } = new MeshGroupOrShorts(); // TODO: xml comment
        /// <summary>
        /// Gets or sets the list of collision vertices. 
        /// The list's length is <see cref="CollisionVerticesCount">CollisionVerticesCount</see>. Is <see langword="null"/> if that value is 0.
        /// <para>Offset: 0x2C</para>
        /// </summary>
        [Order(8), Reference(1)]
        public CollisionVertices CollisionVertices { get; set; }
        /// <summary>
        /// Gets or sets the list of index chunks for the visible vertices. 
        /// Has a value if <see cref="VisibleVerticesCount">VisibleVerticesCount</see> is greater than 0, otherwise is <see langword="null"/>.
        /// <para>Offset: 0x30</para>
        /// </summary>
        [Order(9), Reference(3)]
        public IndicesChunks VisibleIndicesChunks { get; set; }
        /// <summary>
        /// Gets or sets the list of visible vertices. 
        /// The list's length is <see cref="VisibleVerticesCount">VisibleVerticesCount</see>. Is <see langword="null"/> if that value is 0.
        /// <para>Offset: 0x34</para>
        /// </summary>
        [Order(10), Reference(4)] [Length(nameof(VisibleVerticesCount))]
        public List<Vertex> VisibleVertices { get; set; }
        /// <summary>
        /// Gets or sets the count of <see cref="CollisionVertices">CollisionVertices</see>. 
        /// If 0, <see cref="VisibleVerticesCount">VisibleVerticesCount</see> is greater than 0.
        /// <para>Offset: 0x38</para>
        /// </summary>
        [Order(11)]
        public short CollisionVerticesCount { get; set; }
        /// <summary>
        /// Gets or sets the count of <see cref="VisibleVertices">VisibleVertices</see>. 
        /// If 0, <see cref="CollisionVerticesCount">CollisionVerticesCount</see> is greater than 0.
        /// <para>Offset: 0x3A</para>
        /// </summary>
        [Order(12)]
        public short VisibleVerticesCount { get; set; }
        /// <summary>
        /// Unknown count. 
        /// <para>
        ///   If greater than 0, the following applies:
        ///   <list type="bullet">
        ///     <item><description><see cref="PrimitiveType">PrimitiveType</see> == <see cref="PrimitiveType.Triangles"/></description></item>
        ///     <item><description><see cref="VisibleVerticesCount">VisibleVerticesCount</see> > 0</description></item>
        ///     <item><description><see cref="CollisionVerticesCount">CollisionVerticesCount</see> >= 0</description></item>
        ///   </list>
        /// </para>
        /// <para>Offset: 0x3E</para>
        /// </summary>
        [Order(13), Offset(0x3e)]
        public short Unk_Count { get; set; } // TODO: xml comment

        #endregion

        #region Properties (helper)

        public Bounds3Single FixedBounds => 
            new Bounds3Single(Bounds0, Bounds1);

        #endregion

        #region Properties (: INode)

        public List<INode> Children { get; set; } = new List<INode>();

        #endregion

        #region Methods (serialization)

        public void UpdateCounts()
        {
            // TODO: implement in BindingComponent

            if (FacesVertexCounts != null)
                FacesCount = Convert.ToInt16(FacesVertexCounts.Count);
            
            CollisionVerticesCount = Convert.ToInt16(CollisionVertices?.List.Count ?? 0);
            VisibleVerticesCount = Convert.ToInt16(VisibleVertices?.Count ?? 0);

            // TODO: throw exception if MeshGroupOrShorts.Shorts.Length is invalid
        }

        public void UpdateFacesCountByVisibleIndicesChunks() =>
            FacesCount = Convert.ToInt16(VisibleIndicesChunks.SelectMany(x => x.Triangles).Count());

        public void UpdateBounds()
        {
            var bounds = new Bounds3Single(
                VisibleVertices.Select(v => (Vector3Single)v.Position).ToArray());
            Bounds0 = bounds.Min;
            Bounds1 = bounds.Max;
        }

        #endregion

        #region Methods (export)

        public List<Triangle> GetCollisionTriangles()
        {
            var triangles = new List<Triangle>();
            if (CollisionVerticesCount > 2)
            {
                int verticesIndex = 0;
                for (int i = 0; i < FacesCount; i++)
                {
                    int verticesCount = 0;
                    switch (PrimitiveType)
                    {
                        case PrimitiveType.Triangles:
                            verticesCount = 3;
                            var triangle = new Triangle(Enumerable.Range(verticesIndex, verticesCount).ToArray());
                            triangles.Add(triangle);
                            break;
                        case PrimitiveType.Quads:
                            verticesCount = 4;
                            var quad = new Quad(Enumerable.Range(verticesIndex, verticesCount).ToArray());
                            triangles.AddRange(quad.GetTriangles());
                            break;
                        case PrimitiveType.Polygons:
                            verticesCount = FacesVertexCounts[i];
                            var triangleStrip = new TriangleStrip(Enumerable.Range(verticesIndex, verticesCount).ToArray());
                            triangles.AddRange(triangleStrip.GetTriangles());
                            break;
                    }
                    verticesIndex += verticesCount;
                }
            }
            return triangles;
        }

        #endregion
    }
}
