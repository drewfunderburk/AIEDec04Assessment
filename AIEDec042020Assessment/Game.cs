using System;
using System.Collections.Generic;
using System.Text;
using Raylib_cs;

namespace AIEDec042020Assessment
{
    class Game
    {
        private static Scene[] _scenes = new Scene[0];

        private static int _currentSceneIndex;
        public static bool GameOver { get; set; }
        public static int CurrentSceneIndex { get => _currentSceneIndex; set => _currentSceneIndex = value; }


        #region SCENE
        public static Scene GetScene(int index)
        {
            if (index < 0 || index >= _scenes.Length)
                return new Scene();

            return _scenes[index];
        }

        public static Scene GetCurrentScene()
        {
            return _scenes[_currentSceneIndex];
        }

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

            return index;
        }

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

            return sceneRemoved;
        }

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
        private void Start()
        {
            // Init Raylib window
            Raylib.InitWindow(1024, 720, "AIEDec042020Assessment");
            Raylib.SetTargetFPS(60);

            // Create a new scene
            AddScene(new Scene());
            SetCurrentScene(0);

            // Spawn player in the center bottom of the screen
            Player player = Actor.Instantiate(new Player(
                Raylib.GetScreenWidth() / 2, 
                Raylib.GetScreenHeight() * 0.95f, 
                (float)Math.PI / 2)) as Player;
            player.Speed = 40;


            /*
            // Base Enemy
            Enemy enemy = Actor.Instantiate(new Enemy(Raylib.GetScreenWidth() / 2, Raylib.GetScreenHeight() * 0.2f)) as Enemy;
            enemy.Target = player;

            // String of patrol enemies
            for (int i = -2; i < 3; i++)
            {
                PatrolEnemy p = Actor.Instantiate(new PatrolEnemy((Raylib.GetScreenWidth() * 0.75f, -10 - (i * 50)))) as PatrolEnemy;
                p.Target = player;
            }

            // String of Homing Enemy
            for (int i = -2; i < 3; i++)
            {
                HomingEnemy h = Actor.Instantiate(new HomingEnemy(((Raylib.GetScreenWidth() / 2) + (i * 50), -10))) as HomingEnemy;
                h.Target = player;
            }
            for (int i = 0; i < 5; i++)
            {
                PowerUp powerUp = Actor.Instantiate(new PowerUp((Raylib.GetScreenWidth() / 2, -10 + (i * 50)))) as PowerUp;

            }
            */
        }

        private void Update(float deltaTime)
        {
            if (!GetCurrentScene().Started)
                GetCurrentScene().Start();

            GetCurrentScene().Update(deltaTime);
        }

        private void Draw()
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.RAYWHITE);

            GetCurrentScene().Draw();

            Raylib.EndDrawing();
        }

        private void End()
        {
            GetCurrentScene().End();
        }

        public void Run()
        {
            Start();
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
