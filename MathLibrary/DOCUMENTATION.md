# MathLibrary Documentation
This library contains classes and functions for use in the mathmatical side of Game Development
___
### Table of Contents
- [class Vector2](#class-vector2)
- [class Vector3](#class-vector3)
- [class Vector4](#class-vector4)
- [class Matrix3](#class-matrix3)
- [class Matrix4](#class-matrix4)
___

### class Vector2
2D representation of Vectors and Points.

Properties | Use
:----------|:----
public float X          | X coordinate
public float Y          | Y coordinate
public float Magnitude  | Returns the length of this Vector (Read Only)
public float Normalized | Returns this Vector with a magnitude of 1 (Read Only)

|Constructors
|:------------
|Vector2()
|Vector2(float x, float y)

Public Methods | Parameters | Use
:--------------|:-----------|:----
public static Vector2 Normalize | Vector2 vector                                   | Returns the given vector with a magnitude of 1
public static float DotProduct  | Vector2 lhs,<br>Vector2 rhs                      | Returns the dot product of two vectors
public static Vector2 Lerp      | Vector2 start,<br>Vector2 end,<br>float interval | Linearly interpolates between two points by a given interval

Operators | Use
:--------:|:----
\+        | Adds two Vectors
\-        | Subtracts two Vectors
\*        | Multiplies a Vector by a number
\\        | Divides a Vector by a number

> A Vector2 may be created with a Tuple of floats<br>
> `Vector2 vector = (x, y);`
___

### class Vector3
3D representation of Vectors and Points.

Properties | Use
:----------|:----
public float X          | X coordinate
public float Y          | Y coordinate
public float Z          | Z coordinate
public float Magnitude  | Returns the length of this Vector (Read Only)
public float Normalized | Returns this Vector with a magnitude of 1 (Read Only)

|Constructors
|:------------
|Vector3()
|Vector3(float x, float y, float z)
|Vector3(Vector2 vector)

Public Methods | Parameters | Use
:--------------|:-----------|:----
public static Vector3 Normalize    | Vector3 vector              | Returns the given vector with a magnitude of 1
public static float DotProduct     | Vector3 lhs,<br>Vector3 rhs | Returns the dot product of two vectors
public static Vector3 CrossProduct | Vector3 lhs,<br>Vector3 rhs | Returns the cross product of two vectors

Operators | Use
:--------:|:----
\+        | Adds two Vectors
\-        | Subtracts two Vectors
\*        | Multiplies a Vector by a number
\\        | Divides a Vector by a number

> A Vector3 may be created with a Tuple of floats<br>
> `Vector3 vector = (x, y, z);`
___
### class Vector4
3D Homogenous representation of Vectors and Points.

Properties | Use
:----------|:----
public float X          | X coordinate
public float Y          | Y coordinate
public float Z          | Z coordinate
public float W          | W coordinate
public float Magnitude  | Returns the length of this Vector (Read Only)
public float Normalized | Returns this Vector with a magnitude of 1 (Read Only)

|Constructors
|:------------
|Vector4()
|Vector4(float x, float y, float z, float w)
|Vector4(Vector3 vector)

Public Methods | Parameters | Use
:--------------|:-----------|:----
public static Vector4 Normalize    | Vector4 vector              | Returns the given vector with a magnitude of 1
public static float DotProduct     | Vector4 lhs,<br>Vector4 rhs | Returns the dot product of two vectors
public static Vector4 CrossProduct | Vector4 lhs,<br>Vector4 rhs | Returns the cross product of two vectors

Operators | Use
:--------:|:----
\+        | Adds two Vectors
\-        | Subtracts two Vectors
\*        | Multiplies a Vector by a number
\\        | Divides a Vector by a number

> A Vector4 may be created with a Tuple of floats<br>
> `Vector4 vector = (x, y, z, w);`
___

### class Matrix3
3x3 Matrix for use in 2D transforms

Variables | Use
:---------|:----
public float m11, m12, m13, m21, m22, m23, m31, m32, m33 | The individual elements of this matrix

|Constructors
|:------------
|Matrix3()
|Matrix3(float m11, float m12, float m13, float m21, float m22, float m23, float m31, float m32, float m33)

Public Methods | Parameters | Use
:--------------|:-----------|:----
public static Matrix3 CreateTranslation | Vector2 position | Returns an Identity matrix that has been translated by the given vector
public static Matrix3 CreateRotation    | float radians    | Returns an Identity matrix that has been rotated by the given value
public static Matrix3 CreateScale       | Vector2 Scale    | Returns an Identity matrix that has been scaled by the given vector

Operators | Use
:--------:|:----
\+        | Adds to Matrices
\-        | Subtracts two Matrices
\*        | Multiplies two Matrices together, or by a homogenous vector
___

### class Matrix4
4x4 Matrix for use in 3D transforms

Variables | Use
:---------|:----
public float m11, m12, m13, m14, m21, m22, m23, m24, m31, m32, m33, m34, m41, m42, m43, m44 | The individual elements of this matrix

|Constructors
|:------------
|Matrix4()
|public Matrix4(float m11, float m12, float m13, float m14, float m21, float m22, float m23, float m24, float m31, float m32, float m33, float m34, float m41, float m42, float m43, float m44)

Public Methods | Parameters | Use
:--------------|:-----------|:----
public static Matrix4 CreateTranslation      | Vector3 position | Returns an Identity matrix that has been translated by the given vector
public static Matrix4 CreateRotationX        | float radians    | Returns an Identity matrix that has been rotated by the given value on the X axis
public static Matrix4 CreateRotationY        | float radians    | Returns an Identity matrix that has been rotated by the given value on the Y axis
public static Matrix4 CreateRotationZ        | float radians    | Returns an Identity matrix that has been rotated by the given value on the Z axis
public static Matrix4 CreateCombinedRotation | Vector3 radians  | Returns an Identity matrix that has been rotated on all axes by the given vector
public static Matrix4 CreateScale            | Vector3 Scale    | Returns an Identity matrix that has been scaled by the given vector

Operators | Use
:--------:|:----
\+        | Adds to Matrices
\-        | Subtracts two Matrices
\*        | Multiplies two Matrices together, or by a homogenous vector