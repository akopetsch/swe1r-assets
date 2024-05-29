// SPDX-License-Identifier: MIT

using ByteSerialization.Components.Values.Customs;
using ByteSerialization.IO;
using System;
using System.Linq;

namespace SWE1R.Assets.Blocks.Vectors
{
    public class Bounds3Single : ICustomSerializable // TODO: use struct
    {
        #region Properties (serialized)

        public Vector3Single Min { get; set; }
        public Vector3Single Max { get; set; }

        #endregion

        #region Properties (helper)

        public Vector3Single Size => Max - Min;

        public bool IsValid =>
            Min.X <= Max.X &&
            Min.Y <= Max.Y &&
            Min.Z <= Max.Z;

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

        public Bounds3Single(params Vector3Single[] vectors)
        {
            var x = vectors.Select(v => v.X).ToArray();
            var y = vectors.Select(v => v.Y).ToArray();
            var z = vectors.Select(v => v.Z).ToArray();

            Min = new Vector3Single() {
                X = x.Min(),
                Y = y.Min(),
                Z = z.Min(),
            };

            Max = new Vector3Single() {
                X = x.Max(),
                Y = y.Max(),
                Z = z.Max(),
            };
        }

        public Bounds3Single(Bounds3Single[] bounds) :
            this(bounds.SelectMany(b => new Vector3Single[] { b.Min, b.Max }).ToArray())
        { }

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
