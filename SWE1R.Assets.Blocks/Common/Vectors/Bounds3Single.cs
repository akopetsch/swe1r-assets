// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization.Components.Values.Customs;
using ByteSerialization.IO;
using System;
using System.Linq;

namespace SWE1R.Assets.Blocks.Common.Vectors
{
    public class Bounds3Single : ICustomSerializable // TODO: use struct
    {
        #region Properties (serialized)

        public Vector3Single Min { get; set; }
        public Vector3Single Max { get; set; }

        #endregion

        #region Properties (helper)

        public Bounds3Single Fixed
        {
            get
            {
                float minX = Math.Min(Min.X, Max.X);
                float minY = Math.Min(Min.Y, Max.Y);
                float minZ = Math.Min(Min.Z, Max.Z);

                float maxX = Math.Max(Min.X, Max.X);
                float maxY = Math.Max(Min.Y, Max.Y);
                float maxZ = Math.Max(Min.Z, Max.Z);

                return new Bounds3Single(minX, minY, minZ, maxX, maxY, maxZ);
            }
        }

        #endregion

        #region Constructor

        public Bounds3Single()
        {
            Min = new Vector3Single();
            Max = new Vector3Single();
        }

        public Bounds3Single(Vector3Single v0, Vector3Single v1) :
            this(v0.X, v0.Y, v0.Z, v1.X, v1.Y, v1.Z) { }

        public Bounds3Single(
            float x0, float y0, float z0, 
            float x1, float y1, float z1)
        {
            Min = new Vector3Single() {
                X = Math.Min(x0, x1),
                Y = Math.Min(y0, y1),
                Z = Math.Min(z0, z1),
            };

            Max = new Vector3Single() {
                X = Math.Max(x0, x1),
                Y = Math.Max(y0, y1),
                Z = Math.Max(z0, z1),
            };
        }

        #endregion

        #region Methods (: ICustomSerializable)

        public void Serialize(CustomComponent customComponent)
        {
            EndianBinaryWriter writer = customComponent.Writer;

            Min.Serialize(writer);
            Max.Serialize(writer);
        }

        public void Deserialize(CustomComponent customComponent)
        {
            EndianBinaryReader reader = customComponent.Reader;

            Min = new Vector3Single();
            Max = new Vector3Single();
            Min.Deserialize(reader);
            Max.Deserialize(reader);
        }

        #endregion

        #region Methods (helper)

        public bool Contains(Vector3Single vector)
        {
            if (vector.X < Min.X || Max.X < vector.X)
                return false;
            if (vector.Y < Min.Y || Max.Y < vector.Y)
                return false;
            if (vector.Z < Min.Z || Max.Z < vector.Z)
                return false;
            return true;
        }

        public bool Contains(params Bounds3Single[] bounds)
        {
            foreach (Bounds3Single b in bounds)
            {
                if (Contains(b.Min))
                    return false;
                if (Contains(b.Max))
                    return false;
            }
            return true;
        }

        public bool Contains(Bounds3Single other) =>
            Contains(other.Min) && Contains(other.Max);

        public static Bounds3Single Encapsulate(params Vector3Single[] vectors)
        {
            Vector3Single v0 = vectors.First();
            var bounds = new Bounds3Single(v0.X, v0.Y, v0.Z, v0.X, v0.Y, v0.Z); // TODO: use better constructor
            foreach (Vector3Single vector in vectors.Skip(1))
            {
                bounds.Min.X = Math.Min(bounds.Min.X, vector.X);
                bounds.Min.Y = Math.Min(bounds.Min.Y, vector.Y);
                bounds.Min.Z = Math.Min(bounds.Min.Z, vector.Z);

                bounds.Max.X = Math.Max(bounds.Max.X, vector.X);
                bounds.Max.Y = Math.Max(bounds.Max.Y, vector.Y);
                bounds.Max.Z = Math.Max(bounds.Max.Z, vector.Z);
            }

            return bounds;
        }

        public static Bounds3Single Encapsulate(params Bounds3Single[] bounds) =>
            Encapsulate(bounds.SelectMany(b => new Vector3Single[] { b.Min, b.Max }).ToArray());

        public bool Equals(Bounds3Single other)
        {
            if (other == null)
                return false;
            if (!Min.Equals(other.Min))
                return false;
            if (!Max.Equals(other.Max))
                return false;
            return true;
        }

        #endregion

        #region Methods (: object)

        public override string ToString() =>
            $"({Min}, {Max})";

        #endregion
    }
}
