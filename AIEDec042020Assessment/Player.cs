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

        // Player screen boundaries
        private float _leftBoundary =   Raylib.GetScreenWidth() * 0.05f;
        private float _rightBoundary =  Raylib.GetScreenWidth() * 0.95f;
        private float _bottomBoundary = Raylib.GetScreenHeight() * 0.95f;
        private float _topBoundary =    Raylib.GetScreenHeight() * 0.3f;

        private System.Diagnostics.Stopwatch _stopwatch = new System.Diagnostics.Stopwatch();

        #region CONSTRUCTORS
        public Player(Vector2 position, float rotation = 0) : base(position, rotation) 
        { _stopwatch.Start(); }
        public Player(float x, float y, float rotation = 0) : this(new Vector2(x, y), rotation) { }
        #endregion
        #region CORE
        public override void Update(float deltaTime)
        {
            //Gets the player's input to determine which direction the actor will move in on each axis 
            int xDirection = -Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_A))
                + Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_D));
            int yDirection = -Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_W))
                + Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_S));

            // Clamp position to boundary
            if (LocalPosition.X + (Velocity.X * deltaTime) > _rightBoundary - _collisionRadius)
                xDirection = Math.Min(xDirection, 0);
            if (LocalPosition.X + (Velocity.X * deltaTime) < _leftBoundary + _collisionRadius)
                xDirection = Math.Max(0, xDirection);
            if (LocalPosition.Y + (Velocity.Y * deltaTime) > _bottomBoundary - _collisionRadius)
                yDirection = Math.Min(yDirection, 0);
            if (LocalPosition.Y + (Velocity.Y * deltaTime) < _topBoundary +  _collisionRadius)
                yDirection = Math.Max(0, yDirection);
            
            // Shooting
            if (_stopwatch.ElapsedMilliseconds > _fireDelay && Raylib.IsMouseButtonDown(MouseButton.MOUSE_LEFT_BUTTON))
            {
                _stopwatch.Restart();
                Instantiate(new Bullet(GlobalPosition, RotationAngle, new Type[] { typeof(Enemy) }));
            }

            // Target mouse position
            LookAt((Raylib.GetMouseX(), Raylib.GetMouseY()));

            // Update velocity
            Velocity = new Vector2(xDirection, yDirection).Normalized * Speed;
            base.Update(deltaTime);
        }

        public override void Draw()
        {

            base.Draw();
        }
        #endregion
    }
}
