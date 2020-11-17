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
            Player player = Actor.Instantiate(new Player(
                Raylib.GetScreenWidth() / 2, 
                Raylib.GetScreenHeight() * 0.95f, 
                (float)Math.PI / 2)) as Player;
            player.Speed = 300;
            player.SetScale(1, 3);

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
