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


        public static implicit operator Vector2((float, float) tuple)
        { return new Vector2(tuple.Item1, tuple.Item2); }

        public static Vector2 operator +(Vector2 lhs, Vector2 rhs)
        { return new Vector2(lhs.X + rhs.X, lhs.Y + rhs.Y); }

        public static Vector2 operator -(Vector2 lhs, Vector2 rhs)
        { return new Vector2(lhs.X - rhs.X, lhs.Y - rhs.Y); }

        public static Vector2 operator *(Vector2 lhs, float scalar)
        { return new Vector2(lhs.X * scalar, lhs.Y * scalar); }

        public static Vector2 operator /(Vector2 lhs, float scalar)
        { return new Vector2(lhs.X / scalar, lhs.Y / scalar); }
    }
}
