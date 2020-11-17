using System;
using System.Collections.Generic;
using System.Text;
using MathLibrary;
using Raylib_cs;

namespace AIEDec042020Assessment
{
    class HomingEnemy : Enemy
    {
        public HomingEnemy(Vector2 position, float rotation = 0) : base(position, rotation) { }

        #region CORE
        public override void Start()
        {
            base.Start();
            _sprite = new Sprite("Sprites/Enemy_Ship_1.png");
        }

        public override void Update(float deltaTime)
        {
            Velocity = Forward * Speed;
            base.Update(deltaTime);
        }
        #endregion
    }
}
