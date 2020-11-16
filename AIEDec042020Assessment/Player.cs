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

        public float Speed { get; set; }

        #region CONSTRUCTORS
        public Player(Vector2 position, float rotation) : base(position, rotation) { }
        #endregion
        #region CORE
        public override void Start()
        {

        }

        public override void Update(float deltaTime)
        {

        }

        public override void Draw()
        {

        }

        public override void End()
        {

        }
        #endregion
    }
}
