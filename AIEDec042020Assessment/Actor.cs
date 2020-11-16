using System;
using System.Collections.Generic;
using System.Text;
using MathLibrary;
using Raylib_cs;

namespace AIEDec042020Assessment
{
    class Actor
    {
        public bool Started { get; set; }
        public bool WillDestroy { get; set; }

        private Sprite _sprite;

        private Actor _parent;
        private Actor[] _children;

        private Matrix3 _globalTransform = new Matrix3();
        private Matrix3 _localTransform = new Matrix3();
        private Matrix3 _translation = new Matrix3();
        private Matrix3 _rotation = new Matrix3();
        private Matrix3 _scale = new Matrix3();

        private float _collisionRadius;

        private float RotationAngle { get; set; } = 0;

        public float Speed { get; set; }
        // X-axis forward
        public Vector2 Forward
        { get { return new Vector2(_localTransform.m11, _localTransform.m21).Normalized; } }

        public Vector2 GlobalPosition
        { get { return new Vector2(_globalTransform.m13, _globalTransform.m23); } }

        public Vector2 LocalPosition
        {
            get { return new Vector2(_localTransform.m13, _localTransform.m23); }
            set { SetTranslation(value); }
        }

        public Vector2 Velocity { get; set; }

        #region CONSTRUCTORS
        public Actor(Vector2 position, float rotation = 0)
        {
            LocalPosition = position;
            Velocity = new Vector2();
            RotationAngle = rotation;
            _children = new Actor[0];
            _collisionRadius = 20;

            UpdateTransform();
        }

        public Actor(float x, float y, float rotation = 0) : this(new Vector2(x, y), rotation) { }
        #endregion
        #region TRANSFORMATION
        public void SetTranslation(Vector2 position)
        {
            _translation = Matrix3.CreateTranslation(position);
        }

        public void SetRotation(float radians)
        {
            RotationAngle = radians;
            _rotation = Matrix3.CreateRotation(radians);
        }

        public void Rotate(float radians)
        {
            RotationAngle += radians;
            _rotation *= Matrix3.CreateRotation(radians);
        }

        public void SetScale(float x, float y)
        {
            _scale = Matrix3.CreateScale(new Vector2(x, y));
        }

        public void LookAt(Vector2 position)
        {
            // Find the direction to look at
            Vector2 direction = (position - LocalPosition).Normalized;

            // Get dotproduct between forward and direction to look
            float dotProduct = Vector2.DotProduct(Forward, direction);

            // If actor is already facing that direction, return
            if (dotProduct >= 1)
                return;

            // Get angle to face
            float angle = (float)Math.Acos(dotProduct);

            // Get perpendicular vector to direction
            Vector2 perpVector = new Vector2(-direction.Y, direction.X);

            // Get dotproduct between forward and perpendicular vector
            float perpDotProduct = Vector2.DotProduct(perpVector, Forward);

            // Negate angle if perDotProduct is negative
            if (perpDotProduct != 0)
                angle *= perpDotProduct / Math.Abs(perpDotProduct);

            Rotate(angle);
        }

        protected void UpdateGlobalTransform()
        {
            if (_parent != null)
            {
                _globalTransform = _parent._globalTransform * _localTransform;
            }
            else
            {
                _globalTransform = _localTransform;
            }

            for (int i = 0; i < _children.Length; i++)
            {
                _children[i].UpdateGlobalTransform();
            }
        }

        private void UpdateTransform()
        {
            UpdateGlobalTransform();
            _localTransform = _translation * _rotation * _scale;
            for (int i = 0; i < _children.Length; i++)
            {
                _children[i]._globalTransform = _localTransform;
            }
        }
        #endregion
        #region COLLISION
        public bool CheckCollision(Actor other)
        {
            float distance = (other.GlobalPosition - this.GlobalPosition).Magnitude;
            if (distance < other._collisionRadius + _collisionRadius)
                return true;
            else
                return false;
        }

        public virtual void OnCollision(Actor other)
        {
            // Check if objects are really collided
            if (!CheckCollision(other))
                return;
        }
        #endregion
        #region CORE
        public virtual void Start()
        {
            Started = true;
        }

        public virtual void Update(float deltaTime)
        {
            //Increase position by the current velocity
            LocalPosition += (Velocity * Speed) * deltaTime;

            // Update Transform
            UpdateTransform();
        }

        public virtual void Draw()
        {
            Raylib.DrawCircleLines(
                (int)GlobalPosition.X,
                (int)GlobalPosition.Y,
                _collisionRadius, Color.GREEN);

            Raylib.DrawLine(
               (int)GlobalPosition.X,
               (int)GlobalPosition.Y,
               (int)(GlobalPosition.X + (Forward.X * 50)),
               (int)(GlobalPosition.Y + (Forward.Y * 50)),
               Color.RED);

            if (_sprite != null)
                _sprite.Draw(_globalTransform);
        }

        public virtual void End()
        {
            Started = false;
        }
        #endregion
    }
}
