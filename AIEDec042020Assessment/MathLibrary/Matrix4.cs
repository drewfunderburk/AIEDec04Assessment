﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MathLibrary
{
    public class Matrix4
    {
        public float
            m11, m12, m13, m14,
            m21, m22, m23, m24,
            m31, m32, m33, m34,
            m41, m42, m43, m44;

        /// <summary>
        /// Initializes a new Matrix4 with an identity matrix
        /// </summary>
        public Matrix4()
        {
            m11 = 1; m12 = 0; m13 = 0; m14 = 0;
            m21 = 0; m22 = 1; m23 = 0; m24 = 0;
            m31 = 0; m32 = 0; m33 = 1; m34 = 0;
            m41 = 0; m42 = 0; m43 = 0; m44 = 1;
        }

        /// <summary>
        /// Initializes a new Matrix4 with the given values
        /// </summary>
        /// <param name="m11">Row 1 Column 1</param>
        /// <param name="m12">Row 1 Column 2</param>
        /// <param name="m13">Row 1 Column 3</param>
        /// <param name="m14">Row 1 Column 4</param>
        /// <param name="m21">Row 2 Column 1</param>
        /// <param name="m22">Row 2 Column 2</param>
        /// <param name="m23">Row 2 Column 3</param>
        /// <param name="m24">Row 2 Column 4</param>
        /// <param name="m31">Row 3 Column 1</param>
        /// <param name="m32">Row 3 Column 2</param>
        /// <param name="m33">Row 3 Column 3</param>
        /// <param name="m34">Row 3 Column 4</param>
        /// <param name="m41">Row 4 Column 1</param>
        /// <param name="m42">Row 4 Column 2</param>
        /// <param name="m43">Row 4 Column 3</param>
        /// <param name="m44">Row 4 Column 4</param>
        public Matrix4(
            float m11, float m12, float m13, float m14,
            float m21, float m22, float m23, float m24,
            float m31, float m32, float m33, float m34,
            float m41, float m42, float m43, float m44)
        {
            this.m11 = m11; this.m12 = m12; this.m13 = m13; this.m14 = m14;
            this.m21 = m21; this.m22 = m22; this.m23 = m23; this.m24 = m14;
            this.m31 = m31; this.m32 = m32; this.m33 = m33; this.m34 = m34;
            this.m41 = m41; this.m42 = m42; this.m43 = m43; this.m44 = m44;
        }

        /// <summary>
        /// Creates a new matrix that has been translated by the given value
        /// </summary>
        /// <param name="position">The position of the new matrix</param>
        /// <returns></returns>
        public static Matrix4 CreateTranslation(Vector3 position)
        {
            return new Matrix4(
                1, 0, 0, position.X,
                0, 1, 0, position.Y,
                0, 0, 1, position.Z,
                0, 0, 0, 1);
        }

        public static Matrix4 CreateRotationX(float radians)
        {
            return new Matrix4(
                1, 0, 0, 0,
                0, (float)Math.Cos(radians), (float)Math.Sin(radians), 0,
                0, -(float)Math.Sin(radians), (float)Math.Cos(radians), 0,
                0, 0, 0, 1);
        }

        public static Matrix4 CreateRotationY(float radians)
        {
            return new Matrix4(
                (float)Math.Cos(radians), 0, -(float)Math.Sin(radians), 0,
                0, 1, 0, 0,
                (float)Math.Sin(radians), 0, (float)Math.Cos(radians), 0,
                0, 0, 0, 1);
        }

        public static Matrix4 CreateRotationZ(float radians)
        {
            return new Matrix4(
                (float)Math.Cos(radians), (float)Math.Sin(radians), 0, 0,
                -(float)Math.Sin(radians), (float)Math.Cos(radians), 0, 0,
                0, 0, 1, 0,
                0, 0, 0, 1);
        }

        public static Matrix4 CreateCombinedRotation(Vector3 radians)
        {
            return new Matrix4() * CreateRotationX(radians.X) * CreateRotationY(radians.Y) * CreateRotationZ(radians.Z);
        }

        public static Matrix4 CreateScale(Vector3 scale)
        {
            return new Matrix4(
                scale.X, 0, 0, 0,
                0, scale.Y, 0, 0,
                0, 0, scale.Z, 0,
                0, 0, 0, 1);
        }

        public static Matrix4 operator +(Matrix4 lhs, Matrix4 rhs)
        {
            return new Matrix4
                (
                    lhs.m11 + rhs.m11, lhs.m12 + rhs.m12, lhs.m13 + rhs.m13, lhs.m14 + rhs.m14,
                    lhs.m21 + rhs.m21, lhs.m22 + rhs.m22, lhs.m23 + rhs.m23, lhs.m24 + rhs.m24,
                    lhs.m31 + rhs.m31, lhs.m32 + rhs.m32, lhs.m33 + rhs.m33, lhs.m34 + rhs.m34,
                    lhs.m41 + rhs.m41, lhs.m42 + rhs.m42, lhs.m43 + rhs.m43, lhs.m44 + rhs.m44
                );
        }

        public static Matrix4 operator -(Matrix4 lhs, Matrix4 rhs)
        {
            return new Matrix4
                (
                    lhs.m11 - rhs.m11, lhs.m12 - rhs.m12, lhs.m13 - rhs.m13, lhs.m14 - rhs.m14,
                    lhs.m21 - rhs.m21, lhs.m22 - rhs.m22, lhs.m23 - rhs.m23, lhs.m24 - rhs.m24,
                    lhs.m31 - rhs.m31, lhs.m32 - rhs.m32, lhs.m33 - rhs.m33, lhs.m34 - rhs.m34,
                    lhs.m41 - rhs.m41, lhs.m42 - rhs.m42, lhs.m43 - rhs.m43, lhs.m44 - rhs.m44
                );
        }

        public static Matrix4 operator *(Matrix4 lhs, Matrix4 rhs)
        {
            return new Matrix4
                (
                    // Row 1 Column 1
                    lhs.m11 * rhs.m11 + lhs.m12 * rhs.m21 + lhs.m13 * rhs.m31 + lhs.m14 * rhs.m41,
                    // Row 1 Column 2
                    lhs.m11 * rhs.m12 + lhs.m12 * rhs.m22 + lhs.m13 * rhs.m32 + lhs.m14 * rhs.m42,
                    // Row 1 Column3
                    lhs.m11 * rhs.m13 + lhs.m12 * rhs.m23 + lhs.m13 * rhs.m33 + lhs.m14 * rhs.m43,
                    // Row1 Column 4
                    lhs.m11 * rhs.m14 + lhs.m12 * rhs.m24 + lhs.m13 * rhs.m34 + lhs.m14 * rhs.m44,

                    // Row 2 Column 1
                    lhs.m21 * rhs.m11 + lhs.m22 * rhs.m21 + lhs.m23 * rhs.m31 + lhs.m24 * rhs.m41,
                    // Row 2 Column 2
                    lhs.m21 * rhs.m12 + lhs.m22 * rhs.m22 + lhs.m23 * rhs.m32 + lhs.m24 * rhs.m42,
                    // Row 2 Column 3
                    lhs.m21 * rhs.m13 + lhs.m22 * rhs.m23 + lhs.m23 * rhs.m33 + lhs.m24 * rhs.m43,
                    // Row 2 Column 4
                    lhs.m21 * rhs.m14 + lhs.m22 * rhs.m24 + lhs.m23 * rhs.m34 + lhs.m24 + rhs.m44,

                    // Row 3 Column 1
                    lhs.m31 * rhs.m11 + lhs.m32 * rhs.m21 + lhs.m33 * rhs.m31 + lhs.m34 + rhs.m41,
                    // Row 3 Column 2
                    lhs.m31 * rhs.m12 + lhs.m32 * rhs.m22 + lhs.m33 * rhs.m32 + lhs.m34 * rhs.m42,
                    // Row 3 Column 3
                    lhs.m31 * rhs.m13 + lhs.m32 * rhs.m23 + lhs.m33 * rhs.m33 + lhs.m34 * rhs.m43,
                    // Row 3 Column 4
                    lhs.m31 * rhs.m14 + lhs.m32 * rhs.m24 + lhs.m33 * rhs.m34 + lhs.m34 * rhs.m44,

                    // Row 4 Column 1
                    lhs.m41 * rhs.m11 + lhs.m42 * rhs.m21 + lhs.m43 * rhs.m31 + lhs.m44 + rhs.m41,
                    // Row 4 Column 2
                    lhs.m41 * rhs.m21 + lhs.m42 * rhs.m22 + lhs.m43 * rhs.m32 + lhs.m44 + rhs.m42,
                    // Row 4 Column 3
                    lhs.m41 * rhs.m31 + lhs.m42 * rhs.m23 + lhs.m43 * rhs.m33 + lhs.m44 * rhs.m43,
                    // Row 4 Column 4
                    lhs.m41 * rhs.m41 * lhs.m42 * rhs.m24 + lhs.m43 * rhs.m34 + lhs.m44 * rhs.m44
                );
        }

        public static Vector4 operator *(Matrix4 lhs, Vector4 rhs)
        {
            return new Vector4(
                lhs.m11 * rhs.X + lhs.m12 * rhs.Y + lhs.m13 * rhs.Z + lhs.m14 * rhs.W,
                lhs.m21 * rhs.X + lhs.m22 * rhs.Y + lhs.m23 * rhs.Z + lhs.m24 * rhs.W,
                lhs.m31 * rhs.X + lhs.m32 * rhs.Y + lhs.m33 * rhs.Z + lhs.m34 * rhs.W,
                lhs.m41 * rhs.X + lhs.m42 * rhs.Y + lhs.m43 * rhs.Z + lhs.m44 * rhs.W
                );
        }

        public static Vector4 operator *(Vector4 lhs, Matrix4 rhs)
        {
            return new Vector4();
        }
    }
}
