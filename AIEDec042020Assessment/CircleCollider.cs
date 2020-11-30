using System;
using System.Collections.Generic;
using System.Text;
using MathLibrary;
using Raylib_cs;

namespace AIEDec042020Assessment
{
    /// <summary>
    /// Basic Circle collision
    /// </summary>
    class CircleCollider : Collider
    {
        /// <summary>
        /// Radius of the collider
        /// </summary>
        public float Radius { get; set; }

        public CircleCollider(Vector2 position, float radius) : base(position, (float)Math.PI / 2)
        {
            Radius = radius;
        }

        public override void Draw()
        {
            Raylib.DrawCircleLines(
                (int)GlobalPosition.X, 
                (int)GlobalPosition.Y, 
                Radius, 
                Color.RED);
        }

        // Implement basic circle-circle collision
        public override bool IsCollided(Collider collidedActor)
        {
            // Check if other collider is also a circle
            if (collidedActor is CircleCollider)
            {
                // Cast to CircleCollider
                CircleCollider other = collidedActor as CircleCollider;

                // If the two colliders are intersecting, return true
                if ((other.GlobalPosition - GlobalPosition).Magnitude <= other.Radius + Radius)
                    return true;
                else
                    return false;
            }
            else
            {
                // Throw error if another collider type is used, as it has not been implemented
                throw new NotImplementedException();
            }
        }
    }
}
