using System;
using System.Collections.Generic;
using System.Text;
using MathLibrary;
using Raylib_cs;

namespace AIEDec042020Assessment
{
    class Player : Actor
    {
        private float _speed = 10;
        private float _fireDelay = 100;

        private System.Diagnostics.Stopwatch _stopwatch = new System.Diagnostics.Stopwatch();

        #region CONSTRUCTORS
        public Player(Vector2 position, float rotation = 0) : base(position, rotation) { }
        public Player(float x, float y, float rotation = 0) : this(new Vector2(x, y), rotation) { }
        #endregion
        #region CORE
        public override void Start()
        {
            base.Start();
        }

        public override void Update(float deltaTime)
        {
            //Gets the player's input to determine which direction the actor will move in on each axis 
            int xDirection = -Convert.ToInt32(Raylib.IsKeyPressed(KeyboardKey.KEY_A))
                + Convert.ToInt32(Raylib.IsKeyPressed(KeyboardKey.KEY_D));
            int yDirection = -Convert.ToInt32(Raylib.IsKeyPressed(KeyboardKey.KEY_W))
                + Convert.ToInt32(Raylib.IsKeyPressed(KeyboardKey.KEY_W));

            Velocity = new Vector2(xDirection, yDirection);

            base.Update(deltaTime);
        }

        public override void Draw()
        {

            base.Draw();
        }

        public override void End()
        {
            base.End();
        }
        #endregion
    }
}
