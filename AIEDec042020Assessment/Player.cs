using System;
using System.Collections.Generic;
using System.Text;
using MathLibrary;
using Raylib_cs;

namespace AIEDec042020Assessment
{
    /// <summary>
    /// Class to define the player
    /// </summary>
    class Player : Actor
    {
        // Delay between shots
        private float _fireDelay = 150;

        // Radius of colliders
        private float _colliderRadius = 20;

        // Maximum health
        private float _maxHealth = 5;

        // Current health
        private float _health;

        // Current acceleration
        private Vector2 _acceleration = new Vector2();

        // Player screen boundaries
        private float _leftBoundary =   Raylib.GetScreenWidth() * 0.05f;
        private float _rightBoundary =  Raylib.GetScreenWidth() * 0.95f;
        private float _bottomBoundary = Raylib.GetScreenHeight() * 0.95f;
        private float _topBoundary =    Raylib.GetScreenHeight() * 0.3f;

        // Timer for use in fire rate
        private System.Diagnostics.Stopwatch _fireRateTimer = new System.Diagnostics.Stopwatch();

        #region CONSTRUCTORS
        /// <summary>
        /// Creates a new player
        /// </summary>
        /// <param name="position"></param>
        /// <param name="rotation"></param>
        public Player(Vector2 position, float rotation = 0) : base(position, rotation) 
        { 
            _fireRateTimer.Start();
            ID = ActorID.PLAYER;
            _health = _maxHealth;
            MaxSpeed = 500;
        }
        public Player(float x, float y, float rotation = 0) : this(new Vector2(x, y), rotation) { }
        #endregion

        public override bool OnCollision(Actor other)
        {
            // Check if base conditions are met before continuing
            if (!base.OnCollision(other))
                return false;

            switch (other.ID)
            {
                // Take damage on collision with an enemy
                case ActorID.ENEMY:
                    TakeDamage(1);
                    Game.GetCurrentScene().CameraIsShaking = true;
                    return true;
                case ActorID.ENEMY_BULLET:
                    TakeDamage(1);
                    Game.GetCurrentScene().CameraIsShaking = true;
                    return true;
                // Do nothing on collision by default
                default:
                    break;
            }
            return false;
        }

        /// <summary>
        /// Causes the player to take damage
        /// </summary>
        /// <param name="damage">Amount of damage</param>
        public void TakeDamage(int damage)
        {
            // Ensure health is always between 0 and MaxHealth
            _health = Math.Clamp(_health - damage, 0, _maxHealth);

            // Set game over if health == 0
            if (_health == 0)
                Game.GameOver = true;
        }

        #region CORE
        public override void Start()
        {
            base.Start();

            // Set player sprite
            _sprite = new Sprite("Sprites/Player_Ship.png");

            // Add collider
            AddCollider(new CircleCollider((0, 0), _colliderRadius));

            // Shield
            int radius = 50;
            int numShields = 10;
            for (int i = 0; i < numShields; i++)
            {
                float radians = (float)((Math.PI * 2) / numShields) * i;
                float x = radius * (float)(Math.Cos(radians));
                float y = radius * (float)(Math.Sin(radians));
                Vector2 pos = (x, y);

                Bullet actor = Actor.Instantiate(new Bullet(pos, 0, (2, 2), ActorID.PLAYER_BULLET)) as Bullet;
                actor.Speed = 0;
                actor.DespawnTime = 0;
                AddChild(actor);
            }
        }

        public override void Update(float deltaTime)
        {
            //Gets the player's input to determine which direction the actor will move in on each axis 
            int xDirection = -Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_A))
                + Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_D));
            int yDirection = -Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_W))
                + Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_S));
            
            // Check if we can shoot
            if (_fireRateTimer.ElapsedMilliseconds > _fireDelay && Raylib.IsMouseButtonDown(MouseButton.MOUSE_LEFT_BUTTON))
            {
                // Restart fire timer
                _fireRateTimer.Restart();

                // Create a new bullet
                Bullet bullet = Instantiate(new Bullet(GlobalPosition, RotationAngle, Scale, ActorID.PLAYER_BULLET)) as Bullet;

                // Set the bullet's speed
                bullet.MaxSpeed = 1000;
                bullet.Speed = 1000;
            }

            // Target mouse position
            LookAt((Raylib.GetMouseX(), Raylib.GetMouseY()));

            // Update acceleration and deceleration
            float x, y;
            if (xDirection == 0)
                x = -Velocity.Normalized.X;
            else
                x = xDirection;
            if (yDirection == 0)
                y = -Velocity.Normalized.Y;
            else
                y = yDirection;

            _acceleration = new Vector2(x, y).Normalized * Speed;

            // Update Velocity
            Velocity += _acceleration;
            if (Velocity.Magnitude > MaxSpeed)
                Velocity = Velocity.Normalized * MaxSpeed;

            // Clamp drifting when velocity approaches 0
            if (Math.Abs(Velocity.X) < 2)
                Velocity = (0, Velocity.Y);
            if (Math.Abs(Velocity.Y) < 2)
                Velocity = (Velocity.X, 0);

            // Clamp position to boundary
            if (LocalPosition.X + (Velocity.X * deltaTime) > _rightBoundary - _sprite.Width * Scale.X)
                Velocity = new Vector2(Math.Min(Velocity.X, 0), Velocity.Y);
            if (LocalPosition.X + (Velocity.X * deltaTime) < _leftBoundary + _sprite.Width * Scale.X)
                Velocity = new Vector2(Math.Max(0, Velocity.X), Velocity.Y);
            if (LocalPosition.Y + (Velocity.Y * deltaTime) > _bottomBoundary - _sprite.Height * Scale.Y)
                Velocity = new Vector2(Velocity.X, Math.Min(Velocity.Y, 0));
            if (LocalPosition.Y + (Velocity.Y * deltaTime) < _topBoundary + _sprite.Height * Scale.Y)
                Velocity = new Vector2(Velocity.X, Math.Max(0, Velocity.Y));

            base.Update(deltaTime);
        }
        #endregion
    }
}
