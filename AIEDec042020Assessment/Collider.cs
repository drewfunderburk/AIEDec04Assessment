using System;
using System.Collections.Generic;
using System.Text;
using MathLibrary;
using Raylib_cs;

namespace AIEDec042020Assessment
{
    /// <summary>
    /// Base class for all colliders
    /// </summary>
    abstract class Collider : Actor
    {
        public Collider(Vector2 position, float rotation) : base(position, rotation) { }

        /// <summary>
        /// Is this collider collided with another
        /// </summary>
        /// <param name="other">Other collider</param>
        /// <returns></returns>
        public abstract bool IsCollided(Collider other);
    }
}
