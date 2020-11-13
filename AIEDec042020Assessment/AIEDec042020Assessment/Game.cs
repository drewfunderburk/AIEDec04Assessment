using System;
using System.Collections.Generic;
using System.Text;
using Raylib_cs;

namespace AIEDec042020Assessment
{
    class Game
    {
        public bool GameOver { get; set; }

        #region CORE
        private void Start()
        {

        }

        private void Update(float deltaTime)
        {

        }

        private void Draw()
        {

        }

        private void End()
        {

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
