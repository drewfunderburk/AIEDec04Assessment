using System;
using System.Collections.Generic;
using System.Text;
using Raylib_cs;

namespace AIEDec042020Assessment
{
    /// <summary>
    /// Base game class. Contains the game loop.
    /// </summary>
    class Game
    {
        // All scenes in the game
        private static Scene[] _scenes = new Scene[0];

        // Index of the current scene
        private static int _currentSceneIndex;

        // Should the game end
        public static bool GameOver { get; set; }

        public static int CurrentSceneIndex { get => _currentSceneIndex; set => _currentSceneIndex = value; }


        #region SCENE
        /// <summary>
        /// Retrieve a scene
        /// </summary>
        /// <param name="index">Scene index</param>
        /// <returns></returns>
        public static Scene GetScene(int index)
        {
            // Return a new scene if index is invalid
            if (index < 0 || index >= _scenes.Length)
                return new Scene();

            return _scenes[index];
        }

        /// <summary>
        /// Get the current scene
        /// </summary>
        /// <returns></returns>
        public static Scene GetCurrentScene()
        {
            return _scenes[_currentSceneIndex];
        }

        /// <summary>
        /// Add a scene to the game
        /// </summary>
        /// <param name="scene">Scene to be added</param>
        /// <returns></returns>
        public static int AddScene(Scene scene)
        {
            //If the scene is null then return before running any other logic
            if (scene == null)
                return -1;

            //Create a new temporary array that one size larger than the original
            Scene[] tempArray = new Scene[_scenes.Length + 1];

            //Copy values from old array into new array
            for (int i = 0; i < _scenes.Length; i++)
            {
                tempArray[i] = _scenes[i];
            }

            //Store the current index
            int index = _scenes.Length;

            //Sets the scene at the new index to be the scene passed in
            tempArray[index] = scene;

            //Set the old array to the tmeporary array
            _scenes = tempArray;

            // Return scene index
            return index;
        }

        /// <summary>
        /// Remove a scene from the game
        /// </summary>
        /// <param name="scene">Scene to be removed</param>
        /// <returns></returns>
        public static bool RemoveScene(Scene scene)
        {
            //If the scene is null then return before running any other logic
            if (scene == null)
                return false;

            bool sceneRemoved = false;

            //Create a new temporary array that is one less than our original array
            Scene[] tempArray = new Scene[_scenes.Length - 1];

            //Copy all scenes except the scene we don't want into the new array
            int j = 0;
            for (int i = 0; i < _scenes.Length; i++)
            {
                if (tempArray[i] != scene)
                {
                    tempArray[j] = _scenes[i];
                    j++;
                }
                else
                {
                    sceneRemoved = true;
                }
            }

            //If the scene was successfully removed set the old array to be the new array
            if (sceneRemoved)
                _scenes = tempArray;

            // Return whether or not operation was successful
            return sceneRemoved;
        }

        /// <summary>
        /// Set the current scene
        /// </summary>
        /// <param name="index">Index of scene</param>
        public static void SetCurrentScene(int index)
        {
            //If the index is not within the range of the the array return
            if (index < 0 || index >= _scenes.Length)
                return;

            //Call end for the previous scene before changing to the new one
            if (_scenes[_currentSceneIndex].Started)
                _scenes[_currentSceneIndex].End();

            //Update the current scene index
            _currentSceneIndex = index;
        }
        #endregion

        #region CORE
        /// <summary>
        /// Runs when the game starts
        /// </summary>
        private void Start()
        {
            // Init Raylib window
            Raylib.InitWindow(1024, 720, "AIEDec042020Assessment");
            Raylib.SetTargetFPS(60);

            // Create a new scene
            AddScene(new Level1());
            SetCurrentScene(0);

            // Spawn player in the center bottom of the screen
            Player player = Actor.Instantiate(new Player(
                Raylib.GetScreenWidth() / 2, 
                Raylib.GetScreenHeight() * 0.95f, 
                (float)Math.PI / 2)) as Player;
            player.Speed = 40;
        }

        /// <summary>
        /// Runs every frame. Used for game logic
        /// </summary>
        /// <param name="deltaTime">Duratin of last frame</param>
        private void Update(float deltaTime)
        {
            // Start the current scene if it isn't already
            if (!GetCurrentScene().Started)
                GetCurrentScene().Start();

            // Update current scene
            GetCurrentScene().Update(deltaTime);
        }

        /// <summary>
        /// Runs every frame. Used for drawing to the screen
        /// </summary>
        private void Draw()
        {
            // Begin Raylib Drawing
            Raylib.BeginDrawing();

            // Clear the screen
            Raylib.ClearBackground(Color.RAYWHITE);


            // Draw the current scene to the screen
            GetCurrentScene().Draw();

            // Draw Debug info
            Raylib.DrawFPS(Raylib.GetScreenWidth() -100, 10);
            Raylib.DrawText("Actors: " + GetCurrentScene().NumActors.ToString(), 10, 10, 16, Color.GREEN);

            // End Raylib Drawing
            Raylib.EndDrawing();
        }

        private void End()
        {
            // End the current scene
            GetCurrentScene().End();
        }

        /// <summary>
        /// Game Loop
        /// </summary>
        public void Run()
        {
            Start();
            // Game Loop
            while(!GameOver && !Raylib.WindowShouldClose())
            {
                float deltaTime = Raylib.GetFrameTime();
                Update(deltaTime);
                Draw();
            }
            End();
        }
        #endregion
    }
}
