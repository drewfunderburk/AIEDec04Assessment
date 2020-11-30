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

        public Actor Target { get; set; }

        #region CONSTRUCTORS
        public Enemy(Vector2 position, float rotation = 0) : base(position, rotation) { ID = ActorID.ENEMY; }
        public Enemy(float x, float y, float rotation = 0) : this(new Vector2(x, y), rotation) { }
        #endregion
        public override bool OnCollision(Actor other)
        {
            switch (other.ID)
            {
                // If other is the player, destroy
                case ActorID.PLAYER:
                    WillDestroy = true;
                    return true;
                // Teleport to a random location
                case ActorID.PLAYER_BULLET:
                    WillDestroy = true;
                    return true;
                // Do nothing on collision by default
                default:
                    break;
            }
            return false;
        }
        protected void Shoot()
        {
            if (_stopwatch.ElapsedMilliseconds > _fireDelay)
            if (_timer.ElapsedMilliseconds > _fireDelay)
            {
                // Restart shot timer
                _timer.Restart();
                Bullet bullet = Instantiate(
                    new Bullet(
                        GlobalPosition,
                        RotationAngle, (1, 1), ActorID.ENEMY_BULLET)) as Bullet;
                bullet.Speed = 200;
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
            Random rand = new Random();
            _fireDelay = 1000 + (rand.Next(-500, 500));
            AddCollider(new CircleCollider((0, 0), 20));
        }
        public override void Update(float deltaTime)
        {
            // Look at Target
            if (Target != null)
                LookAt(Target.GlobalPosition);

            Shoot();

            base.Update(deltaTime);
        }
        #endregion
    }
}
