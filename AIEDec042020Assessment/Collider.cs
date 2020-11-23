using System;
using System.Collections.Generic;
using System.Text;
using MathLibrary;
using Raylib_cs;

namespace AIEDec042020Assessment
{
    abstract class Collider : Actor
    {
        public Collider(Vector2 position, float rotation) : base(position, rotation) { }

        public abstract bool IsCollided(Collider other);
    }
}
