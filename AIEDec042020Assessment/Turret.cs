using System;
using System.Collections.Generic;
using System.Text;
using MathLibrary;
using Raylib_cs;

namespace AIEDec042020Assessment
{
    class Turret : Actor
    {
        private float _fireDelay;
        private System.Diagnostics.Stopwatch _timer = new System.Diagnostics.Stopwatch();

        public Turret(Vector2 position, float rotation) : base(position, rotation) { ID = ActorID.PLAYER; }

        public override bool OnCollision(Actor other)
        {
            if (!base.OnCollision(other))
                return false;
            Player parent = _parent as Player;
            switch (other.ID)
            {
                case ActorID.ENEMY:
                    parent.TakeDamage(1);
                    return true;
                case ActorID.ENEMY_BULLET:
                    parent.TakeDamage(1);
                    return true;
                default:
                    break;
            }

            return false;
        }

        public override void Start()
        {
            base.Start();
            _timer.Start();
            _fireDelay = 400;
        }

        public override void Update(float deltaTime)
        {
            // Find a target
            Actor targetActor = null;
            float closestDistance = 1000;
            for (int i = 0; i < Game.GetCurrentScene().NumActors; i++)
            {
                Actor actor = Game.GetCurrentScene().GetActor(i);

                // Check if the actor is an enemy
                if (actor is Enemy)
                {
                    // Find closest enemy
                    if ((actor.GlobalPosition - GlobalPosition).Magnitude < closestDistance)
                    {
                        closestDistance = (actor.GlobalPosition - GlobalPosition).Magnitude;
                        targetActor = actor;
                    }
                }
            }

            // Target actor
            if (targetActor != null)
            {
                LookAt(targetActor.GlobalPosition);
            }
            else
                SetRotation((float)Math.PI / 2);
            Shoot();

            base.Update(deltaTime);
        }

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
                        RotationAngle, (1, 1), ActorID.PLAYER_BULLET)) as Bullet;
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
    }
}
