using System;

namespace MathLibrary
{
    public class Vector2
    {
        public float X { get; set; }

        public float Y { get; set; }

        /// <summary>
        /// This vector's magnitude
        /// </summary>
        public float Magnitude
        { get { return (float)Math.Sqrt(X * X + Y * Y); } }

        /// <summary>
        /// This vector with its magnitude normalized to 1
        /// </summary>
        public Vector2 Normalized
        { get { return Normalize(this); } }

        /// <summary>
        /// Constructs a new Vector2 (0, 0)
        /// </summary>
        public Vector2()
        {
            X = 0;
            Y = 0;
        }

        /// <summary>
        /// Constructs a new Vector2 from the given values (x, y)
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public Vector2(float x, float y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// Return the given vector normalized
        /// </summary>
        /// <param name="vector"></param>
        /// <returns></returns>
        public static Vector2 Normalize(Vector2 vector)
        {
            if (vector.Magnitude == 0)
                return new Vector2();

            return vector / vector.Magnitude;
        }

        /// <summary>
        /// Returns the Dot Product of the given vectors
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        /// <returns></returns>
        public static float DotProduct(Vector2 lhs, Vector2 rhs)
        { return (lhs.X * rhs.X) + (lhs.Y * rhs.Y); }


        // Overload Tuple to allow easy Vector2 creation
        public static implicit operator Vector2((float, float) tuple)
        { return new Vector2(tuple.Item1, tuple.Item2); }

        // Vector2-Vector2 addition
        public static Vector2 operator +(Vector2 lhs, Vector2 rhs)
        { return new Vector2(lhs.X + rhs.X, lhs.Y + rhs.Y); }

        // Vector2-Vector2 subtraction
        public static Vector2 operator -(Vector2 lhs, Vector2 rhs)
        { return new Vector2(lhs.X - rhs.X, lhs.Y - rhs.Y); }

        // Vector2-Scalar multiplication
        public static Vector2 operator *(Vector2 lhs, float rhs)
        { return new Vector2(lhs.X * rhs, lhs.Y * rhs); }

        // Scalar-Vector2 multiplication
        public static Vector2 operator *(float lhs, Vector2 rhs)
        { return new Vector2(lhs * rhs.X, lhs * rhs.Y); }

        // Vector2-Scalar division
        public static Vector2 operator /(Vector2 lhs, float rhs)
        { return new Vector2(lhs.X / rhs, lhs.Y / rhs); }

        // Scalar-Vector2 division
        public static Vector2 operator /(float lhs, Vector2 rhs)
        { return new Vector2(lhs / rhs.X, lhs / rhs.Y); }
    }
}
