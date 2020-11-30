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
public static Vector2 Normalize | Vector2 vector              | Returns the given vector with a magnitude of 1
public static float DotProduct  | Vector2 lhs,<br>Vector2 rhs | Returns the dot product of two vectors

Operators | Use
:--------:|:----
\+        | Adds two Vectors
\-        | Subtracts two Vectors
\*        | Multiplies a Vector by a number
\\        | Divides a Vector by a number

> A Vector2 may be created with a Tuple of floats<br>
> `Vector2 vector = (0, 0);`
___

### class Vector3
#D representation of Vectors and Points.

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
> `Vector3 vector = (0, 0, 0);`
___
### class Vector4

___

### class Matrix3

___

### class Matrix4