// SPDX-License-Identifier: MIT

using ByteSerialization.Nodes;
using SWE1R.Assets.Blocks.ModelBlock;
using SWE1R.Assets.Blocks.ModelBlock.Meshes;
using System.ComponentModel.DataAnnotations.Schema;

namespace SWE1R.Assets.Blocks.Original.SQLite.Entities.ModelBlock.Meshes
{
    [Table($"{nameof(Model)}_{nameof(Mesh)}")]
    public class DbMesh : DbBlockItemStructure<Mesh>
    {
        #region Properties

        public int P_MeshMaterial { get; set; }
        public int P_Behaviour { get; set; }
        public float FixedBounds_Min_X { get; set; }
        public float FixedBounds_Min_Y { get; set; }
        public float FixedBounds_Min_Z { get; set; }
        public float FixedBounds_Max_X { get; set; }
        public float FixedBounds_Max_Y { get; set; }
        public float FixedBounds_Max_Z { get; set; }
        public short FacesCount { get; set; }
        public PrimitiveType PrimitiveType { get; set; }
        public int P_FacesVertexCounts { get; set; }
        public int P_MeshGroupNodeOrShorts { get; set; }
        public int P_CollisionVertices { get; set; }
        public byte[] CollisionVertices_PaddingGarbage { get; set; }
        public int P_CommandList { get; set; }
        public int P_Vertices { get; set; }
        public int CollisionVerticesCount { get; set; }
        public int VerticesCount { get; set; }
        public short Unk_Count { get; set; }

        #endregion

        public override void CopyFrom(Node node)
        {
            base.CopyFrom(node);

            var x = (Mesh)node.Value;

            P_MeshMaterial = GetPropertyPointer(node, nameof(Mesh.MeshMaterial));
            P_Behaviour = GetPropertyPointer(node, nameof(Mesh.Behaviour));

            FixedBounds_Min_X = x.FixedBounds.Min.X;
            FixedBounds_Min_Y = x.FixedBounds.Min.Y;
            FixedBounds_Min_Z = x.FixedBounds.Min.Z;

            FixedBounds_Max_X = x.FixedBounds.Max.X;
            FixedBounds_Max_Y = x.FixedBounds.Max.Y;
            FixedBounds_Max_Z = x.FixedBounds.Max.Z;

            FacesCount = x.FacesCount;
            PrimitiveType = x.PrimitiveType;

            P_FacesVertexCounts = GetPropertyPointer(node, nameof(Mesh.FacesVertexCounts));
            P_MeshGroupNodeOrShorts = GetValuePosition(node.Graph, x.MeshGroupNodeOrShorts.Value);
            P_CollisionVertices = GetPropertyPointer(node, nameof(Mesh.CollisionVertices));

            CollisionVertices_PaddingGarbage = x.CollisionVertices?.PaddingGarbage;

            P_CommandList = GetPropertyPointer(node, nameof(Mesh.CommandList));
            P_Vertices = GetPropertyPointer(node, nameof(Mesh.Vertices));

            CollisionVerticesCount = x.CollisionVerticesCount;
            VerticesCount = x.VerticesCount;
            Unk_Count = x.Unk_Count;
        }

        public override bool Equals(DbBlockItemStructure<Mesh> other)
        {
            var x = (DbMesh)other;

            if (!base.Equals(x))
                return false;

            if (P_MeshMaterial != x.P_MeshMaterial) return false;
            if (P_Behaviour != x.P_Behaviour) return false;

            if (FixedBounds_Min_X != x.FixedBounds_Min_X) return false;
            if (FixedBounds_Min_Y != x.FixedBounds_Min_Y) return false;
            if (FixedBounds_Min_Z != x.FixedBounds_Min_Z) return false;

            if (FixedBounds_Max_X != x.FixedBounds_Max_X) return false;
            if (FixedBounds_Max_Y != x.FixedBounds_Max_Y) return false;
            if (FixedBounds_Max_Z != x.FixedBounds_Max_Z) return false;

            if (FacesCount != x.FacesCount) return false;
            if (PrimitiveType != x.PrimitiveType) return false;

            if (P_FacesVertexCounts != x.P_FacesVertexCounts) return false;
            if (P_MeshGroupNodeOrShorts != x.P_MeshGroupNodeOrShorts) return false;
            if (P_CollisionVertices != x.P_CollisionVertices) return false;

            if (!EqualsPaddingGarbage(CollisionVertices_PaddingGarbage, x.CollisionVertices_PaddingGarbage)) return false;

            if (P_CommandList != x.P_CommandList) return false;
            if (P_Vertices != x.P_Vertices) return false;

            if (CollisionVerticesCount != x.CollisionVerticesCount) return false;
            if (VerticesCount != x.VerticesCount) return false;
            if (Unk_Count != x.Unk_Count) return false;

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
            if (obj is DbMesh x)
                return Equals(x);
            else
                return base.Equals(obj);
        }

        public override int GetHashCode() =>
            CombineHashCodes(base.GetHashCode(),
                P_MeshMaterial, P_Behaviour,
                FixedBounds_Min_X, FixedBounds_Min_Y, FixedBounds_Min_Z,
                FixedBounds_Max_X, FixedBounds_Max_Y, FixedBounds_Max_Z,
                FacesCount, PrimitiveType, P_FacesVertexCounts, P_MeshGroupNodeOrShorts, 
                P_CollisionVertices, CollisionVertices_PaddingGarbage, P_CommandList, P_Vertices,
                CollisionVerticesCount, VerticesCount, Unk_Count);
    }
}
