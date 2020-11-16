using System;
using System.Collections.Generic;
using System.Text;
using Raylib_cs;

namespace AIEDec042020Assessment
{
    class Game
    {
        public static Scene scene;

        public bool GameOver { get; set; }

        #region CORE
        private void Start()
        {
            // Init Raylib window
            Raylib.InitWindow(1024, 720, "AIEDec042020Assessment");
            Raylib.SetTargetFPS(60);

            // Create a new scene
            scene = new Scene();

            // Spawn player in the center bottom of the screen
            Player player = new Player(
                Raylib.GetScreenWidth() / 2, 
                Raylib.GetScreenHeight() * 0.95f, 
                (float)Math.PI / 2);
            player.Speed = 300;
            scene.AddActor(player);

            Enemy enemy = new Enemy(Raylib.GetScreenWidth() / 2, Raylib.GetScreenHeight() * 0.2f);
            enemy.Target = player;
            scene.AddActor(enemy);
        }

        private void Update(float deltaTime)
        {
            if (!scene.Started)
                scene.Start();

            scene.Update(deltaTime);
        }

        private void Draw()
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.RAYWHITE);

            scene.Draw();

            Raylib.EndDrawing();
        }

        private void End()
        {
            scene.End();
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
