using System;
using System.Collections.Generic;
using CyborgPunch.Core;
using Microsoft.Xna.Framework;

namespace CyborgPunch.Game.Enemies
{
    public class EnemySpawner : Component
    {
        Blob dude;
        public float speed = 70f;

        int spawnTime = 4;
        int spawnTimeMax = 6;
        float timer;

        int[] spawnCounts = { 1, 2, 3, 4, 5 };
        int[] scoreThresholds = { 0, 20, 40, 80, 150 };

        Rectangle target;

        static Random rand { get { return RandomCore.random; } }

        public EnemySpawner(Rectangle target)
            : base()
        {
            this.target = target;
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

            //determine how many to spawn
            int spawnCount = spawnCounts[0];
            for (int i = 0; i < scoreThresholds.Length; i++)
            {
                if (ScoreManager.Instance.Score < scoreThresholds[i])
                {
                    spawnCount = spawnCounts[i - 1];
                    break;
                }
            }

            //spawnCount = rand.Next(1, spawnCount);

            for (int i = 0; i < spawnCount; i++)
            {
                Blob enemy = new Blob();
                Enemy comp = new Enemy(this.blob.transform.Position, RandomPointInRect(target));
                enemy.AddComponent(comp);
                enemy.transform.Parent = blob.transform;
                enemy.transform.Position = blob.transform.Position;
            }
        }

        Vector2 RandomPointInRect(Rectangle rect)
        {
            Vector2 point = new Vector2();
            point.X = rand.Next(rect.Left, rect.Right);
            point.Y = rand.Next(rect.Top, rect.Bottom);
            return point;
        }

    }
}
