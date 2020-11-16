using System;
using System.Collections.Generic;
using System.Text;

namespace MathLibrary
{
    public class Vector4
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }
        public float W { get; set; }

        /// <summary>
        /// This vector's magnitude
        /// </summary>
        public float Magnitude
        { get { return (float)Math.Sqrt(X * X + Y * Y + Z * Z + W * W); } }

        /// <summary>
        /// This vector with its magnitude normalized to 1
        /// </summary>
        public Vector4 Normalized
        { get { return Normalize(this); } }

        /// <summary>
        /// Constructs a new Vector4 (0, 0, 0, 0)
        /// </summary>
        public Vector4()
        {
            X = 0;
            Y = 0;
            Z = 0;
            W = 0;
        }

        /// <summary>
        /// Create a new vector4 from a vector3 (X, Y, Z, 0)
        /// </summary>
        /// <param name="vector"></param>
        public Vector4(Vector3 vector)
        {
            X = vector.X;
            Y = vector.Y;
            Z = vector.Z;
            W = 0;
        }
        
        public Vector4(float x, float y, float z, float w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }

        /// <summary>
        /// Return the given vector normalized
        /// </summary>
        /// <param name="vector"></param>
        /// <returns></returns>
        public static Vector4 Normalize(Vector4 vector)
        {
            if (vector.Magnitude == 0)
                return new Vector4();
            return vector / vector.Magnitude;
        }

        /// <summary>
        /// Returns the Dot Product of the given vectors
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        /// <returns></returns>
        public static float DotProduct(Vector4 lhs, Vector4 rhs)
        { return (lhs.X * rhs.X) + (lhs.Y * rhs.Y) + (lhs.Z * rhs.Z) + (lhs.W * rhs.W); }

        /// <summary>
        /// Returns the Cross Product of the given vectors
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        /// <returns></returns>
        public static Vector4 CrossProduct(Vector4 lhs, Vector4 rhs)
        {
            Vector3 a = new Vector3(lhs.X, lhs.Y, lhs.Z);
            Vector3 b = new Vector3(rhs.X, rhs.Y, rhs.Z);
            Vector3 cross = Vector3.CrossProduct(a, b);
            return new Vector4(cross.X, cross.Y, cross.Z, 0);
        }

        public static implicit operator Vector4((float, float, float, float) tuple)
        { return new Vector4(tuple.Item1, tuple.Item2, tuple.Item3, tuple.Item4); }

        public static Vector4 operator +(Vector4 lhs, Vector4 rhs)
        { return new Vector4(lhs.X + rhs.X, lhs.Y + rhs.Y, lhs.Z + rhs.Z, lhs.W + rhs.W); }

        public static Vector4 operator -(Vector4 lhs, Vector4 rhs)
        { return new Vector4(lhs.X - rhs.X, lhs.Y - rhs.Y, lhs.Z - rhs.Z, lhs.W - rhs.W); }

        public static Vector4 operator *(Vector4 lhs, float rhs)
        { return new Vector4(lhs.X * rhs, lhs.Y * rhs, lhs.Z * rhs, lhs.W * rhs); }

        public static Vector4 operator *(float lhs, Vector4 rhs)
        { return new Vector4(lhs * rhs.X, lhs * rhs.Y, lhs * rhs.Z, lhs * rhs.W); }

        public static Vector4 operator /(Vector4 lhs, float rhs)
        { return new Vector4(lhs.X / rhs, lhs.Y / rhs, lhs.Z / rhs, lhs.W / rhs); }

        public static Vector4 operator /(float lhs, Vector4 rhs)
        { return new Vector4(lhs / rhs.X, lhs / rhs.Y, lhs / rhs.Z, lhs / rhs.W); }
    }
}
