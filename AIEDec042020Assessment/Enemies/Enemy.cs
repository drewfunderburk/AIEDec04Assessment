using System;
using System.Collections.Generic;
using System.Text;
using MathLibrary;
using Raylib_cs;

namespace AIEDec042020Assessment
{
    class Enemy : Actor
    {
        private float _fireDelay;
        private System.Diagnostics.Stopwatch _timer = new System.Diagnostics.Stopwatch();

        /// <summary>
        /// Enemy will constantly LookAt this actor
        /// </summary>
        public Actor Target { get; set; }

        #region CONSTRUCTORS
        public Enemy(Vector2 position, float rotation = 0) : base(position, rotation) { ID = ActorID.ENEMY; }
        public Enemy(float x, float y, float rotation = 0) : this(new Vector2(x, y), rotation) { }
        #endregion
        public override bool OnCollision(Actor other)
        {
            switch (other.ID)
            {
                // If other is the player, destroy this enemy
                case ActorID.PLAYER:
                    WillDestroy = true;
                    return true;
                // If other is the player's bullet, destroy this enemy
                case ActorID.PLAYER_BULLET:
                    WillDestroy = true;
                    return true;
                // Do nothing on collision by default
                default:
                    break;
            }
            return false;
        }

        /// <summary>
        /// Attempt to fire a bullet
        /// </summary>
        protected void Shoot()
        {
            // Check that enough time has passed for another shot
            if (_timer.ElapsedMilliseconds > _fireDelay)
            {
                // Restart shot timer
                _timer.Restart();

                // Create new bullet
                Bullet bullet = Instantiate(
                    new Bullet(
                        GlobalPosition,
                        RotationAngle, (1, 1), ActorID.ENEMY_BULLET)) as Bullet;
                bullet.Speed = 200;

                // Set the bullet's collider
                if (bullet._colliders.Count > 0)
                {
                    if (bullet._colliders[0] is CircleCollider)
                    {
                        CircleCollider collider = bullet._colliders[0] as CircleCollider;
                        collider.Radius = 10;
                    }
                }
            }
        }
        #region CORE
        public override void Start()
        {
            base.Start();
            // Start firing timer
            _timer.Start();
            
            // Assign a fire delay within a random range so enemies don't all fire at once
            Random rand = new Random();
            _fireDelay = 1000 + (rand.Next(-500, 500));

            // Add a circle collider to this actor
            AddCollider(new CircleCollider((0, 0), 20));
        }
        public override void Update(float deltaTime)
        {
            // Look at Target
            if (Target != null)
                LookAt(Target.GlobalPosition);

            // Attempt to fire
            Shoot();

            base.Update(deltaTime);
        }
        #endregion
    }
}
