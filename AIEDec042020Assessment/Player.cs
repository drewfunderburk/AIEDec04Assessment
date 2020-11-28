using System;
using System.Collections.Generic;
using System.Text;
using MathLibrary;
using Raylib_cs;

namespace AIEDec042020Assessment
{
    class Player : Actor
    {
        private float _fireDelay = 150;
        private float _colliderRadius = 20;
        private float _maxSpeed = 500;
        private float _maxHealth = 5;
        private float _health;

        private Vector2 _acceleration = new Vector2();

        // Player screen boundaries
        private float _leftBoundary =   Raylib.GetScreenWidth() * 0.05f;
        private float _rightBoundary =  Raylib.GetScreenWidth() * 0.95f;
        private float _bottomBoundary = Raylib.GetScreenHeight() * 0.95f;
        private float _topBoundary =    Raylib.GetScreenHeight() * 0.3f;

        private System.Diagnostics.Stopwatch _fireRateTimer = new System.Diagnostics.Stopwatch();
        private System.Diagnostics.Stopwatch _iFrameTimer = new System.Diagnostics.Stopwatch();

        private Actor[] _healthBar;

        #region CONSTRUCTORS
        public Player(Vector2 position, float rotation = 0) : base(position, rotation) 
        { 
            _fireRateTimer.Start();
            ID = ActorID.PLAYER;
            _health = _maxHealth;
        }
        public Player(float x, float y, float rotation = 0) : this(new Vector2(x, y), rotation) { }
        #endregion

        public override bool OnCollision(Actor other)
        {
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
        public void TakeDamage(int damage)
        {
            Console.WriteLine("Damage");
            _health = Math.Clamp(_health - damage, 0, _maxHealth);
            if (_health == 0)
                Game.GameOver = true;
        }
        #region CORE
        public override void Start()
        {
            base.Start();
            _sprite = new Sprite("Sprites/Player_Ship.png");
            AddCollider(new CircleCollider((0, 0), _colliderRadius));

            // Init Healthbar
            _healthBar = new Actor[(int)_maxHealth];
            int barWidth = 100;
            int barSpacing = 100;
            for (int i = 0; i < _healthBar.Length; i++)
            {
                _healthBar[i] = Actor.Instantiate(new Actor(0, 0));
                _healthBar[i]._sprite = new Sprite("Sprites/Player_Bullet.png");
                AddChild(_healthBar[i]);

                int pos = ((-barWidth / 2) + barSpacing * (_healthBar[i]._sprite.Width / 2)) * (i + 1);
                _healthBar[i].LocalPosition = (-40, pos);
            }
        }

        public override void Update(float deltaTime)
        {
            //Gets the player's input to determine which direction the actor will move in on each axis 
            int xDirection = -Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_A))
                + Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_D));
            int yDirection = -Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_W))
                + Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_S));
            
            // Shooting
            if (_fireRateTimer.ElapsedMilliseconds > _fireDelay && Raylib.IsMouseButtonDown(MouseButton.MOUSE_LEFT_BUTTON))
            {
                _fireRateTimer.Restart();
                Bullet bullet = Instantiate(new Bullet(GlobalPosition, RotationAngle, Scale, ActorID.PLAYER_BULLET)) as Bullet;
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
            if (Velocity.Magnitude > _maxSpeed)
                Velocity = Velocity.Normalized * _maxSpeed;

            // Clamp drifting
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

        public override void Draw()
        {
            base.Draw();
        }
        #endregion
    }
}
