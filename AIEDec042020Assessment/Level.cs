using System;
using System.Collections.Generic;
using System.Text;

namespace AIEDec042020Assessment
{
    class Level : Scene
    {
        public Level() : base() { }

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

            // Add enemies
        }
        #endregion
    }
}
