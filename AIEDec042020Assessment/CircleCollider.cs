using System;
using System.Collections.Generic;
using System.Text;
using MathLibrary;
using Raylib_cs;

namespace AIEDec042020Assessment
{
    class CircleCollider : Collider
    {
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

        public override bool IsCollided(Collider collidedActor)
        {
            if (collidedActor is CircleCollider)
            {
                CircleCollider other = collidedActor as CircleCollider;
                if ((other.GlobalPosition - GlobalPosition).Magnitude <= other.Radius + Radius)
                    return true;
                else
                    return false;
            }
            else
            {
                throw new NotImplementedException();
            }
        }
    }
}
