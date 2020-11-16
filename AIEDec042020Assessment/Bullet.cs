using System;
using System.Collections.Generic;
using System.Text;
using MathLibrary;
using Raylib_cs;

namespace AIEDec042020Assessment
{
    class Bullet : Actor
    {
        private System.Diagnostics.Stopwatch _stopwatch = new System.Diagnostics.Stopwatch();

        public float DespawnTime { get; set; } = 10000;

        #region CONSTRUCTORS
        public Bullet(Vector2 position, float rotation) : base(position, rotation) 
        {
            Speed = 2000;
            _collisionRadius = 5;
            _collisionMask = new Type[0];
        }
        public Bullet(Vector2 position, float rotation, Type[] types) : this(position, rotation)
        {
            _collisionMask = types;
        }
        #endregion
        public override bool OnCollision(Actor other)
        {
            if (!base.OnCollision(other))
                return false;

            WillDestroy = true;
            return true;
        }

        #region CORE
        public override void Start()
        {
            base.Start();
            _stopwatch.Start();
        }
        public override void Update(float deltaTime)
        {
            if (_stopwatch.ElapsedMilliseconds > DespawnTime)
                WillDestroy = true;

            Velocity = Forward * Speed;
            base.Update(deltaTime);
        }
        #endregion
    }
}
