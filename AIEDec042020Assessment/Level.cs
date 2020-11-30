using System;
using System.Collections.Generic;
using System.Text;
using MathLibrary;
using Raylib_cs;

namespace AIEDec042020Assessment
{
    class Level : Scene
    {
        protected System.Diagnostics.Stopwatch _timer;
        protected Player _player;

        public Level() : base() { }

        /// <summary>
        /// Get the number of enemies remaining in the scene
        /// </summary>
        /// <returns></returns>
        public int GetRemainingEnemies()
        {
            int count = 0;
            for (int i = 0; i < _actors.Length; i++)
            {
                if (_actors[i] is Enemy)
                    count++;
            }
            return count;
        }

        #region CORE
        public override void Start()
        {
            base.Start();
            // Get player
            foreach (Actor actor in _actors)
            {
                if (actor != null)
                {
                    if (actor is Player)
                    {
                        _player = actor as Player;
                        break;
                    }
                }
            }
            if (_player == null)
                throw new NullReferenceException();

            _timer = new System.Diagnostics.Stopwatch();
            _timer.Start();
        }
        #endregion
    }
}
