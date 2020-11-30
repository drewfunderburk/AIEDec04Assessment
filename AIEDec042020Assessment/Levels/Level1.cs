using System;
using System.Collections.Generic;
using System.Text;
using MathLibrary;
using Raylib_cs;

namespace AIEDec042020Assessment
{
    class Level1 : Level
    {
        /// <summary>
        /// Stores data for a wave of enemies
        /// </summary>
        private struct Wave
        {
            // Whether or not this wave has been spawned
            public bool Spawned;

            // What time on the timer this wave is meant to start
            public int StartTime;

            // Delay between enemy spawns
            public int Delay;

            // Maximum number of enemies to spawn
            public int MaxCounter;

            // Number of enemies from this wave spawned
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

        // Store this level's waves
        private Wave[] _waves = new Wave[5];

        public Level1() : base() { }

        // Spawns a wave of enemies from _waves
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
                // Ensure that we don't check an index < 0 since this is a decrementing for loop
                if (i - 1 >= 0)
                    if (_waves[i - 1].Spawned == false)
                        return;
            }

            // Check if enough time has passed to spawn a new enemy
            if (_timer.ElapsedMilliseconds > _waves[index].StartTime + (_waves[index].Delay * _waves[index].Counter))
            {
                // Spawn new PatrolEnemy
                PatrolEnemy enemy = Actor.Instantiate(new PatrolEnemy((Raylib.GetScreenWidth() / 2, -30))) as PatrolEnemy;
                enemy.Target = _player;
                _waves[index].Counter++;
            }

            // Check if all enemies have been spawned
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

            // Initialize waves
            for (int i = 0; i < _waves.Length; i++)
            {
                _waves[i] = new Wave(false, 1000, 200, 5, 0);
            }
        }
        public override void Update(float deltaTime)
        {
            // Spawn waves
            for (int i = 0; i < _waves.Length; i++)
            {
                SpawnWave(i);
            }
            base.Update(deltaTime);
        }
        #endregion
    }
}
