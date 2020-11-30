using System;
using System.Collections.Generic;
using System.Text;
using MathLibrary;
using Raylib_cs;

namespace AIEDec042020Assessment
{
    /// <summary>
    /// Projectile to be fired
    /// </summary>
    class Bullet : Actor
    {
        // Timer used for auto-destroy
        private System.Diagnostics.Stopwatch _timer = new System.Diagnostics.Stopwatch();

        /// <summary>
        /// Maximum time this bullet should live
        /// </summary>
        public float DespawnTime { get; set; } = 5000;

        #region CONSTRUCTORS
        public Bullet(Vector2 position, float rotation, Vector2 scale, ActorID ID) : base(position, rotation) 
        {
            Speed = 2000;
            this.ID = ID;
            SetScale(scale.X, scale.Y);
        }
        #endregion
        public override bool OnCollision(Actor other)
        {
            switch (other.ID)
            {
                // If other is a player and this bullet is an enemy bullet, destroy it
                case ActorID.PLAYER:
                    if (ID == ActorID.ENEMY_BULLET)
                        WillDestroy = true;
                    return true;
                // If other is an enemy and this bullet is a player bullet, destroy it
                case ActorID.ENEMY:
                    if (ID == ActorID.PLAYER_BULLET)
                        WillDestroy = true;
                    return true;
                // If other is a player bullet and this is an enemy bullet, destroy it
                case ActorID.PLAYER_BULLET:
                    if (ID == ActorID.ENEMY_BULLET)
                        WillDestroy = true;
                    return true;
                // If other is an enemy bullet and this is a player bullet, destroy it
                case ActorID.ENEMY_BULLET:
                    if (ID == ActorID.PLAYER_BULLET)
                        WillDestroy = true;
                    return true;
                // Do nothing on collision by default
                default:
                    break;
            }
            return false;
        }

        #region CORE
        public override void Start()
        {
            base.Start();
            _timer.Start();

            // Set sprite based on what type of bullet this is
            if (ID == ActorID.ENEMY_BULLET)
                _sprite = new Sprite("Sprites/Enemy_Bullet.png");
            else if (ID == ActorID.PLAYER_BULLET)
                _sprite = new Sprite("Sprites/Player_Bullet.png");

            _sprite.Scale = 2;

            // Add collider
            AddCollider(new CircleCollider((0, 0), 5));
        }
        public override void Update(float deltaTime)
        {
            // Check if DespawnTime has been reached
            if (_timer.ElapsedMilliseconds > DespawnTime)
                WillDestroy = true;

            // Move bullet forward
            Velocity = Forward * Speed;
            base.Update(deltaTime);
        }
        #endregion
    }
}
