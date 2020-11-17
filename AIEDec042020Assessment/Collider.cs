using System;
using System.Collections.Generic;
using System.Text;
using MathLibrary;
using Raylib_cs;

namespace AIEDec042020Assessment
{
    class Collider
    {
        public float Radius { get; set; }
        public Vector2 Position { get; set; }

        public Collider(Vector2 position, float radius)
        {
            Radius = radius;
        }

        public bool IsCollided()
        {
            return false;
        }
    }
}
