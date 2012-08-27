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

        int spawnTime = 4;
        int spawnTimeMax = 10;
        float timer;


        static Random rand = new Random();

        public EnemySpawner()
            : base()
        {
        }

        public override void Start()
        {
            base.Start();
            timer = rand.Next(spawnTime, spawnTimeMax);
        }


        public override void Update()
        {
            base.Update();

            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                SpawnEnemy();
            }
        }

        public void SpawnEnemy()
        {
            timer = rand.Next(spawnTime, spawnTimeMax);
            Blob enemy = new Blob();
            Enemy comp = new Enemy();
            enemy.AddComponent(comp);
            enemy.transform.Parent = blob.transform;
            enemy.transform.Position = blob.transform.Position;
        }

    }
}
