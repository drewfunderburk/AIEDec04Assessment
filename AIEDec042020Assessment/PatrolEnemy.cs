using System;
using System.Collections.Generic;
using System.Text;
using MathLibrary;
using Raylib_cs;

namespace AIEDec042020Assessment
{
    class PatrolEnemy : Enemy
    {
        public float PatrolDistance { get; set; }
        private float _startingX;

        public PatrolEnemy(Vector2 position, float rotation = 0, float patrolDistance = 100) : base(position, rotation)
        { 
            PatrolDistance = patrolDistance;
            _startingX = position.X;
        }

        #region CORE
        public override void Start()
        {
            base.Start();
            _sprite = new Sprite("Sprites/Enemy_Ship_2.png");
        }
        public override void Update(float deltaTime)
        {
            /*
            // Reverse X direction when reaching end of patrol
            if (Math.Abs(GlobalPosition.X - _startingX) > PatrolDistance)
                Velocity.X *= -1;
            */

            Velocity = ((PatrolDistance * 2)  * (float)Math.Cos(GlobalPosition.Y / (PatrolDistance / 2)), Speed);

            // Send to top again if it goes off the screen
            if (GlobalPosition.Y > Raylib.GetScreenHeight() + _collisionRadius)
                LocalPosition = (_startingX, -_collisionRadius);

            base.Update(deltaTime);
        }
        #endregion
    }
}
