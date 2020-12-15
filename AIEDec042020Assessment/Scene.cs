using System;
using System.Collections.Generic;
using System.Text;
using MathLibrary;
using Raylib_cs;

namespace AIEDec042020Assessment
{
    /// <summary>
    /// Holds actors and objects in a contained space
    /// </summary>
    class Scene
    {
        /// <summary>
        /// Whether this scene has been started
        /// </summary>
        public bool Started { get; set; }

        /// <summary>
        /// Number of actors in the scene
        /// </summary>
        public int NumActors { get { return _actors.Length; } }

        /// <summary>
        /// Array of all actors in this scene
        /// </summary>
        protected Actor[] _actors;

        // Transform matrices
        public Matrix3 _globalTransform = new Matrix3();
        public Matrix3 _localTransform = new Matrix3();
        private Matrix3 _translation = new Matrix3();
        private Matrix3 _rotation = new Matrix3();
        private Matrix3 _scale = new Matrix3();

        // Used for camerashake to determine how many times we have moved the scene
        private int _shakeCounter = 0;

        // Whether or not the camera is shaking
        private bool _cameraIsShaking = false;
        public bool CameraIsShaking
        {
            get => _cameraIsShaking;
            set
            {
                // Always restart the shake timer if this variable is set
                if (_timer.IsRunning)
                    _timer.Restart();
                else
                    _timer.Start();

                _cameraIsShaking = value;
            }
        }

        // Timer used for camera shake
        private System.Diagnostics.Stopwatch _timer = new System.Diagnostics.Stopwatch();

        public Scene()
        {
            Started = false;
            _actors = new Actor[0];
        }

        /// <summary>
        /// Simulates camera shake by moving the scene to a series of random points before
        ///  recentering it at the origin
        /// </summary>
        private void ShakeCamera()
        {
            if (_cameraIsShaking)
            {
                // How much shake
                int severity = 30;

                // Randomized x and y positions based on severity
                int x = new Random().Next(-severity, severity);
                int y = new Random().Next(-severity, severity);

                // Delay between shakes
                int delay = 5;

                // How many shakes
                int positions = 5;

                // Check if we are ready for the next position
                if (_timer.ElapsedMilliseconds > _shakeCounter * delay)
                {
                    // Move to the next position
                    SetTranslation((x, y));
                    _shakeCounter++;
                }

                // Check if we have shaken enough times
                if (_shakeCounter >= positions)
                {
                    _shakeCounter = 0;
                    _cameraIsShaking = false;
                    // Reset scene to origin
                    SetTranslation((0, 0));
                }
            }
        }
        #region TRANSFORMATION
        /// <summary>
        /// Sets the scene's position
        /// </summary>
        /// <param name="position">Position</param>
        public void SetTranslation(Vector2 position)
        {
            _translation = Matrix3.CreateTranslation(position);
            UpdateTransform();
        }

        /// <summary>
        /// Updates the scene's transform
        /// </summary>
        protected void UpdateTransform()
        {
            _localTransform = _translation * _rotation * _scale;
        }
        #endregion
        #region ACTOR METHODS
        /// <summary>
        /// Add an actor to the scene
        /// </summary>
        /// <param name="actor">Actor to be added</param>
        public void AddActor(Actor actor)
        {
            //Create a new array with a size one greater than our old array
            Actor[] appendedArray = new Actor[_actors.Length + 1];
            //Copy the values from the old array to the new array
            for (int i = 0; i < _actors.Length; i++)
            {
                appendedArray[i] = _actors[i];
            }
            //Set the last value in the new array to be the actor we want to add
            appendedArray[_actors.Length] = actor;
            //Set old array to hold the values of the new array
            _actors = appendedArray;
        }

        /// <summary>
        /// Remove an actor from the scene
        /// </summary>
        /// <param name="index">Index to remove</param>
        /// <returns></returns>
        public bool RemoveActor(int index)
        {
            //Check to see if the index is outside the bounds of our array
            if (index < 0 || index >= _actors.Length)
            {
                return false;
            }

            bool actorRemoved = false;

            //Create a new array with a size one less than our old array 
            Actor[] newArray = new Actor[_actors.Length - 1];
            //Create variable to access tempArray index
            int j = 0;
            //Copy values from the old array to the new array
            for (int i = 0; i < _actors.Length; i++)
            {
                //If the current index is not the index that needs to be removed,
                //add the value into the old array and increment j
                if (i != index)
                {
                    newArray[j] = _actors[i];
                    j++;
                }
                else
                {
                    actorRemoved = true;
                    if (_actors[i].Started)
                        _actors[i].End();
                }
            }

            //Set the old array to be the tempArray
            _actors = newArray;
            return actorRemoved;
        }

        /// <summary>
        /// Remove an actor from the scene
        /// </summary>
        /// <param name="actor">Actor to remove</param>
        /// <returns></returns>
        public bool RemoveActor(Actor actor)
        {
            //Check to see if the actor was null
            if (actor == null)
            {
                return false;
            }

            bool actorRemoved = false;
            //Create a new array with a size one less than our old array
            Actor[] newArray = new Actor[_actors.Length - 1];
            //Create variable to access tempArray index
            int j = 0;
            //Copy values from the old array to the new array
            for (int i = 0; i < _actors.Length; i++)
            {
                if (actor != _actors[i])
                {
                    newArray[j] = _actors[i];
                    j++;
                }
                else
                {
                    actorRemoved = true;
                    if (actor.Started)
                        actor.End();
                }
            }

            //Set the old array to the new array
            _actors = newArray;
            //Return whether or not the removal was successful
            return actorRemoved;
        }

        public Actor GetActor(int index)
        {
            if (index < 0 || index >= NumActors)
                return null;

            return _actors[index];
        }

        /// <summary>
        /// Removes actors set to be removed
        /// </summary>
        private void DestroyActors()
        {
            for (int i = 0; i < _actors.Length; i++)
            {
                if (_actors[i].WillDestroy)
                    RemoveActor(_actors[i]);
            }
        }
        #endregion
        #region CORE
        /// <summary>
        /// Start the scene
        /// </summary>
        public virtual void Start()
        {
            Started = true;
            _timer.Start();
        }

        /// <summary>
        /// Update the scene and all actors contained in it
        /// </summary>
        /// <param name="deltaTime"></param>
        public virtual void Update(float deltaTime)
        {
            // Update the scene's transform
            UpdateTransform();
            
            // Do camera shake if applicable
            ShakeCamera();

            // Update all actors
            for (int i = 0; i < _actors.Length; i++)
            {
                if (!_actors[i].Started)
                    _actors[i].Start();

                _actors[i].Update(deltaTime);
            }

            // Check for collisions
            for (int i = 0; i < _actors.Length; i++)
            {
                for (int n = 0; n < _actors.Length; n++)
                {
                    // Do not check collision against self
                    if (_actors[i] == _actors[n])
                        continue;

                    if (_actors[i].CheckCollision(_actors[n]))
                    {
                        _actors[i].OnCollision(_actors[n]);
                        _actors[n].OnCollision(_actors[i]);
                    }
                }
            }

            // Clean up actors
            DestroyActors();
        }

        /// <summary>
        /// Draw all actors to the screen
        /// </summary>
        public virtual void Draw()
        {
            for (int i = 0; i < _actors.Length; i++)
            {
                _actors[i].Draw();
            }
        }

        /// <summary>
        /// End the scene and all actors contained in it
        /// </summary>
        public virtual void End()
        {
            for (int i = 0; i < _actors.Length; i++)
            {
                if (_actors[i].Started)
                    _actors[i].End();
            }

            Started = false;
        }
        #endregion
    }
}
