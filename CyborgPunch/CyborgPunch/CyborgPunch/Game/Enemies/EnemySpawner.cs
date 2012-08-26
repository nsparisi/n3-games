using System;
using System.Collections.Generic;
using CyborgPunch.Core;
using Microsoft.Xna.Framework;

namespace CyborgPunch.Game.Enemies
{
    public class EnemySpawner : Component
    {
        Blob dude;
        public float speed = 70;

        float spawnTime = 5;
        float timer;

        public EnemySpawner()
            : base()
        {
        }

        public override void Start()
        {
            base.Start();

            blob.transform.Position = new Vector2(Constants.GAME_WIDTH / 2, Constants.GAME_HEIGHT + 100);
        }

        public override void Update()
        {
            base.Update();

            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                SpawnEnemy();
                timer += spawnTime;
            }
        }

        private void SpawnEnemy()
        {

            Blob enemy = new Blob();
            Enemy comp = new Enemy();
            enemy.AddComponent(comp);
            enemy.transform.Parent = blob.transform;
            enemy.transform.Position = blob.transform.Position;
        }

    }
}
