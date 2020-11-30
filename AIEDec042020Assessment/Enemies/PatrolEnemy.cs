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
        // Length of one full period of motion
        public float PatrolPeriod { get; set; } = 400;
        // Starting X position
        private float _startingX;

        /// <summary>
        /// Creates a new PatrolEnemy
        /// </summary>
        /// <param name="position"></param>
        /// <param name="rotation"></param>
        /// <param name="patrolDistance"></param>
        /// <param name="patrolSpeed"></param>
        public PatrolEnemy(Vector2 position, float rotation = 0, float patrolDistance = 100, float patrolPeriod = 2) : base(position, rotation)
        { 
            PatrolDistance = patrolDistance;
            PatrolPeriod = patrolPeriod;
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
            // Formula to move on the x in a sine wave
            // x = A sin(B(y))
            float A = PatrolDistance;
            float B = (2 * (float)Math.PI) / PatrolPeriod;
            float y = GlobalPosition.Y;
            float xOffset = A * (float)Math.Sin(B * y);

            LocalPosition = (_startingX + xOffset, LocalPosition.Y);
            Velocity = (0, Speed);

            // Send to top again if it goes off the screen
            if (GlobalPosition.Y > Raylib.GetScreenHeight() + 40)
                LocalPosition = (_startingX, -40);

            base.Update(deltaTime);
        }
        #endregion
    }
}
