using System;
using System.Collections.Generic;
using System.Text;
using MathLibrary;
using Raylib_cs;

namespace AIEDec042020Assessment
{
    class PowerUp : Actor
    {
        private float _scaleAmount;
        public PowerUp(Vector2 position, ActorID ID = ActorID.POWER_UP) : base(position, (float)Math.PI / 2) { this.ID = ID; }

        public override bool OnCollision(Actor other)
        {
            base.OnCollision(other);
            switch (other.ID)
            {
                // Power up player
                case ActorID.PLAYER:
                    other.SetScale(other.Scale + (0, _scaleAmount));
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
            _sprite = new Sprite("Sprites/Power_Up.png");
            _scaleAmount = (float)new Random().Next(1, 2);
            Velocity = (0, new Random().Next(10, 100));
        }
        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);
        }
        #endregion
    }
}
