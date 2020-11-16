using System;
using System.Collections.Generic;
using System.Text;

namespace MathLibrary
{
    public class Vector3
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }

        /// <summary>
        /// This vector's magnitude
        /// </summary>
        public float Magnitude
        { get { return (float)Math.Sqrt(X * X + Y * Y + Z * Z); } }

        /// <summary>
        /// This vector with its magnitude normalized to 1
        /// </summary>
        public Vector3 Normalized
        { get { return Normalize(this); } }

        /// <summary>
        /// Constructs a new Vector3 (0, 0, 0)
        /// </summary>
        public Vector3()
        {
            X = 0;
            Y = 0;
            Z = 0;
        }

        /// <summary>
        /// Constructs a new Vector3 from a Vector2 (X, Y, 0)
        /// </summary>
        /// <param name="vector"></param>
        public Vector3(Vector2 vector)
        {
            X = vector.X;
            Y = vector.Y;
            Z = 0;
        }

        public Vector3(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        /// <summary>
        /// Return the given vector normalized
        /// </summary>
        /// <param name="vector"></param>
        /// <returns></returns>
        public static Vector3 Normalize(Vector3 vector)
        {
            if (vector.Magnitude == 0)
                return new Vector3();
            return vector / vector.Magnitude;
        }

        /// <summary>
        /// Returns the Dot Product of the given vectors
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        /// <returns></returns>
        public static float DotProduct(Vector3 lhs, Vector3 rhs)
        { return (lhs.X * rhs.X) + (lhs.Y * rhs.Y) + (lhs.Z * rhs.Z); }

        /// <summary>
        /// Returns the Cross Product of the given vectors
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        /// <returns></returns>
        public static Vector3 CrossProduct(Vector3 lhs, Vector3 rhs)
        {
            return new Vector3(
                lhs.Y * rhs.Z - lhs.Z * rhs.Y,
                lhs.Z * rhs.X - lhs.X * rhs.Z,
                lhs.X * rhs.Y - lhs.Y * rhs.X);
        }

        public static Vector3 operator +(Vector3 lhs, Vector3 rhs)
        { return new Vector3(lhs.X + rhs.X, lhs.Y + rhs.Y, lhs.Z + rhs.Z); }

        public static Vector3 operator -(Vector3 lhs, Vector3 rhs)
        { return new Vector3(lhs.X - rhs.X, lhs.Y - rhs.Y, lhs.Z - rhs.Z); }

        public static Vector3 operator *(Vector3 lhs, float rhs)
        { return new Vector3(lhs.X * rhs, lhs.Y * rhs, lhs.Z * rhs); }

        public static Vector3 operator *(float lhs, Vector3 rhs)
        { return new Vector3(lhs * rhs.X, lhs * rhs.Y, lhs * rhs.Z); }

        public static Vector3 operator /(Vector3 lhs, float rhs)
        { return new Vector3(lhs.X / rhs, lhs.Y / rhs, lhs.Z / rhs); }

        public static Vector3 operator /(float lhs, Vector3 rhs)
        { return new Vector3(lhs / rhs.X, lhs / rhs.Y, lhs / rhs.Z); }
    }
}
