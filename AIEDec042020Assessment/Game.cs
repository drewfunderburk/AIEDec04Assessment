using System;
using System.Collections.Generic;
using System.Text;
using Raylib_cs;

namespace AIEDec042020Assessment
{
    class Game
    {
        public Scene scene;

        public bool GameOver { get; set; }


        #region CORE
        private void Start()
        {
            scene = new Scene();
        }

        private void Update(float deltaTime)
        {
            if (!scene.Started)
                scene.Start();

            scene.Update(deltaTime);
        }

        private void Draw()
        {
            scene.Draw();
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
