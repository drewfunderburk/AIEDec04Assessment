using System;
using System.Collections.Generic;
using System.Text;
using MathLibrary;
using Raylib_cs;

namespace AIEDec042020Assessment
{
    /// <summary>
    /// Enemy that patrols back and forth on the X axis
    /// </summary>
    class PatrolEnemy : Enemy
    {
        // Distance to patrol
        public float PatrolDistance { get; set; }
        // Starting X position
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
            // These calculations are not mathmatically correct for the behavior I want,
            // But these values if not changed produce the desired effect.
            Velocity = ((PatrolDistance * 2)  * (float)Math.Cos(GlobalPosition.Y / (PatrolDistance / 2)), Speed);

            // Send to top again if it goes off the screen
            if (GlobalPosition.Y > Raylib.GetScreenHeight() + 40)
                LocalPosition = (_startingX, -40);

            base.Update(deltaTime);
        }
        #endregion
    }
}
