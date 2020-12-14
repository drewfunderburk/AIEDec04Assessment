using System;
using System.Collections.Generic;
using System.Text;
using MathLibrary;
using Raylib_cs;

namespace AIEDec042020Assessment
{
    /// <summary>
    /// Base class for all objects used in this game
    /// </summary>
    class Actor
    {
        /// <summary>
        /// Used by collision to determine what sort of actor this is
        /// </summary>
        public enum ActorID
        {
            PLAYER,
            ENEMY,
            PLAYER_BULLET,
            ENEMY_BULLET,
        }

        /// <summary>
        /// Whether or not this actor has been started
        /// </summary>
        public bool Started { get; set; }

        /// <summary>
        /// Whether or not this actor will be destroyed on the next update
        /// </summary>
        public bool WillDestroy { get; set; }

        /// <summary>
        /// This actor's sprite
        /// </summary>
        public Sprite _sprite;

        /// <summary>
        /// This actor's parent
        /// </summary>
        public Actor _parent;

        // This actor's children
        private Actor[] _children;

        // Transform matrices
        private Matrix3 _globalTransform = new Matrix3();
        private Matrix3 _localTransform = new Matrix3();
        private Matrix3 _translation = new Matrix3();
        private Matrix3 _rotation = new Matrix3();
        private Matrix3 _scale = new Matrix3();

        /// <summary>
        /// This actor's colliders
        /// </summary>
        public List<Collider> _colliders = new List<Collider>();


        /// <summary>
        /// ID to identify what type of actor this is. Defaults to Enemy
        /// </summary>
        public ActorID ID = ActorID.ENEMY;

        #region PROPERTIES
        /// <summary>
        /// This actor's scale
        /// </summary>
        public Vector2 Scale { get; protected set; } = (1, 1);

        /// <summary>
        /// This actor's rotation angle
        /// </summary>
        public float RotationAngle { get; private set; } = 0;

        /// <summary>
        /// This actor's rate of acceleration
        /// </summary>
        public float Speed { get; set; } = 100;

        /// <summary>
        /// This actor's top speed
        /// </summary>
        public float MaxSpeed { get; set; } = 300;

        /// <summary>
        /// X-axis forward
        /// </summary>
        public Vector2 Forward
        { get { return new Vector2(_localTransform.m11, _localTransform.m21).Normalized; } }

        /// <summary>
        /// Position relative to global 0, 0
        /// </summary>
        public Vector2 GlobalPosition
        { get { return new Vector2(_globalTransform.m13, _globalTransform.m23); } }

        /// <summary>
        /// Position relative to parent
        /// </summary>
        public Vector2 LocalPosition
        {
            get { return new Vector2(_localTransform.m13, _localTransform.m23); }
            set { SetTranslation(value); }
        }

        /// <summary>
        /// This actor's current velocity
        /// </summary>
        public Vector2 Velocity { get; set; }

        /// <summary>
        /// This actor's current acceleration
        /// </summary>
        protected Vector2 Acceleration { get; set; }
        #endregion
        #region CONSTRUCTORS
        public Actor(Vector2 position, float rotation = 0)
        {
            LocalPosition = position;
            Velocity = new Vector2();
            Acceleration = new Vector2();
            SetRotation(rotation);
            _children = new Actor[0];
        }

        public Actor(float x, float y, float rotation = 0) : this(new Vector2(x, y), rotation) { }
        #endregion
        #region CHILDREN
        /// <summary>
        /// Add a child to this actor
        /// </summary>
        /// <param name="child">The actor to be added</param>
        /// <returns></returns>
        public bool AddChild(Actor child)
        {
            if (child == null)
                return false;

            Actor[] tempArray = new Actor[_children.Length + 1];

            for (int i = 0; i < _children.Length; i++)
            {
                tempArray[i] = _children[i];
            }
            tempArray[_children.Length] = child;
            _children = tempArray;
            child._parent = this;
            return true;
        }

        /// <summary>
        /// Removes a child from this actor
        /// </summary>
        /// <param name="child">The actor to be removed</param>
        /// <returns></returns>
        public bool RemoveChild(Actor child)
        {
            if (child == null)
                return false;

            Actor[] tempArray = new Actor[_children.Length - 1];
            bool childRemoved = false;

            int j = 0;
            for (int i = 0; i < tempArray.Length; i++)
            {
                if (_children[i] != child)
                {
                    tempArray[j] = _children[i];
                    j++;
                }
                else
                {
                    childRemoved = true;
                }
            }

            _children = tempArray;
            child._parent = null;
            return childRemoved;
        }
        #endregion
        #region UTILITY
        /// <summary>
        /// Adds an actor to the scene
        /// </summary>
        /// <param name="actor">The actor to be added</param>
        /// <returns></returns>
        public static Actor Instantiate(Actor actor)
        {
            Game.GetCurrentScene().AddActor(actor);
            return actor;
        }

        /// <summary>
        /// Removes an actor from the scene
        /// </summary>
        /// <param name="actor">The actor to be removed</param>
        public static void Destroy(Actor actor)
        {
            Game.GetCurrentScene().RemoveActor(actor);
        }
        
        /// <summary>
        /// Adds a collider to this actor
        /// </summary>
        /// <param name="collider">The collider to be added</param>
        public void AddCollider(Collider collider)
        {
            Instantiate(collider);
            AddChild(collider);
            _colliders.Add(collider);
        }

        /// <summary>
        /// Removes a collider from this actor
        /// </summary>
        /// <param name="collider">The collider to be removed</param>
        public void RemoveCollider(Collider collider)
        {
            _colliders.Remove(collider);
            RemoveChild(collider);
        }
        #endregion
        #region TRANSFORMATION
        /// <summary>
        /// Sets the actor's local tranlation to the given position
        /// </summary>
        /// <param name="position">Position</param>
        public void SetTranslation(Vector2 position)
        {
            _translation = Matrix3.CreateTranslation(position);
            UpdateTransform();
        }

        /// <summary>
        /// Sets the actor's local rotation to the given value
        /// </summary>
        /// <param name="radians">Rotation value in radians</param>
        public void SetRotation(float radians)
        {
            // Update RotationAngle so that this angle can be retrieved easily
            RotationAngle = radians;
            _rotation = Matrix3.CreateRotation(radians);
            UpdateTransform();
        }

        /// <summary>
        /// Rotate the actor's local transform by the given value
        /// </summary>
        /// <param name="radians">Amount to rotate in radians</param>
        public void Rotate(float radians)
        {
            RotationAngle += radians;
            _rotation *= Matrix3.CreateRotation(radians);
            UpdateTransform();
        }

        /// <summary>
        /// Sets the actor's local scale to the given value
        /// </summary>
        /// <param name="x">Value to scale on the X-Axis</param>
        /// <param name="y">Value to scale on the Y-Axis</param>
        public void SetScale(float x, float y)
        {
            _scale = Matrix3.CreateScale(new Vector2(x, y));
            Scale = (x, y);
            UpdateTransform();
        }

        /// <summary>
        /// Sets the actor's local scale to the given value
        /// </summary>
        /// <param name="scale">Value to scale by</param>
        public void SetScale(Vector2 scale)
        { SetScale(scale.X, scale.Y); }

        /// <summary>
        /// Rotate this actor to face a position
        /// </summary>
        /// <param name="position">Position to face</param>
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

        /// <summary>
        /// Update this actor's global transform and it's children
        /// </summary>
        protected void UpdateGlobalTransform()
        {
            if (_parent != null)
            {
                _globalTransform = _parent._globalTransform * _localTransform;
            }
            else
            {
                _globalTransform = Game.GetCurrentScene()._localTransform * _localTransform;
            }

            if (_children != null)
            {
                for (int i = 0; i < _children.Length; i++)
                {
                    if (_children[i] != null)
                        _children[i].UpdateGlobalTransform();
                }
            }
        }

        /// <summary>
        /// Update this actor's local transform and the global transform's of it's children
        /// </summary>
        protected void UpdateTransform()
        {
            UpdateGlobalTransform();
            _localTransform = _translation * _rotation * _scale;
            if (_children != null)
            {
                for (int i = 0; i < _children.Length; i++)
                {
                    if (_children[i] != null)
                        _children[i]._globalTransform = _localTransform;
                }
            }
        }
        #endregion
        #region COLLISION
        /// <summary>
        /// Check if this actor has collided with another
        /// </summary>
        /// <param name="other">Other actor</param>
        /// <returns></returns>
        public bool CheckCollision(Actor other)
        {
            // Check all of this Actor's colliders against the other Actor's colliders
            if (_colliders.Count == 0 || other._colliders.Count == 0)
                return false;

            for (int i = 0; i < _colliders.Count; i++)
            {
                for (int n = 0; n < other._colliders.Count; n++)
                {
                    if (other._colliders[n].IsCollided(this._colliders[i]))
                        return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Actions to perform if a collision is detected
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public virtual bool OnCollision(Actor other)
        {
            // Do not collide if object is set to be destroyed
            if (other.WillDestroy)
                return false;

            // Check if objects are really collided
            if (!CheckCollision(other))
                return false;

            // Returns a boolean for children to easily check if these base conditions are satisfied
            return true;
        }
        #endregion
        #region CORE
        /// <summary>
        /// Called when this actor is started
        /// </summary>
        public virtual void Start()
        {
            Started = true;
            _children = new Actor[0];
        }

        /// <summary>
        /// Called every frame. Updates actor logic
        /// </summary>
        /// <param name="deltaTime">Duration of last frame</param>
        public virtual void Update(float deltaTime)
        {
            //Increase position by the current velocity
            Velocity += Acceleration;
            if (Velocity.Magnitude > MaxSpeed)
                Velocity = Velocity.Normalized * MaxSpeed;

            // Update local position
            LocalPosition += Velocity * deltaTime;

            UpdateTransform();
        }

        /// <summary>
        /// Called every frame. Draws actor to the screen
        /// </summary>
        public virtual void Draw()
        {
            // Draw facing line
            Raylib.DrawLine(
               (int)GlobalPosition.X,
               (int)GlobalPosition.Y,
               (int)(GlobalPosition.X + (Forward.X * 50)),
               (int)(GlobalPosition.Y + (Forward.Y * 50)),
               Color.RED);

            // Draw Sprite
            if (_sprite != null)
                _sprite.Draw(_globalTransform);
        }

        /// <summary>
        /// Called when an actor is destroyed
        /// </summary>
        public virtual void End()
        {
            Started = false;
            WillDestroy = true;
            if (_parent != null)
                _parent.RemoveChild(this);

            for (int i = 0; i < _children.Length; i++)
            {
                _children[i].End();
            }
        }
        #endregion
    }
}
