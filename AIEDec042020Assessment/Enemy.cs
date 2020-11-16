using System;
using System.Collections.Generic;
using System.Text;
using MathLibrary;
using Raylib_cs;

namespace AIEDec042020Assessment
{
    class Enemy : Actor
    {
        private float _fireDelay = 1000;
        private System.Diagnostics.Stopwatch _stopwatch = new System.Diagnostics.Stopwatch();

        public Actor Target { get; set; }

        #region CONSTRUCTORS
        public Enemy(Vector2 position, float rotation = 0) : base(position, rotation) { }
        public Enemy(float x, float y, float rotation = 0) : this(new Vector2(x, y), rotation) { }
        #endregion
        #region CORE
        public override void Start()
        {
            base.Start();
            _stopwatch.Start();
        }
        public override void Update(float deltaTime)
        {
            // Look at Target
            if (Target != null)
                LookAt(Target.GlobalPosition);

            if (_stopwatch.ElapsedMilliseconds > _fireDelay)
            {
                _stopwatch.Restart();
                Bullet bullet = Instantiate(
                    new Bullet(
                        GlobalPosition, 
                        RotationAngle, 
                        new Type[] { typeof(Player) })) as Bullet;
                bullet.Speed = 200;
                bullet._collisionRadius = 10;
            }

            base.Update(deltaTime);
        }

        public override bool OnCollision(Actor other)
        {
            if (!base.OnCollision(other))
                return false;
            

            // Randomize Location
            Random rand = new Random();
            float randX = (float)rand.NextDouble() * Raylib.GetScreenWidth();
            float randY = (float)rand.NextDouble() * Raylib.GetScreenHeight();
            SetTranslation(new Vector2(randX, randY));
            return true;
        }
        #endregion
    }
}
