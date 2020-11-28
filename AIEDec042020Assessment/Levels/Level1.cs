using System;
using System.Collections.Generic;
using System.Text;
using MathLibrary;
using Raylib_cs;

namespace AIEDec042020Assessment
{
    class Level1 : Level
    {
        private struct Wave
        {
            public bool Spawned;
            public int StartTime;
            public int Delay;
            public int MaxCounter;
            public int Counter;
            public Wave(bool spawned = false, int startTime = 1000, int delay = 100, int maxCounter = 5, int counter = 0)
            {
                this.Spawned = spawned;
                this.StartTime = startTime;
                this.Delay = delay;
                this.MaxCounter = maxCounter;
                this.Counter = counter;
            }
        }

        private Wave[] _waves = new Wave[5];

        public Level1() : base() { }

        private void SpawnWave(int index)
        {
            // Ensure a positive index
            index = Math.Max(0, index);

            // Check if this wave has already been spawned
            if (_waves[index].Spawned == true)
                return;

            // Check that all previous waves have been spawned
            for (int i = index; i >= 0; i--)
            {
                // Ensure that we don't check an index < 0
                if (i - 1 >= 0)
                    if (_waves[i - 1].Spawned == false)
                        return;
            }


            if (_timer.ElapsedMilliseconds > _waves[index].StartTime + (_waves[index].Delay * _waves[index].Counter))
            {
                PatrolEnemy enemy = Actor.Instantiate(new PatrolEnemy((Raylib.GetScreenWidth() / 2, -30))) as PatrolEnemy;
                enemy.Target = _player;
                _waves[index].Counter++;
            }

            if (_waves[index].Counter >= _waves[index].MaxCounter)
            {
                _waves[index].Spawned = true;
                _timer.Restart();
            }
        }

        #region CORE
        public override void Start()
        {
            base.Start();
            for (int i = 0; i < _waves.Length; i++)
            {
                _waves[i] = new Wave(false, 1000, 200, 5, 0);
            }
        }
        public override void Update(float deltaTime)
        {
            for (int i = 0; i < _waves.Length; i++)
            {
                SpawnWave(i);
            }
            base.Update(deltaTime);
        }
        #endregion
    }
}
