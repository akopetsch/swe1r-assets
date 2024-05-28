// Copyright 2024 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization.Nodes;
using SWE1R.Assets.Blocks.ModelBlock.Meshes;
using System.ComponentModel.DataAnnotations.Schema;

namespace SWE1R.Assets.Blocks.Original.SQLite.Entities.ModelBlock.Meshes
{
    [Table("Model_Mesh")]
    public class DbMesh : DbBlockItemStructure<Mesh>
    {
        public int P_Material { get; set; }
        public int P_Mapping { get; set; }
        public float Bounds_Min_X { get; set; }
        public float Bounds_Min_Y { get; set; }
        public float Bounds_Min_Z { get; set; }
        public float Bounds_Max_X { get; set; }
        public float Bounds_Max_Y { get; set; }
        public float Bounds_Max_Z { get; set; }
        public short FacesCount { get; set; }
        public PrimitiveType PrimitiveType { get; set; }
        public int P_FacesVertexCounts { get; set; }
        public int P_Unk_Array { get; set; }
        public int P_CollisionVertices { get; set; }
        public byte[] PaddingGarbage { get; set; }
        public int P_IndicesChunks { get; set; }
        public int P_Vertices { get; set; }
        public int CollisionVerticesCount { get; set; }
        public int VerticesCount { get; set; }
        public short Unk_Count { get; set; }

        public override void CopyFrom(Node node)
        {
            base.CopyFrom(node);

            var m = (Mesh)node.Value;

            P_Material = GetPropertyPointer(node, nameof(Mesh.Material));
            P_Mapping = GetPropertyPointer(node, nameof(Mesh.Mapping));

            Bounds_Min_X = m.FixedBounds.Min.X;
            Bounds_Min_Y = m.FixedBounds.Min.Y;
            Bounds_Min_Z = m.FixedBounds.Min.Z;

            Bounds_Max_X = m.FixedBounds.Max.X;
            Bounds_Max_Y = m.FixedBounds.Max.Y;
            Bounds_Max_Z = m.FixedBounds.Max.Z;

            FacesCount = m.FacesCount;
            PrimitiveType = m.PrimitiveType;

            P_FacesVertexCounts = GetPropertyPointer(node, nameof(Mesh.FacesVertexCounts));
            P_Unk_Array = GetValuePosition(node.Graph, m.MeshGroupOrShorts.Value);
            P_CollisionVertices = GetPropertyPointer(node, nameof(Mesh.CollisionVertices));

            PaddingGarbage = m.CollisionVertices?.PaddingGarbage;

            P_IndicesChunks = GetPropertyPointer(node, nameof(Mesh.VisibleIndicesChunks));
            P_Vertices = GetPropertyPointer(node, nameof(Mesh.VisibleVertices));

            CollisionVerticesCount = m.CollisionVerticesCount;
            VerticesCount = m.VisibleVerticesCount;
            Unk_Count = m.Unk_Count;
        }

        public override bool Equals(DbBlockItemStructure<Mesh> other)
        {
            var _other = (DbMesh)other;

            if (!base.Equals(_other))
                return false;

            if (P_Material != _other.P_Material) return false;
            if (P_Mapping != _other.P_Mapping) return false;

            if (Bounds_Min_X != _other.Bounds_Min_X) return false;
            if (Bounds_Min_Y != _other.Bounds_Min_Y) return false;
            if (Bounds_Min_Z != _other.Bounds_Min_Z) return false;

            if (Bounds_Max_X != _other.Bounds_Max_X) return false;
            if (Bounds_Max_Y != _other.Bounds_Max_Y) return false;
            if (Bounds_Max_Z != _other.Bounds_Max_Z) return false;

            if (FacesCount != _other.FacesCount) return false;
            if (PrimitiveType != _other.PrimitiveType) return false;

            if (P_FacesVertexCounts != _other.P_FacesVertexCounts) return false;
            if (P_Unk_Array != _other.P_Unk_Array) return false;
            if (P_CollisionVertices != _other.P_CollisionVertices) return false;

            if (!EqualsPaddingGarbage(PaddingGarbage, _other.PaddingGarbage)) return false;

            if (P_IndicesChunks != _other.P_IndicesChunks) return false;
            if (P_Vertices != _other.P_Vertices) return false;

            if (CollisionVerticesCount != _other.CollisionVerticesCount) return false;
            if (VerticesCount != _other.VerticesCount) return false;
            if (Unk_Count != _other.Unk_Count) return false;

            return true;
        }

        private bool EqualsPaddingGarbage(byte[] value1, byte[] value2)
        {
            if (value1 == null && value2 == null)
                return true;
            else if (value1 != null && value2 != null)
                return Enumerable.SequenceEqual(value1, value2);
            else
                return false; 
        }

        public override bool Equals(object obj)
        {
            if (obj is DbMesh)
                return this.Equals((DbMesh)obj);
            else
                return base.Equals(obj);
        }

        public override int GetHashCode() =>
            HashCode.Combine(base.GetHashCode(), P_Material, P_Mapping,
                HashCode.Combine(Bounds_Min_X, Bounds_Min_Y, Bounds_Min_Z),
                HashCode.Combine(Bounds_Max_X, Bounds_Max_Y, Bounds_Max_Z),
                HashCode.Combine(FacesCount, PrimitiveType, P_FacesVertexCounts, P_Unk_Array, 
                    P_CollisionVertices, PaddingGarbage, P_IndicesChunks, P_Vertices),
                HashCode.Combine(CollisionVerticesCount, VerticesCount, Unk_Count));
    }
}
